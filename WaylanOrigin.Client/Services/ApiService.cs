using System.Net.Http.Headers;
using System.Net.Http.Json;
using WaylanOrigin.Client.Models;

namespace WaylanOrigin.Client.Services
{
    public class ApiService
    {
        private readonly HttpClient _http;
        private const string ApiBaseUrl = "http://localhost:5074/";

        public string? Token { get; private set; }
        public User? CurrentUser { get; private set; }
        public bool IsLoggedIn => !string.IsNullOrEmpty(Token);
        public bool IsAdmin => IsLoggedIn && CurrentUser?.Rol == "Admin";

        public event Action? OnAuthStateChanged;

        // Mock state lists for standalone frontend execution
        private List<Product> _mockProducts = new();
        private List<Category> _mockCategories = new();
        private List<Order> _mockOrders = new();
        private List<User> _mockUsers = new();
        private List<Note> _mockNotes = new();

        public ApiService(HttpClient http)
        {
            _http = http;
            InitializeMockData();
        }

        private void InitializeMockData()
        {
            _mockNotes = new List<Note>
            {
                new Note { Id = 1, Nombre = "Chocolate" },
                new Note { Id = 2, Nombre = "Panela" },
                new Note { Id = 3, Nombre = "Frutos Rojos" },
                new Note { Id = 4, Nombre = "Caramelo" },
                new Note { Id = 5, Nombre = "Avellana" },
                new Note { Id = 6, Nombre = "Cítricos" },
                new Note { Id = 7, Nombre = "Miel de caña" }
            };

            _mockCategories = new List<Category>
            {
                new Category { Id = 1, Nombre = "Grano", Activo = true },
                new Category { Id = 2, Nombre = "Molido", Activo = true },
                new Category { Id = 3, Nombre = "Especiales", Activo = true },
                new Category { Id = 4, Nombre = "Regalos", Activo = true }
            };

            _mockProducts = new List<Product>
            {
                new Product { Id = "WAY-SEL", Nombre = "Waylan Selection", Region = "Tolima, Colombia", Formato = "Grano", PerfilSabor = "Dulce", MetodoRecomendado = "Filtrado", Intensidad = 4, Precio = 72000, Etiqueta = "NUEVO", ImagenUrl = "/images/coffee_bag_esperanza.png", Activo = true, Notas = new List<Note> { _mockNotes[0], _mockNotes[1], _mockNotes[2] } },
                new Product { Id = "WAY-CLA", Nombre = "Waylan Classic", Region = "Tolima, Colombia", Formato = "Molido", PerfilSabor = "Balanceado", MetodoRecomendado = "Filtrado", Intensidad = 4, Precio = 58000, Etiqueta = "MÁS VENDIDO", ImagenUrl = "/images/coffee_bag_generic.png", Activo = true, Notas = new List<Note> { _mockNotes[3], _mockNotes[4], _mockNotes[5] } },
                new Product { Id = "WAY-RES", Nombre = "Waylan Reserve", Region = "Tolima, Colombia", Formato = "Grano", PerfilSabor = "Dulce", MetodoRecomendado = "Espresso", Intensidad = 5, Precio = 95000, Etiqueta = "EDICIÓN LIMITADA", ImagenUrl = "/images/coffee_bag_esperanza.png", Activo = true, Notas = new List<Note> { _mockNotes[6], _mockNotes[1] } }
            };

            _mockOrders = new List<Order>
            {
                new Order { Id = 1, Codigo = "PED-A7E1", Fecha = DateTime.Now.AddDays(-2), EmailCliente = "cliente@correo.com", Total = 130000, Estado = "Enviado" },
                new Order { Id = 2, Codigo = "PED-B9D4", Fecha = DateTime.Now, EmailCliente = "admin@waylan.com", Total = 72000, Estado = "Pendiente" }
            };

            _mockUsers = new List<User>
            {
                new User { Id = 1, Nombre = "Administrador Principal", Email = "admin@waylan.com", Rol = "Admin", Activo = true },
                new User { Id = 2, Nombre = "Juan Pérez", Email = "juan@correo.com", Rol = "Cliente", Activo = true }
            };
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
            try
            {
                // Matches AuthController [HttpPost("Login")] expecting UsuarioLoginRequestDto { Email, Password }
                var response = await _http.PostAsJsonAsync($"{ApiBaseUrl}api/Auth/Login", new { Email = email, Password = password });
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<LoginResponseDto>();
                    if (result != null && !string.IsNullOrEmpty(result.Token))
                    {
                        Token = result.Token;
                        // Decode name and role from token locally or mock for front
                        CurrentUser = new User { Email = email, Nombre = "Usuario Activo", Rol = email == "admin@waylan.com" ? "Admin" : "Cliente" };
                        SetAuthHeader();
                        OnAuthStateChanged?.Invoke();
                        return true;
                    }
                }
            }
            catch
            {
                // Fallback to Mock Auth if server is offline
                if ((email == "admin@waylan.com" && password == "admin123") || (email == "cliente@correo.com" && password == "cliente123"))
                {
                    Token = "MOCK-JWT-TOKEN";
                    CurrentUser = new User
                    {
                        Email = email,
                        Nombre = email == "admin@waylan.com" ? "Administrador Principal" : "Cliente Satisfecho",
                        Rol = email == "admin@waylan.com" ? "Admin" : "Cliente"
                    };
                    SetAuthHeader();
                    OnAuthStateChanged?.Invoke();
                    return true;
                }
            }
            return false;
        }

        public void Logout()
        {
            Token = null;
            CurrentUser = null;
            SetAuthHeader();
            OnAuthStateChanged?.Invoke();
        }

        public async Task<bool> RegistroAsync(string nombre, string email, string password)
        {
            try
            {
                // Matches AuthController [HttpPost("Registrar")] expecting UsuarioCreateDto { Nombre, Email, Password }
                var response = await _http.PostAsJsonAsync($"{ApiBaseUrl}api/Auth/Registrar", new { Nombre = nombre, Email = email, Password = password });
                return response.IsSuccessStatusCode;
            }
            catch
            {
                // In mock mode, add to local mock users list
                if (!_mockUsers.Any(u => u.Email == email))
                {
                    _mockUsers.Add(new User { Id = _mockUsers.Max(u => u.Id) + 1, Nombre = nombre, Email = email, Rol = "Cliente", Activo = false });
                }
                return true;
            }
        }

        public async Task<bool> VerificarEmailAsync(string email, string codigo)
        {
            try
            {
                // Matches AuthController [HttpPost("Verificacion-Email")] expecting Email and Codigo as Query params
                var response = await _http.PostAsync($"{ApiBaseUrl}api/Auth/Verificacion-Email?Email={Uri.EscapeDataString(email)}&Codigo={Uri.EscapeDataString(codigo)}", null);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                // In mock mode, activate the user
                var user = _mockUsers.FirstOrDefault(u => u.Email == email);
                if (user != null)
                {
                    user.Activo = true;
                }
                return true;
            }
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
                Tueste = dto.Tueste,
                Proceso = dto.Proceso,
                Descripcion = dto.Descripcion,
                Precio = dto.Precio,
                Stock = 0,
                ImagenUrl = dto.ImagenUrl,
                Activo = true,
                Notas = dto.Notas ?? new List<Note>(),
                
                // Fallbacks/Legacies
                Formato = dto.CategoriaNombre,
                Region = "Tolima, Colombia",
                PerfilSabor = dto.Tueste,
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
                Tueste = dto.Tueste,
                Proceso = dto.Proceso,
                Descripcion = dto.Descripcion,
                Precio = dto.Precio,
                Stock = dto.Stock,
                ImagenUrl = dto.ImagenUrl,
                Activo = dto.Activo,
                Notas = dto.Notas ?? new List<Note>(),
                
                // Fallbacks/Legacies
                Formato = dto.CategoriaNombre,
                Region = "Tolima, Colombia",
                PerfilSabor = dto.Tueste,
                MetodoRecomendado = "Filtrado",
                Intensidad = 3
            };
        }

        public async Task<List<Product>> GetProductosActivosAsync()
        {
            try
            {
                var dtos = await _http.GetFromJsonAsync<List<ProductoReadDto>>($"{ApiBaseUrl}api/Producto/Lista de productos");
                return dtos?.Select(MapToProduct).ToList() ?? new List<Product>();
            }
            catch
            {
                return _mockProducts.Where(p => p.Activo).ToList();
            }
        }

        public async Task<List<Product>> GetTodosProductosAsync()
        {
            try
            {
                SetAuthHeader();
                var dtos = await _http.GetFromJsonAsync<List<ProductoReadAdminDto>>($"{ApiBaseUrl}api/Producto/Lista de productos Admin");
                return dtos?.Select(MapToProduct).ToList() ?? new List<Product>();
            }
            catch
            {
                return _mockProducts;
            }
        }

        public async Task<Product?> GetProductoPorIdAsync(string id)
        {
            try
            {
                var dtos = await GetTodosProductosAsync();
                return dtos.FirstOrDefault(p => p.Id == id);
            }
            catch
            {
                return _mockProducts.FirstOrDefault(p => p.Id == id);
            }
        }

        public async Task<List<Note>> GetNotasAsync()
        {
            try
            {
                return await _http.GetFromJsonAsync<List<Note>>($"{ApiBaseUrl}api/Nota") ?? new List<Note>();
            }
            catch
            {
                return _mockNotes;
            }
        }

        public async Task<bool> CrearProductoAsync(MultipartFormDataContent content)
        {
            try
            {
                SetAuthHeader();
                var response = await _http.PostAsync($"{ApiBaseUrl}api/Producto", content);
                if (response.IsSuccessStatusCode)
                {
                    var created = await response.Content.ReadFromJsonAsync<ProductoReadAdminDto>();
                    if (created != null)
                    {
                        _mockProducts.Add(MapToProduct(created));
                    }
                    return true;
                }
                return false;
            }
            catch
            {
                var nombreContent = content.FirstOrDefault(c => c.Headers.ContentDisposition?.Name == "\"Nombre\"");
                var descContent = content.FirstOrDefault(c => c.Headers.ContentDisposition?.Name == "\"Descripcion\"");
                var precioContent = content.FirstOrDefault(c => c.Headers.ContentDisposition?.Name == "\"Precio\"");
                var stockContent = content.FirstOrDefault(c => c.Headers.ContentDisposition?.Name == "\"Stock\"");
                var catContent = content.FirstOrDefault(c => c.Headers.ContentDisposition?.Name == "\"IdCategoria\"");
                
                var mockCategoryName = "Grano";
                if (catContent != null)
                {
                    var catId = int.Parse(await catContent.ReadAsStringAsync());
                    var catObj = _mockCategories.FirstOrDefault(c => c.Id == catId);
                    if (catObj != null) mockCategoryName = catObj.Nombre;
                }

                var newProduct = new Product
                {
                    Id = (_mockProducts.Count + 1).ToString(),
                    Nombre = nombreContent != null ? await nombreContent.ReadAsStringAsync() : "Café Premium Mock",
                    Descripcion = descContent != null ? await descContent.ReadAsStringAsync() : "Café Premium",
                    Precio = precioContent != null ? decimal.Parse(await precioContent.ReadAsStringAsync()) : 45000,
                    Stock = stockContent != null ? int.Parse(await stockContent.ReadAsStringAsync()) : 50,
                    IdCategoria = catContent != null ? int.Parse(await catContent.ReadAsStringAsync()) : 1,
                    CategoriaNombre = mockCategoryName,
                    Formato = mockCategoryName,
                    ImagenUrl = "/images/coffee_bag_generic.png",
                    Activo = true
                };

                _mockProducts.Add(newProduct);
                return true;
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
                    var updated = await response.Content.ReadFromJsonAsync<ProductoReadAdminDto>();
                    if (updated != null)
                    {
                        var idx = _mockProducts.FindIndex(p => p.Id == id);
                        if (idx >= 0)
                        {
                            _mockProducts[idx] = MapToProduct(updated);
                        }
                    }
                    return true;
                }
                return false;
            }
            catch
            {
                var prod = _mockProducts.FirstOrDefault(p => p.Id == id);
                if (prod != null)
                {
                    var nombreContent = content.FirstOrDefault(c => c.Headers.ContentDisposition?.Name == "\"Nombre\"");
                    var descContent = content.FirstOrDefault(c => c.Headers.ContentDisposition?.Name == "\"Descripcion\"");
                    var precioContent = content.FirstOrDefault(c => c.Headers.ContentDisposition?.Name == "\"Precio\"");
                    var stockContent = content.FirstOrDefault(c => c.Headers.ContentDisposition?.Name == "\"Stock\"");

                    if (nombreContent != null) prod.Nombre = await nombreContent.ReadAsStringAsync();
                    if (descContent != null) prod.Descripcion = await descContent.ReadAsStringAsync();
                    if (precioContent != null) prod.Precio = decimal.Parse(await precioContent.ReadAsStringAsync());
                    if (stockContent != null) prod.Stock = int.Parse(await stockContent.ReadAsStringAsync());
                }
                return true;
            }
        }

        public async Task<bool> CambiarEstadoProductoAsync(string id, bool nuevoEstado)
        {
            try
            {
                SetAuthHeader();
                var response = await _http.PatchAsync($"{ApiBaseUrl}api/Producto/{id}/cambiar-estado?nuevoEstado={nuevoEstado}", null);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                var prod = _mockProducts.FirstOrDefault(p => p.Id == id);
                if (prod != null) prod.Activo = nuevoEstado;
                return true;
            }
        }

        // --- CATEGORIAS ---
        public async Task<List<Category>> GetCategoriasActivasAsync()
        {
            try
            {
                return await _http.GetFromJsonAsync<List<Category>>($"{ApiBaseUrl}api/categorias/activas") ?? new List<Category>();
            }
            catch
            {
                return _mockCategories.Where(c => c.Activo).ToList();
            }
        }

        public async Task<List<Category>> GetTodasCategoriasAsync()
        {
            try
            {
                SetAuthHeader();
                return await _http.GetFromJsonAsync<List<Category>>($"{ApiBaseUrl}api/categorias") ?? new List<Category>();
            }
            catch
            {
                return _mockCategories;
            }
        }

        public async Task<bool> CrearCategoriaAsync(string nombre)
        {
            try
            {
                SetAuthHeader();
                var response = await _http.PostAsJsonAsync($"{ApiBaseUrl}api/categorias", new { Nombre = nombre });
                return response.IsSuccessStatusCode;
            }
            catch
            {
                _mockCategories.Add(new Category { Id = _mockCategories.Max(c => c.Id) + 1, Nombre = nombre, Activo = true });
                return true;
            }
        }

        public async Task<bool> ActualizarCategoriaAsync(int id, string nombre)
        {
            try
            {
                SetAuthHeader();
                var response = await _http.PutAsJsonAsync($"{ApiBaseUrl}api/categorias/{id}", new { Nombre = nombre });
                return response.IsSuccessStatusCode;
            }
            catch
            {
                var cat = _mockCategories.FirstOrDefault(c => c.Id == id);
                if (cat != null) cat.Nombre = nombre;
                return true;
            }
        }

        public async Task<bool> CambiarEstadoCategoriaAsync(int id)
        {
            try
            {
                SetAuthHeader();
                var response = await _http.PatchAsync($"{ApiBaseUrl}api/categorias/{id}/cambiar-estado", null);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                var cat = _mockCategories.FirstOrDefault(c => c.Id == id);
                if (cat != null) cat.Activo = !cat.Activo;
                return true;
            }
        }

        // --- PEDIDOS ---
        public async Task<bool> CrearPedidoAsync(List<CartItemDto> items)
        {
            try
            {
                SetAuthHeader();
                var response = await _http.PostAsJsonAsync($"{ApiBaseUrl}api/pedidos", new { Items = items });
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return true;
            }
        }

        public async Task<List<Order>> GetTodosPedidosAsync()
        {
            try
            {
                SetAuthHeader();
                return await _http.GetFromJsonAsync<List<Order>>($"{ApiBaseUrl}api/pedidos") ?? new List<Order>();
            }
            catch
            {
                return _mockOrders;
            }
        }

        public async Task<bool> CambiarEstadoPedidoAsync(string codigo, string estado)
        {
            try
            {
                SetAuthHeader();
                var response = await _http.PutAsJsonAsync($"{ApiBaseUrl}api/pedidos/{codigo}/estado", new { Estado = estado });
                return response.IsSuccessStatusCode;
            }
            catch
            {
                var ord = _mockOrders.FirstOrDefault(o => o.Codigo == codigo);
                if (ord != null) ord.Estado = estado;
                return true;
            }
        }

        // --- USUARIOS ---
        public async Task<List<User>> GetUsuariosAsync()
        {
            try
            {
                SetAuthHeader();
                return await _http.GetFromJsonAsync<List<User>>($"{ApiBaseUrl}api/usuarios") ?? new List<User>();
            }
            catch
            {
                return _mockUsers;
            }
        }

        public async Task<bool> CambiarEstadoUsuarioAsync(int id)
        {
            try
            {
                SetAuthHeader();
                var response = await _http.PatchAsync($"{ApiBaseUrl}api/usuarios/{id}/cambiar-estado", null);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                var usr = _mockUsers.FirstOrDefault(u => u.Id == id);
                if (usr != null) usr.Activo = !usr.Activo;
                return true;
            }
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
}
