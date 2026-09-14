namespace WaylanOrigin.Client.Models;

using System.Collections.Generic;
using System.Text.Json.Serialization;

public class OrganizationModel
{
    public int Id { get; set; }

    [JsonPropertyName("nombre")]
    public string Nombre { get; set; } = string.Empty;

    [JsonPropertyName("imagenLogo")]
    public string? ImagenLogo { get; set; }

    [JsonPropertyName("imagenHero")]
    public string? HeroImagen { get; set; }

    [JsonPropertyName("descripcion1")]
    public string? Descripcion1 { get; set; }

    [JsonPropertyName("descripcion2")]
    public string? Descripcion2 { get; set; }

    [JsonPropertyName("enfoque")]
    public string? Enfoque { get; set; }

    [JsonPropertyName("productoresDestacados")]
    public List<ProductorModel> Productores { get; set; } = new();
}