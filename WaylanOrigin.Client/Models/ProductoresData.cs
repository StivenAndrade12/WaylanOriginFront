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
            HistoriaTexto = "Mi historia con el café comenzó desde niño en las montañas de Caldas. Heredé de mis padres la pasión por la tierra y el respeto por cada cultivo. Durante más de 25 años he trabajado por perfeccionar nuestros procesos, cuidando desde la floración hasta la recolección manual de cada grano.",

            Altitud = "1.850 msnm",
            Proceso = "Lavado",
            Variedades = "Caturra, Castillo",
            Experiencia = "25+ años de experiencia",

            FincaDescripcion = "Finca El Paraíso está ubicada en las laderas de Caldas, rodeada de bosques nativos y fuentes de agua pura. Su microclima particular permite un desarrollo lento y complejo de los granos.",
            FincaImagen = "imagenes/pro.png",

            ProcesosDescripcion = "Nuestros procesos incluyen recolección manual selectiva, despulpado ecológico y secado al sol en marquesinas elevadas.",
            SostenibilidadDescripcion = "Trabajamos con prácticas sostenibles que protegen la biodiversidad, conservan las fuentes hídricas y reducen la huella ambiental.",
            ProcesosFooterTexto = "Conoce más sobre nuestros estándares de calidad y compromiso con la excelencia.",

            Procesos = new List<ProcesoModel>
            {
                new ProcesoModel { Numero = 1, Titulo = "Recolección", Descripcion = "Seleccionamos únicamente los frutos en su punto óptimo de madurez.", Imagen = "imagenes/linea1.png" },
                new ProcesoModel { Numero = 2, Titulo = "Despulpado", Descripcion = "El café se despulpa el mismo día usando tecnología de bajo consumo de agua.", Imagen = "imagenes/linea2.png" },
                new ProcesoModel { Numero = 3, Titulo = "Fermentación", Descripcion = "Fermentaciones naturales controladas durante 18 a 24 horas.", Imagen = "imagenes/linea3.png" },
                new ProcesoModel { Numero = 4, Titulo = "Secado", Descripcion = "Secado lento al sol en camas elevadas para estabilizar la humedad.", Imagen = "imagenes/maduro.png" },
                new ProcesoModel { Numero = 5, Titulo = "Almacenamiento", Descripcion = "Conservado en sacos especiales que protegen su aroma y frescura.", Imagen = "imagenes/taza.png" }
            },

            ImagenPrincipal = "productores/bannerj.png",
            Galeria = new List<string>
            {
                "imagenes/linea1.png",
                "imagenes/manoscafe.png",
                "imagenes/maduro.png",
                "imagenes/taza.png"
            }
        },

        new ProductorModel
        {
            Id = "jose",
            Nombre = "José Ramírez",
            Finca = "Finca Alto Nevado",
            Ubicacion = "Tolima, Colombia",
            Frase = "Café de altura cultivado con dedicación y respeto por la montaña.",

            HistoriaTitulo = "Nuestra historia",
            HistoriaTexto = "Desde joven he trabajado en las laderas del Nevado del Tolima, aprendiendo de mi familia las técnicas tradicionales de cultivo. Con el paso de los años, hemos incorporado prácticas sostenibles que protegen la tierra y mejoran la calidad de nuestro café.",

            Altitud = "1.900 msnm",
            Proceso = "Lavado",
            Variedades = "Caturra, Colombia",
            Experiencia = "20+ años de experiencia",

            FincaDescripcion = "Finca Alto Nevado se encuentra en las laderas del Nevado del Tolima, donde el clima frío y la altura permiten desarrollar cafés complejos y aromáticos.",
            FincaImagen = "imagenes/espalda.png",

            ProcesosDescripcion = "Implementamos procesos de beneficio ecológico, secado en camas africanas y fermentaciones naturales para resaltar los sabores de la montaña.",
            SostenibilidadDescripcion = "Trabajamos con métodos ecológicos que reducen el consumo de agua y protegen la biodiversidad del Nevado del Tolima.",
            ProcesosFooterTexto = "Descubre cómo transformamos el café desde la montaña hasta tu taza.",

            Procesos = new List<ProcesoModel>
            {
                new ProcesoModel { Numero = 1, Titulo = "Recolección manual", Descripcion = "Seleccionamos únicamente los frutos maduros para garantizar calidad.", Imagen = "imagenes/linea1.png" },
                new ProcesoModel { Numero = 2, Titulo = "Despulpado ecológico", Descripcion = "Usamos sistemas de bajo consumo de agua para proteger el entorno.", Imagen = "imagenes/linea2.png" },
                new ProcesoModel { Numero = 3, Titulo = "Fermentación natural", Descripcion = "Fermentaciones controladas que resaltan notas dulces y frutales.", Imagen = "imagenes/linea3.png" },
                new ProcesoModel { Numero = 4, Titulo = "Secado en camas", Descripcion = "Secado lento que estabiliza el grano y mejora la calidad.", Imagen = "imagenes/maduro.png" },
                new ProcesoModel { Numero = 5, Titulo = "Almacenamiento", Descripcion = "Conservamos el café en condiciones óptimas antes del trillado.", Imagen = "imagenes/taza.png" }
            },

            ImagenPrincipal = "imagenes/camp.png",
            Galeria = new List<string>
            {
                "imagenes/linea2.png",
                "imagenes/espalda.png",
                "imagenes/maduro.png",
                "imagenes/taza.png"
            }
        },

        new ProductorModel
        {
            Id = "maria",
            Nombre = "María Elvira Gómez",
            Finca = "Finca La Esperanza",
            Ubicacion = "Antioquia, Colombia",
            Frase = "Liderazgo femenino y pasión por el café de alta especialidad.",

            HistoriaTitulo = "Nuestra historia",
            HistoriaTexto = "Como mujer caficultora, he liderado la transformación de nuestra finca familiar hacia la producción de café de especialidad con certificación orgánica.",

            Altitud = "1.800 msnm",
            Proceso = "Honey",
            Variedades = "Geisha, Bourbon Rosado",
            Experiencia = "18+ años de experiencia",

            FincaDescripcion = "Finca La Esperanza cuenta con microclimas privilegiados en las montañas antioqueñas que producen notas frutales exóticas.",
            FincaImagen = "imagenes/2f.png",

            ProcesosDescripcion = "Especialistas en procesos Honey y Anaeróbicos con control de temperatura estricto.",
            SostenibilidadDescripcion = "Empoderamiento comunitario y cultivo libre de agroquímicos.",
            ProcesosFooterTexto = "Compromiso con la innovación constante en perfiles de taza.",

            Procesos = new List<ProcesoModel>
            {
                new ProcesoModel { Numero = 1, Titulo = "Recolección Geisha", Descripcion = "Cosecha selectiva grano a grano.", Imagen = "imagenes/linea1.png" },
                new ProcesoModel { Numero = 2, Titulo = "Proceso Honey", Descripcion = "Despulpado conservando el mucílago natural.", Imagen = "imagenes/linea2.png" },
                new ProcesoModel { Numero = 3, Titulo = "Secado controlado", Descripcion = "Secado en marquesinas bajo sombra parcial.", Imagen = "imagenes/maduro.png" }
            },

            ImagenPrincipal = "imagenes/1f.png",
            Galeria = new List<string>
            {
                "imagenes/3f.png",
                "imagenes/4f.png",
                "imagenes/5f.png",
                "imagenes/6f.png"
            }
        },

        new ProductorModel
        {
            Id = "carlos",
            Nombre = "Carlos Alberto Ruiz",
            Finca = "Finca La Montaña",
            Ubicacion = "Cauca, Colombia",
            Frase = "Suelos volcánicos que dan vida a un perfil acaramelado único.",

            HistoriaTitulo = "Nuestra historia",
            HistoriaTexto = "Nuestra finca aprovecha los ricos suelos volcánicos de la cordillera del Cauca, produciendo un café reconocido internacionalmente.",

            Altitud = "1.950 msnm",
            Proceso = "Lavado",
            Variedades = "Castillo, Tabi",
            Experiencia = "22+ años de experiencia",

            FincaDescripcion = "Ubicada en laderas volcánicas con alta biodiversidad y ríos de montaña puros.",
            FincaImagen = "imagenes/4f.png",

            ProcesosDescripcion = "Beneficio tradicional perfeccionado con tecnología de fermentación en tanques de acero.",
            SostenibilidadDescripcion = "Protección de nacimientos de agua y reforestación con árboles nativos.",
            ProcesosFooterTexto = "Tradición cafetera del sur de Colombia en cada grano.",

            Procesos = new List<ProcesoModel>
            {
                new ProcesoModel { Numero = 1, Titulo = "Selección densimétrica", Descripcion = "Clasificación de granos por densidad en agua.", Imagen = "imagenes/linea1.png" },
                new ProcesoModel { Numero = 2, Titulo = "Fermentación extendida", Descripcion = "Fermentación de 36 horas a baja temperatura.", Imagen = "imagenes/linea3.png" },
                new ProcesoModel { Numero = 3, Titulo = "Secado al sol", Descripcion = "Secado en patios tradicionales.", Imagen = "imagenes/taza.png" }
            },

            ImagenPrincipal = "imagenes/3f.png",
            Galeria = new List<string>
            {
                "imagenes/5f.png",
                "imagenes/6f.png",
                "imagenes/8f.png",
                "imagenes/pro.png"
            }
        },

        new ProductorModel
        {
            Id = "elena",
            Nombre = "Elena Mendoza",
            Finca = "Finca Vista Hermosa",
            Ubicacion = "Tolima, Colombia",
            Frase = "Cuidando el ecosistema para cosechar tazas inolvidables.",

            HistoriaTitulo = "Nuestra historia",
            HistoriaTexto = "Tercera generación de caficultores en el Tolima enfocados en preservar el bosque nativo alrededor de nuestros cafetales.",

            Altitud = "1.820 msnm",
            Proceso = "Natural",
            Variedades = "Bourbon Rojo, Caturra",
            Experiencia = "15+ años de experiencia",

            FincaDescripcion = "Vista Hermosa se ubica en un mirador natural del Tolima rodeado de aves y vegetación nativa.",
            FincaImagen = "imagenes/6f.png",

            ProcesosDescripcion = "Procesos naturales secados en fruto entero para máxima dulzura.",
            SostenibilidadDescripcion = "Conservación de corredores biológicos para aves migratorias.",
            ProcesosFooterTexto = "Sostenibilidad ambiental garantizada.",

            Procesos = new List<ProcesoModel>
            {
                new ProcesoModel { Numero = 1, Titulo = "Cosecha súper madura", Descripcion = "Selección estricta de frutos con alto contenido de azúcar.", Imagen = "imagenes/linea1.png" },
                new ProcesoModel { Numero = 2, Titulo = "Secado Natural", Descripcion = "Secado en fruto entero en camas africanas.", Imagen = "imagenes/maduro.png" }
            },

            ImagenPrincipal = "imagenes/5f.png",
            Galeria = new List<string>
            {
                "imagenes/1f.png",
                "imagenes/2f.png",
                "imagenes/3f.png",
                "imagenes/4f.png"
            }
        },

        new ProductorModel
        {
            Id = "lucia",
            Nombre = "Lucía Herrera",
            Finca = "Finca La Cumbre",
            Ubicacion = "Tolima, Colombia",
            Frase = "Cafés de altitud con carácter firme y notas florales.",

            HistoriaTitulo = "Nuestra historia",
            HistoriaTexto = "Cultivamos a casi 2.000 metros de altura en las cumbres del Tolima, donde la neblina constante favorece la maduración lenta.",

            Altitud = "1.980 msnm",
            Proceso = "Lavado",
            Variedades = "Caturra, Colombia",
            Experiencia = "19+ años de experiencia",

            FincaDescripcion = "En las zonas más altas de la cordillera tolimense.",
            FincaImagen = "imagenes/gra.png",

            ProcesosDescripcion = "Lavados clásicos con aguas de manantial alpino.",
            SostenibilidadDescripcion = "Basura cero y compostaje integral del cisco y pulpa.",
            ProcesosFooterTexto = "Calidad de montaña garantizada.",

            Procesos = new List<ProcesoModel>
            {
                new ProcesoModel { Numero = 1, Titulo = "Cosecha de altura", Descripcion = "Recolección en cumbre.", Imagen = "imagenes/linea1.png" },
                new ProcesoModel { Numero = 2, Titulo = "Secado en marquesina", Descripcion = "Secado uniforme.", Imagen = "imagenes/taza.png" }
            },

            ImagenPrincipal = "imagenes/8f.png",
            Galeria = new List<string>
            {
                "imagenes/pro.png",
                "imagenes/camp.png",
                "imagenes/maduro.png",
                "imagenes/taza.png"
            }
        },

        new ProductorModel
        {
            Id = "oscar",
            Nombre = "Óscar Benítez",
            Finca = "Finca El Mirador",
            Ubicacion = "Tolima, Colombia",
            Frase = "Innovación y tradición unidas en el corazón del Tolima.",

            HistoriaTitulo = "Nuestra historia",
            HistoriaTexto = "Pioneros en la implementación de agricultura de precisión y energía solar para los procesos de secado en finca.",

            Altitud = "1.870 msnm",
            Proceso = "Anaeróbico",
            Variedades = "Castillo, Colombia",
            Experiencia = "17+ años de experiencia",

            FincaDescripcion = "Finca El Mirador combina la visión tecnológica con la herencia campesina tolimense.",
            FincaImagen = "imagenes/ban.png",

            ProcesosDescripcion = "Fermentaciones anaeróbicas controladas por sensores de pH y brix.",
            SostenibilidadDescripcion = "Paneles solares para el 100% de la energía de la finca.",
            ProcesosFooterTexto = "Tecnología al servicio del origen campesino.",

            Procesos = new List<ProcesoModel>
            {
                new ProcesoModel { Numero = 1, Titulo = "Control de Brix", Descripcion = "Medición de azúcares antes de cosechar.", Imagen = "imagenes/linea1.png" },
                new ProcesoModel { Numero = 2, Titulo = "Anaerobia", Descripcion = "Fermentación en tanques sellados sin oxígeno.", Imagen = "imagenes/linea3.png" }
            },

            ImagenPrincipal = "imagenes/gra.png",
            Galeria = new List<string>
            {
                "imagenes/espalda.png",
                "imagenes/manoscafe.png",
                "imagenes/pro.png",
                "imagenes/camp.png"
            }
        }
    };
}
