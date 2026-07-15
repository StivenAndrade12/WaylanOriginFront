
namespace WaylanOrigin.Client.Models;
using System.Collections.Generic;

public class ProductorModel
{
    public string Id { get; set; }              // "juan", "maria", etc.
    public string Nombre { get; set; }         // Juan Carlos Restrepo
    public string Finca { get; set; }          // Finca El Paraíso
    public string Ubicacion { get; set; }      // Caldas, Colombia
    public string Frase { get; set; }          // Más de 25 años...

    public string HistoriaTitulo { get; set; } // Nuestra historia
    public string HistoriaTexto { get; set; }  // Párrafo de historia

    public string Altitud { get; set; }        // 1.850 msnm
    public string Proceso { get; set; }        // Lavado
    public string Variedades { get; set; }     // Caturra, Castillo
    public string Experiencia { get; set; }    // 25+ años

    public string ImagenPrincipal { get; set; } // /imagenes/productores/juan-main.png
    public List<string> Galeria { get; set; }   // Rutas de imágenes de galería
}
