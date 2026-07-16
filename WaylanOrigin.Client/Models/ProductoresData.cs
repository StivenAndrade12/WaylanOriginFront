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
    HistoriaTexto = "Mi historia con el café comenzó desde niño...",

    Altitud = "1.850 msnm",
    Proceso = "Lavado",
    Variedades = "Caturra, Castillo",
    Experiencia = "25+ años de experiencia",

    FincaDescripcion = "Finca El Paraíso está ubicada...",
    FincaImagen = "/fincas/finca-juan.png",

    ProcesosDescripcion = "Nuestros procesos incluyen recolección manual...",
    SostenibilidadDescripcion = "Trabajamos con prácticas sostenibles...",
    ProcesosFooterTexto = "Conoce más sobre nuestros estándares de calidad.",

    Procesos = new List<ProcesoModel>
    {
        new ProcesoModel
        {
            Numero = 1,
            Titulo = "Recolección",
            Descripcion = "Seleccionamos solo los frutos maduros.",
            Imagen = "/procesos/juan-p1.png"
        },
        new ProcesoModel
        {
            Numero = 2,
            Titulo = "Despulpado",
            Descripcion = "El café se despulpa el mismo día.",
            Imagen = "/procesos/juan-p2.png"
        },
        new ProcesoModel
        {
            Numero = 3,
            Titulo = "Fermentación",
            Descripcion = "Fermentaciones controladas para mejorar el perfil.",
            Imagen = "/procesos/juan-p3.png"
        },
        new ProcesoModel
        {
            Numero = 4,
            Titulo = "Secado",
            Descripcion = "Secado en camas africanas durante varios días.",
            Imagen = "/procesos/juan-p4.png"
        },
        new ProcesoModel
        {
            Numero = 5,
            Titulo = "Almacenamiento",
            Descripcion = "El café se guarda en bodegas con humedad controlada.",
            Imagen = "/procesos/juan-p5.png"
        }
    },

    ImagenPrincipal = "/productores/bannerj.png",
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

    FincaDescripcion = "Finca Alto Nevado se encuentra en las laderas del Nevado del Tolima, donde el clima frío y la altura permiten desarrollar cafés complejos y aromáticos.",
    FincaImagen = "/fincas/finca-jose.png",

    ProcesosDescripcion = "Implementamos procesos de beneficio ecológico, secado en camas africanas y fermentaciones naturales para resaltar los sabores de la montaña.",

    // NUEVO — SOSTENIBILIDAD
    SostenibilidadDescripcion = "Trabajamos con métodos ecológicos que reducen el consumo de agua y protegen la biodiversidad del Nevado del Tolima.",

    // NUEVO — TEXTO DEL BANNER INFERIOR
    ProcesosFooterTexto = "Descubre cómo transformamos el café desde la montaña hasta tu taza.",

    // NUEVO — LISTA DE PROCESOS
    Procesos = new List<ProcesoModel>
    {
        new ProcesoModel
        {
            Numero = 1,
            Titulo = "Recolección manual",
            Descripcion = "Seleccionamos únicamente los frutos maduros para garantizar calidad.",
            Imagen = "/procesos/jose-p1.png"
        },
        new ProcesoModel
        {
            Numero = 2,
            Titulo = "Despulpado ecológico",
            Descripcion = "Usamos sistemas de bajo consumo de agua para proteger el entorno.",
            Imagen = "/procesos/jose-p2.png"
        },
        new ProcesoModel
        {
            Numero = 3,
            Titulo = "Fermentación natural",
            Descripcion = "Fermentaciones controladas que resaltan notas dulces y frutales.",
            Imagen = "/procesos/jose-p3.png"
        },
        new ProcesoModel
        {
            Numero = 4,
            Titulo = "Secado en camas africanas",
            Descripcion = "Secado lento que estabiliza el grano y mejora la calidad.",
            Imagen = "/procesos/jose-p4.png"
        },
        new ProcesoModel
        {
            Numero = 5,
            Titulo = "Almacenamiento en bodegas frías",
            Descripcion = "Conservamos el café en condiciones óptimas antes del trillado.",
            Imagen = "/procesos/jose-p5.png"
        }
    },

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
