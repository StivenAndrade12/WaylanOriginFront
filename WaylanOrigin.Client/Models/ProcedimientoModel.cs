namespace WaylanOrigin.Client.Models;

public class ProcedimientoModel
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Titulo { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public string Imagen { get; set; } = string.Empty;

    public int IdProductor { get; set; }
    public ProductorModel? Productor { get; set; }
}