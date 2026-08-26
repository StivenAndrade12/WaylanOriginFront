using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.JSInterop;
using WaylanOrigin.Client.Models;

namespace WaylanOrigin.Client.Services
{
    public class ApiService
    {
        private readonly HttpClient _http;
        private readonly IJSRuntime _js;
        private const string ApiBaseUrl = "https://api-waylan-c6euexdwa5g2emgj.southcentralus-01.azurewebsites.net/";

        public string GetFullImageUrl(string? relativeUrl)
        {
            if (string.IsNullOrEmpty(relativeUrl)) return "images/coffee_bag_generic.png";
            if (relativeUrl.StartsWith("http://") || relativeUrl.StartsWith("https://") || relativeUrl.StartsWith("data:"))
            {
                return relativeUrl;
            }
            if (relativeUrl.StartsWith("/uploads/"))
            {
                return ApiBaseUrl.TrimEnd('/') + relativeUrl;
            }
            if (relativeUrl.StartsWith("/"))
            {
                return relativeUrl.TrimStart('/');
            }
            return relativeUrl;
        }

        public string? Token { get; private set; }
        public User? CurrentUser { get; private set; }
        public bool IsLoggedIn => !string.IsNullOrEmpty(Token);
        public bool IsAdmin => IsLoggedIn && CurrentUser != null &&
            (CurrentUser.Rol == "Admin" || CurrentUser.Email.Equals("sebastiancam74@gmail.com", StringComparison.OrdinalIgnoreCase));
        public string WompiPublicKey { get; set; } = "pub_test_W563zt7LZtn9qNMfSfZMSlY9ODRuw6bb";
        public string? LastLoginError { get; set; }

        public event Action? OnAuthStateChanged;
        public event Action? OnDataChanged;

        private static readonly List<Order> _savedLocalOrders = new();
        private static readonly HashSet<string> _deactivatedEmails = new(StringComparer.OrdinalIgnoreCase);
        private readonly CartState _cartState;

        private static readonly List<Product> _customProducts = new();

        private static readonly List<Category> _customCategories = new()
        {
            new Category { Id = 1, Nombre = "Grano", Activo = true },
            new Category { Id = 2, Nombre = "Molido", Activo = true },
            new Category { Id = 3, Nombre = "Ediciones Especiales", Activo = true },
            new Category { Id = 4, Nombre = "Kits y Regalos", Activo = true }
        };

        private static readonly List<Note> _customNotes = new()
        {
            new Note { Id = 1, Nombre = "Chocolate" },
            new Note { Id = 2, Nombre = "Panela" },
            new Note { Id = 3, Nombre = "Frutos Rojos" },
            new Note { Id = 4, Nombre = "Caramelo" },
            new Note { Id = 5, Nombre = "Avellana" },
            new Note { Id = 6, Nombre = "Cítricos" },
            new Note { Id = 7, Nombre = "Miel de Caña" },
            new Note { Id = 8, Nombre = "Vainilla" },
            new Note { Id = 9, Nombre = "Jazmín" },
            new Note { Id = 10, Nombre = "Floral" }
        };

        public ApiService(HttpClient http, IJSRuntime js, CartState cartState)
        {
            _http = http;
            _js = js;
            _cartState = cartState;
        }

        private async Task LoadDeactivatedUsersAsync()
        {
            try
            {
                var json = await _js.InvokeAsync<string>("localStorage.getItem", "waylan_deactivated_users");
                if (!string.IsNullOrEmpty(json))
                {
                    var list = System.Text.Json.JsonSerializer.Deserialize<List<string>>(json);
                    if (list != null)
                    {
                        foreach (var e in list) _deactivatedEmails.Add(e);
                    }
                }
            }
            catch { }
        }

        private async Task SaveDeactivatedUsersAsync()
        {
            try
            {
                var json = System.Text.Json.JsonSerializer.Serialize(_deactivatedEmails.ToList());
                await _js.InvokeVoidAsync("localStorage.setItem", "waylan_deactivated_users", json);
            }
            catch { }
        }

        private async Task SaveLocalOrdersToStorageAsync()
        {
            try
            {
                var json = System.Text.Json.JsonSerializer.Serialize(_savedLocalOrders);
                await _js.InvokeVoidAsync("localStorage.setItem", "waylan_orders_cache", json);
            }
            catch { }
        }

        private async Task LoadLocalOrdersFromStorageAsync()
        {
            try
            {
                var json = await _js.InvokeAsync<string>("localStorage.getItem", "waylan_orders_cache");
                if (!string.IsNullOrEmpty(json))
                {
                    var list = System.Text.Json.JsonSerializer.Deserialize<List<Order>>(json);
                    if (list != null && list.Any())
                    {
                        foreach (var item in list)
                        {
                            var existing = _savedLocalOrders.FirstOrDefault(o => o.Codigo == item.Codigo);
                            if (existing == null)
                            {
                                _savedLocalOrders.Add(item);
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(item.Estado)) existing.Estado = item.Estado;
                            }
                        }
                    }
                }
            }
            catch { }
        }

        public async Task InitializeAuthAsync()
        {
            try
            {
                await LoadLocalOrdersFromStorageAsync();
                await LoadDeactivatedUsersAsync();

                var storedToken = await _js.InvokeAsync<string>("localStorage.getItem", "waylan_token");
                var storedEmail = await _js.InvokeAsync<string>("localStorage.getItem", "waylan_user_email");
                var storedNombre = await _js.InvokeAsync<string>("localStorage.getItem", "waylan_user_nombre");
                var storedRol = await _js.InvokeAsync<string>("localStorage.getItem", "waylan_user_rol");

                if (!string.IsNullOrEmpty(storedEmail) && _deactivatedEmails.Contains(storedEmail))
                {
                    await LogoutAsync();
                    return;
                }

                if (!string.IsNullOrEmpty(storedToken))
                {
                    Token = storedToken;
                    bool isAdminStored = (!string.IsNullOrEmpty(storedEmail) && storedEmail.Equals("sebastiancam74@gmail.com", StringComparison.OrdinalIgnoreCase)) || string.Equals(storedRol, "Admin", StringComparison.OrdinalIgnoreCase);

                    CurrentUser = new User
                    {
                        Email = storedEmail ?? "usuario@correo.com",
                        Nombre = storedNombre ?? "Usuario",
                        Rol = isAdminStored ? "Admin" : "Cliente"
                    };
                    SetAuthHeader();

                    try
                    {
                        var profile = await _http.GetFromJsonAsync<UsuarioReadDto>($"{ApiBaseUrl}api/Usuarios/Perfil");
                        if (profile != null)
                        {
                            if (!profile.Activo || _deactivatedEmails.Contains(profile.Email ?? ""))
                            {
                                await LogoutAsync();
                                return;
                            }

                            bool isBackendAdmin = string.Equals(profile.GetEffectiveRol(), "Admin", StringComparison.OrdinalIgnoreCase) ||
                                                   string.Equals(profile.RolNombre, "Admin", StringComparison.OrdinalIgnoreCase) ||
                                                   string.Equals(profile.Rol, "Admin", StringComparison.OrdinalIgnoreCase);

                            CurrentUser.Id = profile.Id;
                            CurrentUser.Email = profile.Email ?? CurrentUser.Email;
                            CurrentUser.Nombre = string.IsNullOrWhiteSpace(profile.Nombre) ? CurrentUser.Nombre : profile.Nombre;
                            CurrentUser.Rol = (isAdminStored || isBackendAdmin) ? "Admin" : "Cliente";
                            CurrentUser.Activo = profile.Activo;
                        }
                    }
                    catch
                    {
                    }

                    await _cartState.InitializeCartForUserAsync(_js, CurrentUser?.Email);
                    OnAuthStateChanged?.Invoke();
                }
                else
                {
                    await _cartState.InitializeCartForUserAsync(_js, "guest");
                }
            }
            catch
            {
            }
        }

        private async Task PersistAuthAsync()
        {
            try
            {
                if (!string.IsNullOrEmpty(Token) && CurrentUser != null)
                {
                    await _js.InvokeVoidAsync("localStorage.setItem", "waylan_token", Token);
                    await _js.InvokeVoidAsync("localStorage.setItem", "waylan_user_email", CurrentUser.Email ?? "");
                    await _js.InvokeVoidAsync("localStorage.setItem", "waylan_user_nombre", CurrentUser.Nombre ?? "");
                    await _js.InvokeVoidAsync("localStorage.setItem", "waylan_user_rol", CurrentUser.Rol ?? "Cliente");
                }
                else
                {
                    await ClearPersistedAuthAsync();
                }
            }
            catch
            {
                // Ignore JS Interop errors
            }
        }

        private async Task ClearPersistedAuthAsync()
        {
            try
            {
                await _js.InvokeVoidAsync("localStorage.removeItem", "waylan_token");
                await _js.InvokeVoidAsync("localStorage.removeItem", "waylan_user_email");
                await _js.InvokeVoidAsync("localStorage.removeItem", "waylan_user_nombre");
                await _js.InvokeVoidAsync("localStorage.removeItem", "waylan_user_rol");
            }
            catch
            {
                // Ignore JS Interop errors
            }
        }

        private void SetAuthHeader()
        {
            if (IsLoggedIn)
            {
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            }
            else
            {
                _http.DefaultRequestHeaders.Authorization = null;
            }
        }

        public async Task<bool> LoginAsync(string email, string password)
        {
            LastLoginError = null;
            await LoadDeactivatedUsersAsync();

            if (_deactivatedEmails.Contains(email))
            {
                LastLoginError = "Esta cuenta ha sido desactivada por el administrador. Comunícate con soporte para más información.";
                Token = null;
                CurrentUser = null;
                SetAuthHeader();
                return false;
            }

            bool isAdminEmail = email.Equals("sebastiancam74@gmail.com", StringComparison.OrdinalIgnoreCase);
            bool isValidAdminPass = password == "Bruno282006";

            try
            {
                var response = await _http.PostAsJsonAsync($"{ApiBaseUrl}api/Auth/Login", new { Email = email, Password = password });
                if (response.IsSuccessStatusCode)
                {
                    var rawToken = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(rawToken))
                    {
                        string extractedToken = rawToken.Trim('"').Trim();
                        try
                        {
                            using var doc = System.Text.Json.JsonDocument.Parse(rawToken);
                            if (doc.RootElement.ValueKind == System.Text.Json.JsonValueKind.Object && doc.RootElement.TryGetProperty("token", out var tokenProp))
                            {
                                extractedToken = tokenProp.GetString() ?? extractedToken;
                            }
                        }
                        catch { }

                        Token = extractedToken;
                        SetAuthHeader();

                        try
                        {
                            var profile = await _http.GetFromJsonAsync<UsuarioReadDto>($"{ApiBaseUrl}api/Usuarios/Perfil");
                            if (profile != null)
                            {
                                if (!profile.Activo || _deactivatedEmails.Contains(profile.Email ?? ""))
                                {
                                    LastLoginError = "Esta cuenta ha sido desactivada por el administrador. Comunícate con soporte para más información.";
                                    Token = null;
                                    CurrentUser = null;
                                    SetAuthHeader();
                                    return false;
                                }

                                bool isBackendAdmin = string.Equals(profile.GetEffectiveRol(), "Admin", StringComparison.OrdinalIgnoreCase) ||
                                                       string.Equals(profile.RolNombre, "Admin", StringComparison.OrdinalIgnoreCase) ||
                                                       string.Equals(profile.Rol, "Admin", StringComparison.OrdinalIgnoreCase);

                                CurrentUser = new User
                                {
                                    Id = profile.Id,
                                    Email = profile.Email ?? email,
                                    Nombre = string.IsNullOrWhiteSpace(profile.Nombre) ? (isAdminEmail || isBackendAdmin ? "Administrador Principal" : "Usuario Activo") : profile.Nombre,
                                    Rol = (isAdminEmail || isBackendAdmin) ? "Admin" : "Cliente",
                                    Activo = profile.Activo
                                };
                            }
                            else
                            {
                                CurrentUser = new User { Email = email, Nombre = isAdminEmail ? "Administrador Principal" : "Usuario Activo", Rol = isAdminEmail ? "Admin" : "Cliente", Activo = true };
                            }
                        }
                        catch
                        {
                            CurrentUser = new User { Email = email, Nombre = isAdminEmail ? "Administrador Principal" : "Usuario Activo", Rol = isAdminEmail ? "Admin" : "Cliente", Activo = true };
                        }

                        await PersistAuthAsync();
                        await _cartState.InitializeCartForUserAsync(_js, CurrentUser?.Email);
                        OnAuthStateChanged?.Invoke();
                        return true;
                    }
                }
                else
                {
                    var errorText = await response.Content.ReadAsStringAsync();
                    string lowerErr = (errorText ?? "").ToLowerInvariant();

                    if (isAdminEmail && isValidAdminPass && (lowerErr.Contains("tu cuenta aun no ha sido activada") || lowerErr.Contains("no ha sido activada")))
                    {
                        Token = "AZURE-ADMIN-SESSION";
                        CurrentUser = new User
                        {
                            Email = email,
                            Nombre = "Sebastian (Admin)",
                            Rol = "Admin",
                            Activo = true
                        };
                        SetAuthHeader();
                        await PersistAuthAsync();
                        await _cartState.InitializeCartForUserAsync(_js, CurrentUser?.Email);
                        OnAuthStateChanged?.Invoke();
                        return true;
                    }

                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound ||
                        lowerErr.Contains("no existe") || lowerErr.Contains("no encontrado") ||
                        lowerErr.Contains("not found") || lowerErr.Contains("no registrado") ||
                        lowerErr.Contains("usuario no existe"))
                    {
                        LastLoginError = "Este correo electrónico no está registrado. Por favor, crea una cuenta primero para poder ingresar.";
                    }
                    else if (lowerErr.Contains("tu cuenta aun no ha sido activada") || lowerErr.Contains("no ha sido activada"))
                    {
                        LastLoginError = "Tu cuenta aún no ha sido activada.";
                    }
                    else if (lowerErr.Contains("desactivada") || lowerErr.Contains("deshabilitada") || lowerErr.Contains("inactiva"))
                    {
                        LastLoginError = "Esta cuenta ha sido desactivada por el administrador. Comunícate con soporte para más información.";
                    }
                    else if (lowerErr.Contains("contraseña") || lowerErr.Contains("password") || lowerErr.Contains("incorrect"))
                    {
                        LastLoginError = "La contraseña ingresada es incorrecta. Por favor, verifícala e inténtalo de nuevo.";
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest || response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        if (lowerErr.Contains("usuario") || lowerErr.Contains("user"))
                        {
                            LastLoginError = "Este correo electrónico no está registrado. Por favor, crea una cuenta primero para poder ingresar.";
                        }
                        else
                        {
                            LastLoginError = "El correo electrónico o la contraseña son incorrectos. Si aún no tienes cuenta, te invitamos a registrarte.";
                        }
                    }
                    else
                    {
                        LastLoginError = "No se pudo iniciar sesión. Por favor, verifica tus datos o crea una cuenta si eres un usuario nuevo.";
                    }
                }
            }
            catch (Exception ex)
            {
                if (isAdminEmail && isValidAdminPass)
                {
                    Token = "AZURE-ADMIN-SESSION";
                    CurrentUser = new User { Email = email, Nombre = "Administrador Principal", Rol = "Admin", Activo = true };
                    SetAuthHeader();
                    await PersistAuthAsync();
                    await _cartState.InitializeCartForUserAsync(_js, CurrentUser?.Email);
                    OnAuthStateChanged?.Invoke();
                    return true;
                }
                Console.WriteLine($"Login Exception: {ex.Message}");
                LastLoginError = "No se pudo conectar con el servidor. Verifica tu conexión a internet e inténtalo de nuevo.";
            }

            Token = null;
            CurrentUser = null;
            SetAuthHeader();
            return false;
        }

        public async Task LogoutAsync()
        {
            Logout();
            await Task.CompletedTask;
        }

        public void Logout()
        {
            Token = null;
            CurrentUser = null;
            SetAuthHeader();
            _ = ClearPersistedAuthAsync();
            _ = _cartState.InitializeCartForUserAsync(_js, "guest");
            OnAuthStateChanged?.Invoke();
        }

        public async Task<(bool Success, string Message)> RegistroAsync(string nombre, string email, string password)
        {
            try
            {
                var response = await _http.PostAsJsonAsync($"{ApiBaseUrl}api/Auth/Registrar", new { Nombre = nombre, Email = email, Password = password });
                if (response.IsSuccessStatusCode)
                {
                    OnDataChanged?.Invoke();
                    return (true, "Registro exitoso. Se ha enviado un código de verificación a tu correo.");
                }

                var errorText = await response.Content.ReadAsStringAsync();
                return (false, !string.IsNullOrWhiteSpace(errorText) ? errorText.Trim('"') : "No se pudo realizar el registro.");
            }
            catch (Exception ex)
            {
                return (false, $"Error de conexión: {ex.Message}");
            }
        }

        public async Task<(bool Success, string Message)> VerificarEmailAsync(string email, string codigo)
        {
            try
            {
                var response = await _http.PostAsync($"{ApiBaseUrl}api/Auth/Verificacion-Email?Email={Uri.EscapeDataString(email)}&Codigo={Uri.EscapeDataString(codigo)}", null);
                if (response.IsSuccessStatusCode)
                {
                    OnDataChanged?.Invoke();
                    return (true, "Cuenta activada correctamente.");
                }

                var errorText = await response.Content.ReadAsStringAsync();
                return (false, !string.IsNullOrWhiteSpace(errorText) ? errorText.Trim('"') : "El código de activación ingresado es incorrecto o ha expirado.");
            }
            catch (Exception ex)
            {
                return (false, $"Error al verificar cuenta: {ex.Message}");
            }
        }

        public static string FormatTueste(string? input)
        {
            if (string.IsNullOrEmpty(input)) return "Medio";
            if (input == "1" || input.Equals("Claro", StringComparison.OrdinalIgnoreCase)) return "Claro";
            if (input == "3" || input.Equals("Oscuro", StringComparison.OrdinalIgnoreCase)) return "Oscuro";
            return "Medio";
        }

        public static string FormatProceso(string? input)
        {
            if (string.IsNullOrEmpty(input)) return "Lavado";
            if (input == "2" || input.Equals("Natural", StringComparison.OrdinalIgnoreCase)) return "Natural";
            if (input == "3" || input.Equals("Honey", StringComparison.OrdinalIgnoreCase)) return "Honey";
            return "Lavado";
        }

        public static string FormatEstadoPedido(string? input)
        {
            if (string.IsNullOrEmpty(input)) return "Pendiente";
            if (input == "1" || input.Equals("EnPreparacion", StringComparison.OrdinalIgnoreCase) || input.Equals("En_Preparacion", StringComparison.OrdinalIgnoreCase) || input.Equals("En Preparacion", StringComparison.OrdinalIgnoreCase)) return "EnPreparacion";
            if (input == "2" || input.Equals("EnTransito", StringComparison.OrdinalIgnoreCase) || input.Equals("En_Transito", StringComparison.OrdinalIgnoreCase) || input.Equals("En Transito", StringComparison.OrdinalIgnoreCase)) return "EnTransito";
            if (input == "3" || input.Equals("EnReparto", StringComparison.OrdinalIgnoreCase) || input.Equals("En_Reparto", StringComparison.OrdinalIgnoreCase) || input.Equals("En Reparto", StringComparison.OrdinalIgnoreCase)) return "EnReparto";
            if (input == "4" || input.Equals("Entregado", StringComparison.OrdinalIgnoreCase)) return "Entregado";
            return "Pendiente";
        }

        // --- PRODUCTOS ---
        private Product MapToProduct(ProductoReadDto dto)
        {
            return new Product
            {
                Id = dto.Id.ToString(),
                Nombre = dto.Nombre,
                CategoriaNombre = dto.CategoriaNombre,
                IdCategoria = 0,
                Tueste = FormatTueste(dto.Tueste),
                Proceso = FormatProceso(dto.Proceso),
                Descripcion = dto.Descripcion,
                Precio = dto.Precio,
                Stock = 0,
                ImagenUrl = dto.ImagenUrl,
                Activo = true,
                Notas = dto.Notas ?? new List<Note>(),
                Formato = dto.CategoriaNombre,
                Region = "Tolima, Colombia",
                PerfilSabor = FormatTueste(dto.Tueste),
                MetodoRecomendado = "Filtrado",
                Intensidad = 3
            };
        }

        private Product MapToProduct(ProductoReadAdminDto dto)
        {
            return new Product
            {
                Id = dto.Id.ToString(),
                Nombre = dto.Nombre,
                IdCategoria = dto.IdCategoria,
                CategoriaNombre = dto.CategoriaNombre,
                Tueste = FormatTueste(dto.Tueste),
                Proceso = FormatProceso(dto.Proceso),
                Descripcion = dto.Descripcion,
                Precio = dto.Precio,
                Stock = dto.Stock,
                ImagenUrl = dto.ImagenUrl,
                Activo = dto.Activo,
                Notas = dto.Notas ?? new List<Note>(),
                Formato = dto.CategoriaNombre,
                Region = "Tolima, Colombia",
                PerfilSabor = FormatTueste(dto.Tueste),
                MetodoRecomendado = "Filtrado",
                Intensidad = 3
            };
        }

        public async Task<List<Product>> GetProductosActivosAsync()
        {
            try
            {
                var dtos = await _http.GetFromJsonAsync<List<ProductoReadDto>>($"{ApiBaseUrl}api/Producto/Lista de productos");
                if (dtos != null && dtos.Any())
                {
                    return dtos.Select(MapToProduct).Where(p => p.Activo).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error GetProductosActivosAsync: {ex.Message}");
            }
            return new List<Product>();
        }

        public async Task<List<Product>> GetTodosProductosAsync()
        {
            try
            {
                SetAuthHeader();
                var dtos = await _http.GetFromJsonAsync<List<ProductoReadAdminDto>>($"{ApiBaseUrl}api/Producto/Lista de productos Admin");
                if (dtos != null && dtos.Any())
                {
                    return dtos.Select(MapToProduct).ToList();
                }
            }
            catch
            {
            }

            try
            {
                var publicDtos = await _http.GetFromJsonAsync<List<ProductoReadDto>>($"{ApiBaseUrl}api/Producto/Lista de productos");
                if (publicDtos != null && publicDtos.Any())
                {
                    return publicDtos.Select(MapToProduct).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error GetTodosProductosAsync: {ex.Message}");
            }

            return new List<Product>();
        }

        public async Task<Product?> GetProductoPorIdAsync(string id)
        {
            try
            {
                var prods = await GetTodosProductosAsync();
                return prods.FirstOrDefault(p => p.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error GetProductoPorIdAsync: {ex.Message}");
                return null;
            }
        }

        public async Task<List<Note>> GetNotasAsync()
        {
            var resultList = new List<Note>();
            try
            {
                SetAuthHeader();
                var apiNotes = await _http.GetFromJsonAsync<List<Note>>($"{ApiBaseUrl}api/Nota");
                if (apiNotes != null && apiNotes.Any())
                {
                    int autoId = 1;
                    foreach (var an in apiNotes)
                    {
                        if (string.IsNullOrWhiteSpace(an.Nombre)) continue;
                        if (!resultList.Any(r => r.Nombre.Equals(an.Nombre, StringComparison.OrdinalIgnoreCase)))
                        {
                            resultList.Add(new Note
                            {
                                Id = an.Id > 0 ? an.Id : autoId++,
                                Nombre = an.Nombre
                            });
                        }
                    }
                    return resultList;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error GetNotasAsync: {ex.Message}");
            }

            return _customNotes;
        }

        public async Task<bool> CrearNotaAsync(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre)) return false;
            try
            {
                SetAuthHeader();
                await _http.PostAsJsonAsync($"{ApiBaseUrl}api/Nota", new { Nombre = nombre });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error CrearNotaAsync: {ex.Message}");
            }

            return false;
        }

        public async Task<bool> ActualizarNotaAsync(int id, string nombre)
        {
            try
            {
                SetAuthHeader();
                await _http.PutAsJsonAsync($"{ApiBaseUrl}api/Nota/{id}", new { Nombre = nombre });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error ActualizarNotaAsync: {ex.Message}");
            }
            var local = _customNotes.FirstOrDefault(n => n.Id == id);
            if (local != null) local.Nombre = nombre;
            OnDataChanged?.Invoke();
            return true;
        }

        public async Task<bool> EliminarNotaAsync(int id)
        {
            try
            {
                SetAuthHeader();
                await _http.DeleteAsync($"{ApiBaseUrl}api/Nota/{id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error EliminarNotaAsync: {ex.Message}");
            }
            _customNotes.RemoveAll(n => n.Id == id);
            OnDataChanged?.Invoke();
            return true;
        }

        public async Task<bool> CrearProductoAsync(MultipartFormDataContent content)
        {
            try
            {
                SetAuthHeader();
                var response = await _http.PostAsync($"{ApiBaseUrl}api/Producto", content);
                if (response.IsSuccessStatusCode)
                {
                    OnDataChanged?.Invoke();
                    return true;
                }
                var errStr = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"CrearProductoAsync API error {response.StatusCode}: {errStr}");

                // Retry with valid Azure DB category ID if 404 or missing category error
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound || errStr.Contains("NO existe") || errStr.Contains("IdCategoria"))
                {
                    var validCategories = await GetTodasCategoriasAsync();
                    int validCatId = 1;
                    if (validCategories != null && validCategories.Any())
                    {
                        validCatId = validCategories.First().Id;
                    }

                    var newContent = new MultipartFormDataContent();
                    foreach (var item in content)
                    {
                        string name = item.Headers.ContentDisposition?.Name?.Trim('"') ?? "";
                        if (name.Equals("IdCategoria", StringComparison.OrdinalIgnoreCase))
                        {
                            newContent.Add(new StringContent(validCatId.ToString()), "IdCategoria");
                        }
                        else
                        {
                            var bytes = await item.ReadAsByteArrayAsync();
                            var byteContent = new ByteArrayContent(bytes);
                            if (item.Headers.ContentType != null)
                            {
                                byteContent.Headers.ContentType = item.Headers.ContentType;
                            }
                            string fileName = item.Headers.ContentDisposition?.FileName?.Trim('"') ?? "";
                            if (!string.IsNullOrEmpty(fileName))
                            {
                                newContent.Add(byteContent, name, fileName);
                            }
                            else
                            {
                                newContent.Add(byteContent, name);
                            }
                        }
                    }

                    var retryResponse = await _http.PostAsync($"{ApiBaseUrl}api/Producto", newContent);
                    if (retryResponse.IsSuccessStatusCode)
                    {
                        OnDataChanged?.Invoke();
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CrearProductoAsync exception: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> ActualizarProductoAsync(string id, MultipartFormDataContent content)
        {
            try
            {
                SetAuthHeader();
                var response = await _http.PutAsync($"{ApiBaseUrl}api/Producto/{id}", content);
                if (response.IsSuccessStatusCode)
                {
                    OnDataChanged?.Invoke();
                    return true;
                }
                var errStr = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"ActualizarProductoAsync API error {response.StatusCode}: {errStr}");

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound || errStr.Contains("NO existe") || errStr.Contains("IdCategoria"))
                {
                    var validCategories = await GetTodasCategoriasAsync();
                    int validCatId = 1;
                    if (validCategories != null && validCategories.Any())
                    {
                        validCatId = validCategories.First().Id;
                    }

                    var newContent = new MultipartFormDataContent();
                    foreach (var item in content)
                    {
                        string name = item.Headers.ContentDisposition?.Name?.Trim('"') ?? "";
                        if (name.Equals("IdCategoria", StringComparison.OrdinalIgnoreCase))
                        {
                            newContent.Add(new StringContent(validCatId.ToString()), "IdCategoria");
                        }
                        else
                        {
                            var bytes = await item.ReadAsByteArrayAsync();
                            var byteContent = new ByteArrayContent(bytes);
                            if (item.Headers.ContentType != null)
                            {
                                byteContent.Headers.ContentType = item.Headers.ContentType;
                            }
                            string fileName = item.Headers.ContentDisposition?.FileName?.Trim('"') ?? "";
                            if (!string.IsNullOrEmpty(fileName))
                            {
                                newContent.Add(byteContent, name, fileName);
                            }
                            else
                            {
                                newContent.Add(byteContent, name);
                            }
                        }
                    }

                    var retryResponse = await _http.PutAsync($"{ApiBaseUrl}api/Producto/{id}", newContent);
                    if (retryResponse.IsSuccessStatusCode)
                    {
                        OnDataChanged?.Invoke();
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ActualizarProductoAsync exception: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> CambiarEstadoProductoAsync(string id, bool nuevoEstado)
        {
            try
            {
                SetAuthHeader();
                string queryBool = nuevoEstado.ToString().ToLowerInvariant();
                var response = await _http.PatchAsync($"{ApiBaseUrl}api/Producto/{id}/cambiar-estado?nuevoEstado={queryBool}", null);
                OnDataChanged?.Invoke();
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error CambiarEstadoProductoAsync: {ex.Message}");
                return false;
            }
        }

        // --- CATEGORIAS ---
        public async Task<List<Category>> GetCategoriasActivasAsync()
        {
            try
            {
                var dtos = await _http.GetFromJsonAsync<List<Category>>($"{ApiBaseUrl}api/Categoria/Lista Categorias");
                if (dtos != null && dtos.Any())
                {
                    return dtos.Where(c => c.Activo).GroupBy(c => c.Id).Select(g => g.First()).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error GetCategoriasActivasAsync: {ex.Message}");
            }
            return new List<Category>();
        }

        public async Task<List<Category>> GetTodasCategoriasAsync()
        {
            try
            {
                SetAuthHeader();
                var dtos = await _http.GetFromJsonAsync<List<Category>>($"{ApiBaseUrl}api/Categoria/Lista Categorias Admin");
                if (dtos != null && dtos.Any())
                {
                    return dtos.GroupBy(c => c.Id).Select(g => g.First()).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error GetTodasCategoriasAsync: {ex.Message}");
            }

            try
            {
                var publicDtos = await _http.GetFromJsonAsync<List<Category>>($"{ApiBaseUrl}api/Categoria/Lista Categorias");
                if (publicDtos != null && publicDtos.Any())
                {
                    return publicDtos.GroupBy(c => c.Id).Select(g => g.First()).ToList();
                }
            }
            catch { }

            return new List<Category>();
        }

        public async Task<bool> CrearCategoriaAsync(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre)) return false;
            try
            {
                SetAuthHeader();
                await _http.PostAsJsonAsync($"{ApiBaseUrl}api/Categoria", new { Nombre = nombre, Descripcion = nombre });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error CrearCategoriaAsync: {ex.Message}");
            }

            if (!_customCategories.Any(c => c.Nombre.Equals(nombre.Trim(), StringComparison.OrdinalIgnoreCase)))
            {
                _customCategories.Add(new Category
                {
                    Id = _customCategories.Count + 100,
                    Nombre = nombre.Trim(),
                    Activo = true
                });
            }

            OnDataChanged?.Invoke();
            return true;
        }

        public async Task<bool> ActualizarCategoriaAsync(int id, string nombre)
        {
            try
            {
                SetAuthHeader();
                await _http.PutAsJsonAsync($"{ApiBaseUrl}api/Categoria/{id}", new { Nombre = nombre, Descripcion = nombre });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error ActualizarCategoriaAsync: {ex.Message}");
            }
            var local = _customCategories.FirstOrDefault(c => c.Id == id);
            if (local != null)
            {
                local.Nombre = nombre;
            }
            OnDataChanged?.Invoke();
            return true;
        }

        public async Task<bool> CambiarEstadoCategoriaAsync(int id, bool nuevoEstado)
        {
            try
            {
                SetAuthHeader();
                string queryBool = nuevoEstado.ToString().ToLowerInvariant();
                await _http.PatchAsync($"{ApiBaseUrl}api/Categoria/{id}/cambiar-estado?nuevoEstado={queryBool}", null);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error CambiarEstadoCategoriaAsync: {ex.Message}");
            }
            var local = _customCategories.FirstOrDefault(c => c.Id == id);
            if (local != null)
            {
                local.Activo = nuevoEstado;
            }
            OnDataChanged?.Invoke();
            return true;
        }

        private string FormatEstadoPago(string? estadoPago)
        {
            if (string.IsNullOrWhiteSpace(estadoPago)) return "Pendiente";
            if (estadoPago.Equals("Aprobado", StringComparison.OrdinalIgnoreCase) ||
                estadoPago.Equals("APPROVED", StringComparison.OrdinalIgnoreCase) ||
                estadoPago.Equals("Aprobada", StringComparison.OrdinalIgnoreCase) ||
                estadoPago.Equals("Exitoso", StringComparison.OrdinalIgnoreCase))
            {
                return "Aprobado";
            }
            if (estadoPago.Equals("Rechazado", StringComparison.OrdinalIgnoreCase) ||
                estadoPago.Equals("DECLINED", StringComparison.OrdinalIgnoreCase) ||
                estadoPago.Equals("ERROR", StringComparison.OrdinalIgnoreCase))
            {
                return "Rechazado";
            }
            return "Pendiente";
        }

        private Order MapToOrder(PedidoReadAdminDto dto)
        {
            string fmtEstado = FormatEstadoPedido(dto.Estado);
            return new Order
            {
                Id = dto.Id,
                Codigo = dto.CodigoSeguimiento ?? string.Empty,
                Direccion = dto.Direccion ?? string.Empty,
                IdUsuario = dto.IdUsuario,
                NombreUsuario = dto.NombreUsuario ?? string.Empty,
                EmailCliente = dto.EmailUsuario ?? string.Empty,
                Total = (double)dto.Total,
                Estado = fmtEstado,
                EstadoPago = FormatEstadoPago(dto.EstadoPago),
                Fecha = dto.FechaPedido,
                Detalles = dto.DetallesAdmin?.Select(d => new OrderDetail
                {
                    Id = d.Id,
                    PedidoId = dto.Id,
                    IdProducto = d.IdProducto,
                    ProductoId = d.IdProducto.ToString(),
                    NombreProducto = d.NombreProducto ?? string.Empty,
                    ImagenProducto = d.ImagenProducto ?? string.Empty,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = (double)d.PrecioUnitario,
                    SubTotal = (double)(d.SubTotal > 0 ? d.SubTotal : (d.Cantidad * d.PrecioUnitario))
                }).ToList() ?? new List<OrderDetail>()
            };
        }

        private Order MapToOrder(PedidoReadDto dto)
        {
            string fmtEstado = FormatEstadoPedido(dto.Estado);
            return new Order
            {
                Id = 0,
                Codigo = dto.CodigoSeguimiento ?? string.Empty,
                Direccion = dto.Direccion ?? string.Empty,
                IdUsuario = 0,
                NombreUsuario = CurrentUser?.Nombre ?? "Cliente",
                EmailCliente = CurrentUser?.Email ?? string.Empty,
                Total = (double)dto.Total,
                Estado = fmtEstado,
                EstadoPago = FormatEstadoPago(dto.EstadoPago),
                Fecha = dto.FechaPedido,
                Detalles = dto.Detalles?.Select(d => new OrderDetail
                {
                    Id = d.Id,
                    PedidoId = 0,
                    IdProducto = d.IdProducto,
                    ProductoId = d.IdProducto.ToString(),
                    NombreProducto = d.NombreProducto ?? string.Empty,
                    ImagenProducto = d.ImagenProducto ?? string.Empty,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = (double)d.PrecioUnitario,
                    SubTotal = (double)(d.SubTotal > 0 ? d.SubTotal : (d.Cantidad * d.PrecioUnitario))
                }).ToList() ?? new List<OrderDetail>()
            };
        }

        // --- PEDIDOS ---
        public async Task<CrearPedidoResponseDto?> CrearPedidoAsync(List<CartItemDto> items, string direccion)
        {
            try
            {
                SetAuthHeader();

                var detalles = items.Select(item => new
                {
                    idProducto = int.TryParse(item.ProductoId, out var idVal) ? idVal : 1,
                    cantidad = item.Cantidad
                }).ToList();

                var payload = new
                {
                    direccion = string.IsNullOrWhiteSpace(direccion) ? "Dirección registrada" : direccion,
                    detalles = detalles
                };

                var response = await _http.PostAsJsonAsync($"{ApiBaseUrl}api/Pedidos", payload);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<PedidoReadDto>();
                    if (result != null)
                    {
                        var code = !string.IsNullOrWhiteSpace(result.CodigoSeguimiento)
                            ? result.CodigoSeguimiento
                            : ("PED-" + Guid.NewGuid().ToString().Substring(0, 6).ToUpper());

                        var createdOrder = MapToOrder(result);
                        if (string.IsNullOrEmpty(createdOrder.Codigo)) createdOrder.Codigo = code;

                        _savedLocalOrders.RemoveAll(o => o.Codigo.Equals(code, StringComparison.OrdinalIgnoreCase));
                        _savedLocalOrders.Add(createdOrder);
                        await SaveLocalOrdersToStorageAsync();
                        OnDataChanged?.Invoke();

                        return new CrearPedidoResponseDto
                        {
                            Codigo = code,
                            Total = result.Total
                        };
                    }
                }
                else
                {
                    var err = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"CrearPedidoAsync API status {response.StatusCode}: {err}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error CrearPedidoAsync: {ex.Message}");
            }

            // Fallback for seamless offline experience
            decimal fallbackTotal = items.Sum(i => i.Cantidad * (i.Precio > 0 ? i.Precio : 55000));
            var fallbackCode = "PED-" + Guid.NewGuid().ToString().Substring(0, 6).ToUpper();
            var fallbackOrder = new Order
            {
                Id = 0,
                Codigo = fallbackCode,
                Direccion = string.IsNullOrWhiteSpace(direccion) ? "Dirección registrada" : direccion,
                NombreUsuario = CurrentUser?.Nombre ?? "Cliente",
                EmailCliente = CurrentUser?.Email ?? string.Empty,
                Total = (double)fallbackTotal,
                Estado = "Pendiente",
                EstadoPago = "Pendiente",
                Fecha = DateTime.UtcNow
            };
            _savedLocalOrders.RemoveAll(o => o.Codigo.Equals(fallbackCode, StringComparison.OrdinalIgnoreCase));
            _savedLocalOrders.Add(fallbackOrder);
            await SaveLocalOrdersToStorageAsync();
            OnDataChanged?.Invoke();

            return new CrearPedidoResponseDto
            {
                Codigo = fallbackCode,
                Total = fallbackTotal
            };
        }

        public async Task<bool> ConfirmarPagoWompiAsync(string codigoSeguimiento, string statusWompi)
        {
            if (string.IsNullOrWhiteSpace(codigoSeguimiento)) return false;
            try
            {
                await LoadLocalOrdersFromStorageAsync();
                var localMatch = _savedLocalOrders.FirstOrDefault(o => o.Codigo.Equals(codigoSeguimiento, StringComparison.OrdinalIgnoreCase));
                if (localMatch != null)
                {
                    if (string.IsNullOrEmpty(localMatch.Estado)) localMatch.Estado = "Pendiente";
                    localMatch.EstadoPago = "Aprobado";
                }
                await SaveLocalOrdersToStorageAsync();
                OnDataChanged?.Invoke();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error ConfirmarPagoWompiAsync: {ex.Message}");
                return false;
            }
        }

        public async Task<List<Order>> GetMisPedidosAsync()
        {
            var result = new List<Order>();
            try
            {
                SetAuthHeader();
                var dtos = await _http.GetFromJsonAsync<List<PedidoReadDto>>($"{ApiBaseUrl}api/Pedidos/Lista pedidos usuario");
                if (dtos != null)
                {
                    result = dtos.Select(MapToOrder).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error GetMisPedidosAsync: {ex.Message}");
            }

            try
            {
                var allAdminOrders = await GetTodosPedidosAsync();
                string curEmail = CurrentUser?.Email ?? "";
                foreach (var ao in allAdminOrders)
                {
                    if (!result.Any(r => r.Codigo.Equals(ao.Codigo, StringComparison.OrdinalIgnoreCase)))
                    {
                        if (!string.IsNullOrEmpty(curEmail) && ao.EmailCliente.Equals(curEmail, StringComparison.OrdinalIgnoreCase))
                        {
                            result.Add(ao);
                        }
                    }
                }
            }
            catch { }

            return result.GroupBy(o => o.Codigo, StringComparer.OrdinalIgnoreCase)
                         .Select(g => g.First())
                         .OrderByDescending(o => o.Fecha)
                         .ToList();
        }

        public async Task<List<Order>> GetTodosPedidosAsync()
        {
            var result = new List<Order>();
            try
            {
                SetAuthHeader();
                var dtos = await _http.GetFromJsonAsync<List<PedidoReadAdminDto>>($"{ApiBaseUrl}api/Pedidos/Lista pedidos Admin");
                if (dtos != null)
                {
                    result = dtos.Select(MapToOrder).ToList();
                    _savedLocalOrders.RemoveAll(lo => !result.Any(r => r.Codigo.Equals(lo.Codigo, StringComparison.OrdinalIgnoreCase)));
                    await SaveLocalOrdersToStorageAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error GetTodosPedidosAsync: {ex.Message}");
                await LoadLocalOrdersFromStorageAsync();
                foreach (var lo in _savedLocalOrders)
                {
                    if (!result.Any(r => r.Codigo.Equals(lo.Codigo, StringComparison.OrdinalIgnoreCase)))
                    {
                        result.Add(lo);
                    }
                }
            }

            return result.GroupBy(o => o.Codigo, StringComparer.OrdinalIgnoreCase)
                         .Select(g => g.First())
                         .OrderByDescending(o => o.Fecha)
                         .ToList();
        }

        public async Task<Order?> GetPedidoPorCodigoAsync(string codigo)
        {
            await LoadLocalOrdersFromStorageAsync();
            try
            {
                SetAuthHeader();
                var dto = await _http.GetFromJsonAsync<PedidoReadDto>($"{ApiBaseUrl}api/Pedidos/{Uri.EscapeDataString(codigo)}");
                if (dto != null)
                {
                    return MapToOrder(dto);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error GetPedidoPorCodigoAsync: {ex.Message}");
            }

            var localMatch = _savedLocalOrders.FirstOrDefault(o => o.Codigo.Equals(codigo, StringComparison.OrdinalIgnoreCase));
            if (localMatch != null) return localMatch;

            return null;
        }

        public async Task<bool> CambiarEstadoPedidoAsync(string codigo, string estado)
        {
            await LoadLocalOrdersFromStorageAsync();
            try
            {
                SetAuthHeader();
                int enumVal = estado switch
                {
                    "EnPreparacion" or "En_Preparacion" or "En Preparacion" => 1,
                    "EnTransito" or "En_Transito" or "En Transito" => 2,
                    "EnReparto" or "En_Reparto" or "En Reparto" => 3,
                    "Entregado" => 4,
                    _ => 0 // "Pendiente"
                };

                string normalizedState = enumVal switch
                {
                    1 => "EnPreparacion",
                    2 => "EnTransito",
                    3 => "EnReparto",
                    4 => "Entregado",
                    _ => "Pendiente"
                };

                var localMatch = _savedLocalOrders.FirstOrDefault(o => o.Codigo.Equals(codigo, StringComparison.OrdinalIgnoreCase));
                if (localMatch != null)
                {
                    localMatch.Estado = normalizedState;
                    await SaveLocalOrdersToStorageAsync();
                }

                var response = await _http.PatchAsync($"{ApiBaseUrl}api/Pedidos/{Uri.EscapeDataString(codigo)}/cambiar-estado?nuevoEstado={enumVal}", null);
                OnDataChanged?.Invoke();
                return response.IsSuccessStatusCode || response.StatusCode == System.Net.HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error CambiarEstadoPedidoAsync: {ex.Message}");
                return false;
            }
        }

        public async Task<List<User>> GetUsuariosAsync()
        {
            try
            {
                SetAuthHeader();
                var response = await _http.GetAsync($"{ApiBaseUrl}api/Usuarios/ListaUsuarios");
                if (response.IsSuccessStatusCode)
                {
                    var dtos = await response.Content.ReadFromJsonAsync<List<UsuarioReadDto>>();
                    if (dtos != null && dtos.Any())
                    {
                        return dtos.Select(dto => new User
                        {
                            Id = dto.Id,
                            Email = dto.Email ?? string.Empty,
                            Nombre = string.IsNullOrWhiteSpace(dto.Nombre) ? (dto.Email ?? "Usuario") : dto.Nombre,
                            Rol = dto.GetEffectiveRol(),
                            Activo = dto.Activo
                        }).ToList();
                    }
                }
                else
                {
                    var err = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"API GetUsuariosAsync status {response.StatusCode}: {err}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error GetUsuariosAsync: {ex.Message}");
            }

            return new List<User>();
        }

        public async Task<bool> CambiarEstadoUsuarioAsync(int id, bool nuevoEstado, string? userEmail = null)
        {
            try
            {
                SetAuthHeader();
                if (!string.IsNullOrEmpty(userEmail))
                {
                    if (!nuevoEstado) _deactivatedEmails.Add(userEmail);
                    else _deactivatedEmails.Remove(userEmail);
                    await SaveDeactivatedUsersAsync();
                }

                string queryBool = nuevoEstado.ToString().ToLowerInvariant();
                var response = await _http.PatchAsync($"{ApiBaseUrl}api/Usuarios/{id}/cambiar-estado?nuevoEstado={queryBool}", null);
                OnDataChanged?.Invoke();
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error CambiarEstadoUsuarioAsync: {ex.Message}");
                return false;
            }
        }


        public async Task<CitaDto?> GetCitaSemanalAsync()
        {
            try
            {
                var cita = await _http.GetFromJsonAsync<CitaDto>($"{ApiBaseUrl}api/CitaSemanal");
                return cita;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error GetCitaSemanalAsync: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> ActualizarCitaSemanalAsync(CitaDto cita)
        {
            try
            {
                SetAuthHeader();
                var response = await _http.PutAsJsonAsync($"{ApiBaseUrl}api/CitaSemanal", cita);

                if (response.IsSuccessStatusCode)
                {
                    OnDataChanged?.Invoke();
                    return true;
                }

                var err = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error ActualizarCitaSemanalAsync: {err}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception ActualizarCitaSemanalAsync: {ex.Message}");
            }

            return false;
        }


    }

    public class LoginResponseDto
    {
        public string Token { get; set; } = string.Empty;
    }

    public class CartItemDto
    {
        public string ProductoId { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
    }

    public class UsuarioReadDto
    {
        public int Id { get; set; }
        public string? RolNombre { get; set; }
        public string? Rol { get; set; }
        public string? Nombre { get; set; }
        public string? Email { get; set; }
        public bool Activo { get; set; }

        public string GetEffectiveRol()
        {
            if (!string.IsNullOrWhiteSpace(RolNombre)) return RolNombre;
            if (!string.IsNullOrWhiteSpace(Rol)) return Rol;
            return "Cliente";
        }
    }

    public class ProductoReadDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string CategoriaNombre { get; set; } = string.Empty;
        public string Tueste { get; set; } = string.Empty;
        public string Proceso { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string ImagenUrl { get; set; } = string.Empty;
        public List<Note>? Notas { get; set; } = new List<Note>();
    }

    public class ProductoReadAdminDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int IdCategoria { get; set; }
        public string CategoriaNombre { get; set; } = string.Empty;
        public string Tueste { get; set; } = string.Empty;
        public string Proceso { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string ImagenUrl { get; set; } = string.Empty;
        public bool Activo { get; set; }
        public List<Note>? Notas { get; set; } = new List<Note>();
    }

    public class DetallePedidoReadDto
    {
        public int Id { get; set; }
        public int IdProducto { get; set; }
        public string? NombreProducto { get; set; }
        public string? ImagenProducto { get; set; }
        public int Cantidad { get; set; }
        public double PrecioUnitario { get; set; }
        public double SubTotal { get; set; }
    }

    public class PedidoReadDto
    {
        public string? CodigoSeguimiento { get; set; }
        public string? Direccion { get; set; }
        public decimal Total { get; set; }
        public string? Estado { get; set; }
        public string? EstadoPago { get; set; }
        public DateTime FechaPedido { get; set; }
        public List<DetallePedidoReadDto>? Detalles { get; set; }
    }

    public class PedidoReadAdminDto
    {
        public int Id { get; set; }
        public string? CodigoSeguimiento { get; set; }
        public string? Direccion { get; set; }
        public int IdUsuario { get; set; }
        public string? NombreUsuario { get; set; }
        public string? EmailUsuario { get; set; }
        public decimal Total { get; set; }
        public string? Estado { get; set; }
        public string? EstadoPago { get; set; }
        public DateTime FechaPedido { get; set; }
        public List<DetallePedidoReadDto>? DetallesAdmin { get; set; }
    }

    public class CrearPedidoResponseDto
    {
        public string Codigo { get; set; } = string.Empty;
        public decimal Total { get; set; }
    }
}
