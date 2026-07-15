namespace WaylanOrigin.Client.Models
{
    public class Product
    {
        public string Id { get; set; } = string.Empty; // String representation of the ID (maps to int for backend)
        public string Nombre { get; set; } = string.Empty;
        
        // Backend aligned fields
        public int IdCategoria { get; set; }
        public string CategoriaNombre { get; set; } = string.Empty;
        public string Tueste { get; set; } = string.Empty; // Claro, Medio, Oscuro
        public string Proceso { get; set; } = string.Empty; // Lavado, Natural, Honey
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string ImagenUrl { get; set; } = string.Empty;
        public bool Activo { get; set; } = true;
        
        // Legacy frontend fields / Fallbacks
        public string Region { get; set; } = string.Empty;
        public string Formato { get; set; } = string.Empty; // "Grano", "Molido"
        public string PerfilSabor { get; set; } = string.Empty; // "Dulce", "Balanceado", "Ácido"
        public string MetodoRecomendado { get; set; } = string.Empty;
        public int Intensidad { get; set; } = 3;
        public string Etiqueta { get; set; } = string.Empty;
        public bool IsFavorite { get; set; }

        public List<Note> Notas { get; set; } = new List<Note>();
    }
}
