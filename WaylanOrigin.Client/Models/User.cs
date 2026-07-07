namespace WaylanOrigin.Client.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Rol { get; set; } = "Cliente";
        public bool Activo { get; set; } = true;
    }
}
