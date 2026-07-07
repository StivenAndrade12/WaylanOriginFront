namespace WaylanOrigin.Client.Models
{
    public class Product
    {
        public string Id { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string Formato { get; set; } = string.Empty; // Grano, Molido
        public string PerfilSabor { get; set; } = string.Empty; // Dulce, Balanceado, Ácido
        public string MetodoRecomendado { get; set; } = string.Empty; // Filtrado, Espresso, Prensa francesa, Cold brew
        public int Intensidad { get; set; }
        public int Precio { get; set; }
        public string Etiqueta { get; set; } = string.Empty; // NUEVO, MÁS VENDIDO, EDICIÓN LIMITADA
        public string ImagenUrl { get; set; } = string.Empty;
        public bool Activo { get; set; } = true;
        public bool IsFavorite { get; set; }

        public List<Note> Notas { get; set; } = new List<Note>();
    }
}
