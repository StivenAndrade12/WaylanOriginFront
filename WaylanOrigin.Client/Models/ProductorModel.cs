namespace WaylanOrigin.Client.Models;

using System.Collections.Generic;
using System.Text.Json.Serialization;

public class ProductorModel
{
    private string? _imagenUrl;
    private string? _imagenPrincipal;
    private string? _historia;
    private string? _historiaTexto;

    // Nombres exactos de la entidad C# del backend (PascalCase)
   
    public int Id { get; set; }

    [JsonPropertyName("IdOrganizacion")]
    public int IdOrganizacion { get; set; }

    [JsonPropertyName("Nombre")]
    public string Nombre { get; set; } = string.Empty;

    [JsonPropertyName("Destacado")]
    public bool Destacado { get; set; }

    [JsonPropertyName("Frase")]
    public string? Frase { get; set; }

    [JsonPropertyName("HistoriaTitulo")]
    public string? HistoriaTitulo { get; set; }

    [JsonPropertyName("HistoriaTexto")]
    public string? HistoriaTexto
    {
        get => !string.IsNullOrEmpty(_historiaTexto) ? _historiaTexto : _historia;
        set => _historiaTexto = value;
    }

    [JsonPropertyName("Historia")]
    public string? Historia
    {
        get => !string.IsNullOrEmpty(_historia) ? _historia : _historiaTexto;
        set => _historia = value;
    }

    [JsonPropertyName("SostenibilidadDescripcion")]
    public string? SostenibilidadDescripcion { get; set; }

    [JsonPropertyName("ImagenPrincipal")]
    public string? ImagenPrincipal
    {
        get => !string.IsNullOrEmpty(_imagenPrincipal) ? _imagenPrincipal : _imagenUrl;
        set => _imagenPrincipal = value;
    }

    [JsonPropertyName("ImagenUrl")]
    public string? ImagenUrl
    {
        get => !string.IsNullOrEmpty(_imagenUrl) ? _imagenUrl : _imagenPrincipal;
        set => _imagenUrl = value;
    }

    [JsonPropertyName("OrganizacionNombre")]
    public string? OrganizacionNombre { get; set; }

    [JsonPropertyName("Ubicacion")]
    public string? Ubicacion { get; set; }

    // Relaciones
    [JsonPropertyName("Organizacion")]
    public OrganizationModel? Organizacion { get; set; }

    [JsonPropertyName("Procedimientos")]
    public List<ProcedimientoModel> Procedimientos { get; set; } = new();
}