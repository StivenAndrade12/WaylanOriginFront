namespace WaylanOrigin.Client.Models;
using System.Collections.Generic;

public class ProductorModel
{
    public string Id { get; set; }
    public string Nombre { get; set; }
    public string Finca { get; set; }
    public string Ubicacion { get; set; }
    public string Frase { get; set; }

    public string HistoriaTitulo { get; set; }
    public string HistoriaTexto { get; set; }

    public string Altitud { get; set; }
    public string Proceso { get; set; }
    public string Variedades { get; set; }
    public string Experiencia { get; set; }

    public string ImagenPrincipal { get; set; }
    public List<string> Galeria { get; set; }

    public string FincaDescripcion { get; set; }
    public string FincaImagen { get; set; }

    // PROCESOS
    public string ProcesosDescripcion { get; set; }
    public string SostenibilidadDescripcion { get; set; }
    public string ProcesosFooterTexto { get; set; }

    public List<ProcesoModel> Procesos { get; set; }
}

public class ProcesoModel
{
    public int Numero { get; set; }
    public string Titulo { get; set; }
    public string Descripcion { get; set; }
    public string Imagen { get; set; }
}
