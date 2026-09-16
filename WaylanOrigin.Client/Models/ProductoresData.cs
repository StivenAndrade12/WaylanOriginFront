namespace WaylanOrigin.Client.Models;
using System.Collections.Generic;

public static class ProductoresData
{
    public static List<ProductorModel> Lista = new()
    {
        new ProductorModel
        {
            Id = 1,
            Nombre = "Juan Carlos Restrepo",
            Ubicacion = "Caldas",
            IdOrganizacion = 1,
            OrganizacionNombre = "Tres Nevados",
            Destacado = true,
            Frase = "El café es nuestra forma de vida y nuestra mayor herencia.",
            HistoriaTitulo = "Tradición de Tres Generaciones",
            HistoriaTexto = "Nuestra familia ha cultivado café en las laderas de Caldas por más de 60 años. Cada planta es cuidada con esmero artesanal, respetando los ciclos de la naturaleza y honrando el legado de nuestros abuelos que nos enseñaron a amar la tierra.",
            SostenibilidadDescripcion = "Conservación de fuentes hídricas y abonos orgánicos compostados.",
            ImagenPrincipal = "imagenes/camp.png",
            ImagenUrl = "imagenes/camp.png",
            Procedimientos = new List<ProcedimientoModel>
            {
                new ProcedimientoModel { Id = 1, Nombre = "01", Titulo = "Recolección Selectiva", Descripcion = "Cosecha manual grano a grano en punto óptimo de maduración.", Imagen = "imagenes/maduro.png" },
                new ProcedimientoModel { Id = 2, Nombre = "02", Titulo = "Despulpado y Fermentación", Descripcion = "Fermentación controlada por 24 horas para potenciar notas florales.", Imagen = "imagenes/fermentacion.PNG" },
                new ProcedimientoModel { Id = 3, Nombre = "03", Titulo = "Lavado con Agua de Manantial", Descripcion = "Lavado suave preservando azúcares naturales del mucílago.", Imagen = "imagenes/lavado.PNG" },
                new ProcedimientoModel { Id = 4, Nombre = "04", Titulo = "Secado al Sol", Descripcion = "Secado en marquesinas ventiladas hasta alcanzar humedad óptima del 11%.", Imagen = "imagenes/secado.PNG" }
            }
        },
        new ProductorModel
        {
            Id = 2,
            Nombre = "María Elena Gómez",
            Ubicacion = "Quindío",
            IdOrganizacion = 2,
            OrganizacionNombre = "FAMYCAFE",
            Destacado = true,
            Frase = "Cuidamos cada grano como si fuera el primero.",
            HistoriaTitulo = "Amor y Cuidado en Cada Cosecha",
            HistoriaTexto = "Desde el corazón del Quindío, lideramos una asociación de mujeres caficultoras que transforman el paisaje cafetero con dedicación, innovación y pasión por el café especial de micro-lote.",
            SostenibilidadDescripcion = "Manejo biológico de plagas y reforestación con especies nativas.",
            ImagenPrincipal = "imagenes/seorayseor.png",
            ImagenUrl = "imagenes/seorayseor.png",
            Procedimientos = new List<ProcedimientoModel>
            {
                new ProcedimientoModel { Id = 5, Nombre = "01", Titulo = "Selección Manual", Descripcion = "Clasificación por densidad y color en cerezas maduras.", Imagen = "imagenes/seleccion.PNG" },
                new ProcedimientoModel { Id = 6, Nombre = "02", Titulo = "Secado Natural Honey", Descripcion = "Proceso Honey que aporta notas dulces afrutadas.", Imagen = "imagenes/secado.PNG" }
            }
        },
        new ProductorModel
        {
            Id = 3,
            Nombre = "José Luis Herrera",
            Ubicacion = "Risaralda",
            IdOrganizacion = 1,
            OrganizacionNombre = "Tres Nevados",
            Destacado = true,
            Frase = "La calidad nace en el campo, pero se construye en familia.",
            HistoriaTitulo = "Compromiso con la Excelencia",
            HistoriaTexto = "Cultivamos a más de 1.800 metros de altitud bajo sombra de guamos y nogales, logrando un balance inigualable de acidez cítrica y cuerpo sedoso que caracteriza nuestra región.",
            SostenibilidadDescripcion = "Protección de aves migratorias y caficultura bajo sombra.",
            ImagenPrincipal = "imagenes/espalda.png",
            ImagenUrl = "imagenes/espalda.png",
            Procedimientos = new List<ProcedimientoModel>()
        },
        new ProductorModel
        {
            Id = 4,
            Nombre = "Andrés Felipe Torres",
            Ubicacion = "Tolima",
            IdOrganizacion = 1,
            OrganizacionNombre = "Tres Nevados",
            Destacado = true,
            Frase = "Nuestro café lleva el sabor de nuestra tierra.",
            HistoriaTitulo = "Innovación en el Tolima",
            HistoriaTexto = "Planadas y las montañas del sur del Tolima son el hogar donde cultivamos variedades Geisha y Pink Bourbon con perfiles exóticos reconocidos internacionalmente.",
            SostenibilidadDescripcion = "Agricultura regenerativa y cero agroquímicos de síntesis.",
            ImagenPrincipal = "imagenes/Gemini_Generated_Image_9hfuk69hfuk69hfu.png",
            ImagenUrl = "imagenes/Gemini_Generated_Image_9hfuk69hfuk69hfu.png",
            Procedimientos = new List<ProcedimientoModel>()
        },
        new ProductorModel
        {
            Id = 5,
            Nombre = "Lucía Fernández",
            Ubicacion = "Huila",
            IdOrganizacion = 2,
            OrganizacionNombre = "FAMYCAFE",
            Destacado = true,
            Frase = "Cada taza cuenta una historia de esfuerzo y esperanza.",
            HistoriaTitulo = "Esperanza y Tradición en Huila",
            HistoriaTexto = "En las faldas del Nevado del Huila, nuestras parcelas familiares producen cafés con vibrantes notas a caramelo y frutos rojos, resultado de una recolección meticulosa.",
            SostenibilidadDescripcion = "Cosecha de agua de lluvia y protección de microcuencas.",
            ImagenPrincipal = "imagenes/bar.png",
            ImagenUrl = "imagenes/bar.png",
            Procedimientos = new List<ProcedimientoModel>()
        },
        new ProductorModel
        {
            Id = 6,
            Nombre = "Carlos Alberto Rojas",
            Ubicacion = "Caldas",
            IdOrganizacion = 1,
            OrganizacionNombre = "Tres Nevados",
            Destacado = false,
            Frase = "El café nos une, la tierra nos da la fuerza.",
            HistoriaTitulo = "Fuerza y Arraigo Campesino",
            HistoriaTexto = "Trabajamos día a día en Chinchiná promoviendo el relevo generacional para que los jóvenes encuentren en el café un proyecto de vida digno y próspero.",
            SostenibilidadDescripcion = "Energía solar en beneficiaderos y reducción de huella hídrica.",
            ImagenPrincipal = "imagenes/costales.png",
            ImagenUrl = "imagenes/costales.png",
            Procedimientos = new List<ProcedimientoModel>()
        },
        new ProductorModel
        {
            Id = 7,
            Nombre = "Diego Fernando Sánchez",
            Ubicacion = "Quindío",
            IdOrganizacion = 2,
            OrganizacionNombre = "FAMYCAFE",
            Destacado = false,
            Frase = "Cultivar café es sembrar futuro.",
            HistoriaTitulo = "Sembrando Futuro en el Eje Cafetero",
            HistoriaTexto = "Pioneros en fermentaciones anaeróbicas y procesos experimentales que resaltan la complejidad sensorial del café colombiano de alta gama.",
            SostenibilidadDescripcion = "Conservación de suelos con coberturas vivas.",
            ImagenPrincipal = "imagenes/cafetarros.PNG",
            ImagenUrl = "imagenes/cafetarros.PNG",
            Procedimientos = new List<ProcedimientoModel>()
        },
        new ProductorModel
        {
            Id = 8,
            Nombre = "Paula Andrea Mejía",
            Ubicacion = "Risaralda",
            IdOrganizacion = 2,
            OrganizacionNombre = "FAMYCAFE",
            Destacado = false,
            Frase = "Somos parte de un mismo origen.",
            HistoriaTitulo = "Unión y Origen Cafetero",
            HistoriaTexto = "Uniendo comunidades campesinas para exportar café directamente, garantizando precios justos y bienestar integral para las familias recolectoras.",
            SostenibilidadDescripcion = "Certificaciones de comercio justo y equidad de género en el campo.",
            ImagenPrincipal = "imagenes/manoscafe.png",
            ImagenUrl = "imagenes/manoscafe.png",
            Procedimientos = new List<ProcedimientoModel>()
        }
    };
}
