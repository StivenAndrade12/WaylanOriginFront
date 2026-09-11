namespace WaylanOrigin.Client.Models;
using System.Collections.Generic;

public class ProductorModel
{
    public int Id { get; set; }
    public int IdOrganizacion { get; set; }
    public bool Destacado { get; set; }

    public string Nombre { get; set; } = string.Empty;
    public string ImagenPrincipal { get; set; } = string.Empty;
    public string Frase { get; set; } = string.Empty;

    public string HistoriaTitulo { get; set; } = string.Empty;
    public string HistoriaTexto { get; set; } = string.Empty;
    public string SostenibilidadDescripcion { get; set; } = string.Empty;

    // Relaciones de Entity Framework
    public OrganizationModel? Organizacion { get; set; }
    public List<ProcedimientoModel> Procedimientos { get; set; } = new();
}

