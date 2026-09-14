namespace WaylanOrigin.Client.Models;

using System.Text.Json.Serialization;

public class OrganizationModel
{
    public int Id { get; set; }
    
    public string Nombre { get; set; } = string.Empty;

    // Mapea tanto el JSON de respuesta ("imagenLogo") como el nombre original
    [JsonPropertyName("imagenLogo")]
    public string ImagenLogo { get; set; } = string.Empty;

    // Mapea la clave "imagenHero" que devuelve el servidor al deserializar el JSON
    [JsonPropertyName("imagenHero")]
    public string HeroImagen { get; set; } = string.Empty;

    public string Descripcion1 { get; set; } = string.Empty;
    public string Descripcion2 { get; set; } = string.Empty;
    public string Enfoque { get; set; } = string.Empty;

    public List<ProductorModel> Productores { get; set; } = new();
}