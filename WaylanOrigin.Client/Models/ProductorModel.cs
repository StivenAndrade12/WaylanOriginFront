namespace WaylanOrigin.Client.Models;

using System.Collections.Generic;
using System.Text.Json.Serialization;

public class ProductorModel
{
    private string? _imagenUrl;
    private string? _imagenPrincipal;
    private string? _historia;
    private string? _historiaTexto;

    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("idOrganizacion")]
    public int IdOrganizacion { get; set; }

    [JsonPropertyName("nombre")]
    public string Nombre { get; set; } = string.Empty;

    [JsonPropertyName("ubicacion")]
    public string? Ubicacion { get; set; }

    [JsonPropertyName("historia")]
    public string? Historia
    {
        get => !string.IsNullOrEmpty(_historia) ? _historia : _historiaTexto;
        set => _historia = value;
    }

    [JsonPropertyName("imagenUrl")]
    public string? ImagenUrl
    {
        get => !string.IsNullOrEmpty(_imagenUrl) ? _imagenUrl : _imagenPrincipal;
        set => _imagenUrl = value;
    }

    // Campos de compatibilidad con Swagger/Azure DTOs y vistas existentes
    [JsonPropertyName("destacado")]
    public bool Destacado { get; set; }

    [JsonPropertyName("frase")]
    public string? Frase { get; set; }

    [JsonPropertyName("historiaTitulo")]
    public string? HistoriaTitulo { get; set; }

    [JsonPropertyName("historiaTexto")]
    public string? HistoriaTexto
    {
        get => !string.IsNullOrEmpty(_historiaTexto) ? _historiaTexto : _historia;
        set => _historiaTexto = value;
    }

    [JsonPropertyName("sostenibilidadDescripcion")]
    public string? SostenibilidadDescripcion { get; set; }

    [JsonPropertyName("imagenPrincipal")]
    public string? ImagenPrincipal
    {
        get => !string.IsNullOrEmpty(_imagenPrincipal) ? _imagenPrincipal : _imagenUrl;
        set => _imagenPrincipal = value;
    }

    [JsonPropertyName("organizacionNombre")]
    public string? OrganizacionNombre { get; set; }

    // Relaciones
    public OrganizationModel? Organizacion { get; set; }

    [JsonPropertyName("procedimientos")]
    public List<ProcedimientoModel> Procedimientos { get; set; } = new();
}


