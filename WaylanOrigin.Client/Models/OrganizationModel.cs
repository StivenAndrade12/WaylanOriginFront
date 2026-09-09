namespace WaylanOrigin.Client.Models;


public class OrganizationModel
{
    public string Id { get; set; }              // "famycafe", "tres-nevados"
    public string Nombre { get; set; }          // FAMYCAFE, Tres Nevados

    // HERO
    public string Logo { get; set; }            // logotipo/5.png
    public string HeroImagen { get; set; }      // imagenes/camp.png

    // TEXTOS DEL HERO
    public string Descripcion1 { get; set; }
    public string Descripcion2 { get; set; }

    // ESTADÍSTICAS
    public string AnoFundacion { get; set; }
    public string Familias { get; set; }
    public string Hectareas { get; set; }
    public string Enfoque { get; set; }

    // PRODUCTORES DESTACADOS (máx 4)
    public List<string> DestacadosIds { get; set; } = new();
}