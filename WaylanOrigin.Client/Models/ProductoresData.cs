namespace WaylanOrigin.Client.Models;
using System.Collections.Generic;

public static class ProductoresData
{
    public static List<ProductorModel> Lista = new()
    {
        new ProductorModel
        {
            Id = "juan",
            Nombre = "Juan Carlos Restrepo",
            Finca = "Finca El Paraíso",
            Ubicacion = "Caldas, Colombia",
            Frase = "Más de 25 años cultivando tradición y pasión por el café.",

            HistoriaTitulo = "Nuestra historia",
            HistoriaTexto = "Mi historia con el café comenzó desde niño, viendo a mi abuelo trabajar en la finca. Hoy, junto a mi familia, seguimos cultivando cafés especiales con prácticas sostenibles que reflejan nuestro entorno y la esencia de nuestra región.",

            Altitud = "1.850 msnm",
            Proceso = "Lavado",
            Variedades = "Caturra, Castillo",
            Experiencia = "25+ años de experiencia",

            // Rutas de imágenes (luego tú las cambias)
            ImagenPrincipal = "/productores/ban.png",
            Galeria = new List<string>
            {
                "/imagenes/productores/juan-g1.png",
                "/imagenes/productores/juan-g2.png",
                "/imagenes/productores/juan-g3.png",
                "/imagenes/productores/juan-g4.png"
            }
        },

        // Aquí luego agregas a María, Carlos, Elena, etc.
    };
}
