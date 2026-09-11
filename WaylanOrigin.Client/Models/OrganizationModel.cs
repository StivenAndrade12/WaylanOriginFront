namespace WaylanOrigin.Client.Models;


public class OrganizationModel
{
    public int  Id { get; set; }              // "famycafe", "tres-nevados"
    public string Nombre { get; set; }          // FAMYCAFE, Tres Nevados

    // HERO
    public string ImagenLogo { get; set; }            // logotipo/5.png
    public string HeroImagen { get; set; }      // imagenes/camp.png

    // TEXTOS DEL HERO
    public string Descripcion1 { get; set; }
    public string Descripcion2 { get; set; }

    // ESTADÍSTICAS
    
    public string Enfoque { get; set; }

    // PRODUCTORES DESTACADOS (máx 4)
    public List<ProductorModel> Productores { get; set; } = new();
    
}