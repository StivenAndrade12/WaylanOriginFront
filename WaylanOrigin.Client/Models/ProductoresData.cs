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

        new ProductorModel
{
    Id = "jose",
    Nombre = "José Ramírez",
    Finca = "Finca Alto Nevado",
    Ubicacion = "Tolima, Colombia",
    Frase = "Café de altura cultivado con dedicación y respeto por la montaña.",

    HistoriaTitulo = "Nuestra historia",
    HistoriaTexto = "Desde joven he trabajado en las laderas del Nevado del Tolima, aprendiendo de mi familia las técnicas tradicionales de cultivo. Con el paso de los años, hemos incorporado prácticas sostenibles que protegen la tierra y mejoran la calidad de nuestro café. Hoy, seguimos comprometidos con producir granos de origen que reflejan la esencia de nuestra región.",

    Altitud = "1.900 msnm",
    Proceso = "Lavado",
    Variedades = "Caturra, Colombia",
    Experiencia = "20+ años de experiencia",

    ImagenPrincipal = "/productores/jose-banner.png",

    Galeria = new List<string>
    {
        "/imagenes/productores/jose-g1.png",
        "/imagenes/productores/jose-g2.png",
        "/imagenes/productores/jose-g3.png",
        "/imagenes/productores/jose-g4.png"
    }
},

    };

    
}
