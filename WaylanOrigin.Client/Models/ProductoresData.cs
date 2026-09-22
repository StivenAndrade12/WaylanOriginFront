namespace WaylanOrigin.Client.Models;
using System.Collections.Generic;

public static class ProductoresData
{
    public static List<ProductorModel> Lista = new()
    {
        // ==========================================
        // 1 AL 8: PRODUCTORES OFICIALES DEL BOCETO
        // ==========================================
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
            ImagenPrincipal = "imagenes/productores/juan_carlos_restrepo.jpg",
            ImagenUrl = "imagenes/productores/juan_carlos_restrepo.jpg",
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
            ImagenPrincipal = "imagenes/productores/maria_elena_gomez.jpg",
            ImagenUrl = "imagenes/productores/maria_elena_gomez.jpg",
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
            ImagenPrincipal = "imagenes/productores/jose_luis_herrera.jpg",
            ImagenUrl = "imagenes/productores/jose_luis_herrera.jpg",
            Procedimientos = new List<ProcedimientoModel>
            {
                new ProcedimientoModel { Id = 7, Nombre = "01", Titulo = "Cosecha en Altura", Descripcion = "Cosecha a 1.800 msnm con selección rigurosa de grano maduro.", Imagen = "imagenes/maduro.png" },
                new ProcedimientoModel { Id = 8, Nombre = "02", Titulo = "Fermentación Prolongada", Descripcion = "36 horas de fermentación anaeróbica en tanques sellados.", Imagen = "imagenes/fermentacion.PNG" }
            }
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
            ImagenPrincipal = "imagenes/productores/andres_felipe_torres.jpg",
            ImagenUrl = "imagenes/productores/andres_felipe_torres.jpg",
            Procedimientos = new List<ProcedimientoModel>
            {
                new ProcedimientoModel { Id = 9, Nombre = "01", Titulo = "Desmucilaginado Suave", Descripcion = "Preservación de azúcares naturales de la variedad Bourbon.", Imagen = "imagenes/lavado.PNG" },
                new ProcedimientoModel { Id = 10, Nombre = "02", Titulo = "Secado en Camas Africanas", Descripcion = "Secado lento y uniforme a temperatura controlada.", Imagen = "imagenes/secado.PNG" }
            }
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
            ImagenPrincipal = "imagenes/productores/lucia_fernandez.jpg",
            ImagenUrl = "imagenes/productores/lucia_fernandez.jpg",
            Procedimientos = new List<ProcedimientoModel>
            {
                new ProcedimientoModel { Id = 11, Nombre = "01", Titulo = "Recolección Finca La Esperanza", Descripcion = "Maduración lenta en alturas de San Agustín Huila.", Imagen = "imagenes/maduro.png" },
                new ProcedimientoModel { Id = 12, Nombre = "02", Titulo = "Lavado Artesanal", Descripcion = "Lavado con agua cristalina de nacimiento natural.", Imagen = "imagenes/lavado.PNG" }
            }
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
            ImagenPrincipal = "imagenes/productores/carlos_alberto_rojas.jpg",
            ImagenUrl = "imagenes/productores/carlos_alberto_rojas.jpg",
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
            ImagenPrincipal = "imagenes/productores/diego_fernando_sanchez.jpg",
            ImagenUrl = "imagenes/productores/diego_fernando_sanchez.jpg",
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
            ImagenPrincipal = "imagenes/productores/paula_andrea_mejia.jpg",
            ImagenUrl = "imagenes/productores/paula_andrea_mejia.jpg",
            Procedimientos = new List<ProcedimientoModel>()
        },

        // ==========================================
        // 9 AL 26: FAMILIAS CAFICULTORAS ADICIONALES
        // ==========================================
        new ProductorModel
        {
            Id = 9,
            Nombre = "Hernando Castaño Morales",
            Ubicacion = "Caldas",
            IdOrganizacion = 1,
            OrganizacionNombre = "Tres Nevados",
            Destacado = true,
            Frase = "El aroma del café de montaña despierta el alma de nuestra gente.",
            HistoriaTitulo = "Custodio del Bosque y el Café",
            HistoriaTexto = "A 1.850 metros de altitud en Manizales, la Finca El Mirador combina el cultivo de café Castillo y Geisha con la protección de reservas forestales de niebla.",
            SostenibilidadDescripcion = "Corredor biológico para aves andinas y abono orgánico fermentado.",
            ImagenPrincipal = "imagenes/productores/juan_carlos_restrepo.jpg",
            ImagenUrl = "imagenes/productores/juan_carlos_restrepo.jpg",
            Procedimientos = new List<ProcedimientoModel>()
        },
        new ProductorModel
        {
            Id = 10,
            Nombre = "Gloria Patricia Osorio",
            Ubicacion = "Quindío",
            IdOrganizacion = 2,
            OrganizacionNombre = "FAMYCAFE",
            Destacado = true,
            Frase = "Mujeres caficultoras cuidando el bosque y cosechando café especial.",
            HistoriaTitulo = "Liderazgo Femenino en Pijao",
            HistoriaTexto = "Desde el municipio cordillerano de Pijao, producimos cafés especiales con procesos Honey y Naturales que han recibido reconocimientos en ferias internacionales.",
            SostenibilidadDescripcion = "Sistemas agroforestales con árboles nativos y sombrío regulado.",
            ImagenPrincipal = "imagenes/productores/maria_elena_gomez.jpg",
            ImagenUrl = "imagenes/productores/maria_elena_gomez.jpg",
            Procedimientos = new List<ProcedimientoModel>()
        },
        new ProductorModel
        {
            Id = 11,
            Nombre = "Fabio Nelson Echeverry",
            Ubicacion = "Risaralda",
            IdOrganizacion = 1,
            OrganizacionNombre = "Tres Nevados",
            Destacado = false,
            Frase = "Tres generaciones conservando la pureza del agua y el café.",
            HistoriaTitulo = "Café de Altura en Santa Rosa",
            HistoriaTexto = "En Santa Rosa de Cabal cosechamos a 1.900 msnm granos de Caturra Chiroso irrigados con aguas termales y de deshielo de la cordillera.",
            SostenibilidadDescripcion = "Protección de nacimientos de agua y terrazas antierosión.",
            ImagenPrincipal = "imagenes/productores/jose_luis_herrera.jpg",
            ImagenUrl = "imagenes/productores/jose_luis_herrera.jpg",
            Procedimientos = new List<ProcedimientoModel>()
        },
        new ProductorModel
        {
            Id = 12,
            Nombre = "Esperanza Bedoya Ortiz",
            Ubicacion = "Tolima",
            IdOrganizacion = 1,
            OrganizacionNombre = "Tres Nevados",
            Destacado = true,
            Frase = "El café especial transformó nuestra región en símbolo de paz y excelencia.",
            HistoriaTitulo = "Paz y Café en Planadas",
            HistoriaTexto = "Planadas es cuna de algunos de los mejores cafés de Colombia. En nuestra parcela cultivamos Pink Bourbon con perfiles dulces y notas a jazmín.",
            SostenibilidadDescripcion = "Agricultura limpia, abonos biológicos y cero pesticidas sintéticos.",
            ImagenPrincipal = "imagenes/productores/lucia_fernandez.jpg",
            ImagenUrl = "imagenes/productores/lucia_fernandez.jpg",
            Procedimientos = new List<ProcedimientoModel>()
        },
        new ProductorModel
        {
            Id = 13,
            Nombre = "Jairo de Jesús Ramírez",
            Ubicacion = "Huila",
            IdOrganizacion = 2,
            OrganizacionNombre = "FAMYCAFE",
            Destacado = false,
            Frase = "Cosechamos bajo la mirada mística de los Andes ancestrales.",
            HistoriaTitulo = "Mística Cafetera en San Agustín",
            HistoriaTexto = "A orillas del cañón del río Magdalena, nuestra finca familiar produce microlotes de Tabi y Colombia con fermentaciones controladas en frío.",
            SostenibilidadDescripcion = "Cuidado de la cuenca alta del río Magdalena y microfauna del suelo.",
            ImagenPrincipal = "imagenes/productores/andres_felipe_torres.jpg",
            ImagenUrl = "imagenes/productores/andres_felipe_torres.jpg",
            Procedimientos = new List<ProcedimientoModel>()
        },
        new ProductorModel
        {
            Id = 14,
            Nombre = "Diana Marcela Cárdenas",
            Ubicacion = "Caldas",
            IdOrganizacion = 1,
            OrganizacionNombre = "Tres Nevados",
            Destacado = false,
            Frase = "Innovación científica y amor campesino en cada cereza madura.",
            HistoriaTitulo = "Vanguardia en Chinchiná",
            HistoriaTexto = "Ingeniera agrónoma y caficultora de corazón, aplico análisis sensorial de suelos para maximizar el dulzor natural de cada taza.",
            SostenibilidadDescripcion = "Tratamiento de aguas mieles con biofiltros de carbón activado.",
            ImagenPrincipal = "imagenes/productores/paula_andrea_mejia.jpg",
            ImagenUrl = "imagenes/productores/paula_andrea_mejia.jpg",
            Procedimientos = new List<ProcedimientoModel>()
        },
        new ProductorModel
        {
            Id = 15,
            Nombre = "Gonzalo Marín Quintero",
            Ubicacion = "Quindío",
            IdOrganizacion = 2,
            OrganizacionNombre = "FAMYCAFE",
            Destacado = false,
            Frase = "Entre palmas de cera y neblina cultivamos el café que enorgullece al país.",
            HistoriaTitulo = "El Secreto del Valle de Cocora",
            HistoriaTexto = "En las inmediaciones de Salento, nuestras plantas crecen al abrigo del viento cordillerano produciendo granos de extrema densidad y aroma intenso.",
            SostenibilidadDescripcion = "Reforestación con Palma de Cera y conservación de suelos volcánicos.",
            ImagenPrincipal = "imagenes/productores/carlos_alberto_rojas.jpg",
            ImagenUrl = "imagenes/productores/carlos_alberto_rojas.jpg",
            Procedimientos = new List<ProcedimientoModel>()
        },
        new ProductorModel
        {
            Id = 16,
            Nombre = "Nubia Estella Vargas",
            Ubicacion = "Risaralda",
            IdOrganizacion = 2,
            OrganizacionNombre = "FAMYCAFE",
            Destacado = false,
            Frase = "Cada grano recolectado a mano es sustento y educación para nuestros hijos.",
            HistoriaTitulo = "Esfuerzo y Ternura en Belén de Umbría",
            HistoriaTexto = "Junto a mis tres hijas seleccionamos únicamente cerezas maduras al 100%, garantizando una taza limpia y sedosa.",
            SostenibilidadDescripcion = "Secado solar en marquesinas ecológicas sin consumo energético fósil.",
            ImagenPrincipal = "imagenes/productores/maria_elena_gomez.jpg",
            ImagenUrl = "imagenes/productores/maria_elena_gomez.jpg",
            Procedimientos = new List<ProcedimientoModel>()
        },
        new ProductorModel
        {
            Id = 17,
            Nombre = "Rodrigo Arango Botero",
            Ubicacion = "Tolima",
            IdOrganizacion = 1,
            OrganizacionNombre = "Tres Nevados",
            Destacado = false,
            Frase = "El aire frío del Nevado madura lentamente la dulzura de nuestra taza.",
            HistoriaTitulo = "Cañón del Combeima Cafetero",
            HistoriaTexto = "Cosechamos a los pies del Nevado del Tolima. El choque térmico entre el día soleado y la noche fría genera alta concentración de sacarosa en el grano.",
            SostenibilidadDescripcion = "Manejo agroecológico con abonos verdes y coberturas vivas.",
            ImagenPrincipal = "imagenes/productores/diego_fernando_sanchez.jpg",
            ImagenUrl = "imagenes/productores/diego_fernando_sanchez.jpg",
            Procedimientos = new List<ProcedimientoModel>()
        },
        new ProductorModel
        {
            Id = 18,
            Nombre = "Amparo Lucía Guzmán",
            Ubicacion = "Huila",
            IdOrganizacion = 2,
            OrganizacionNombre = "FAMYCAFE",
            Destacado = false,
            Frase = "El Huila tiene el corazón en sus cafetales y el alma en cada familia.",
            HistoriaTitulo = "Dulzura y Cacao en Pitalito",
            HistoriaTexto = "En el Valle de Laboyos producimos café con notas a panela, caña de azúcar y chocolate amargo mediante secado lento a la sombra.",
            SostenibilidadDescripcion = "Huertos familiares de seguridad alimentaria y compostaje orgánico.",
            ImagenPrincipal = "imagenes/productores/lucia_fernandez.jpg",
            ImagenUrl = "imagenes/productores/lucia_fernandez.jpg",
            Procedimientos = new List<ProcedimientoModel>()
        },
        new ProductorModel
        {
            Id = 19,
            Nombre = "Gustavo Adolfo Villegas",
            Ubicacion = "Caldas",
            IdOrganizacion = 1,
            OrganizacionNombre = "Tres Nevados",
            Destacado = false,
            Frase = "Patrimonio arquitectónico y caficultura en las laderas caldenses.",
            HistoriaTitulo = "Tradición en Salamina",
            HistoriaTexto = "Ciudad luz de Colombia, Salamina conserva cafetales históricos bajo sombra que producen cafés balanceados con acidez málica brillante.",
            SostenibilidadDescripcion = "Caficultura bajo dosel de nogales y guamos centenarios.",
            ImagenPrincipal = "imagenes/productores/juan_carlos_restrepo.jpg",
            ImagenUrl = "imagenes/productores/juan_carlos_restrepo.jpg",
            Procedimientos = new List<ProcedimientoModel>()
        },
        new ProductorModel
        {
            Id = 20,
            Nombre = "Luz Mery Valencia",
            Ubicacion = "Quindío",
            IdOrganizacion = 2,
            OrganizacionNombre = "FAMYCAFE",
            Destacado = false,
            Frase = "Canastos tejidos a mano y café de altura cosechado con gratitud.",
            HistoriaTitulo = "Artesanía y Café en Filandia",
            HistoriaTexto = "Rescatamos la técnica tradicional de recolección en canastos de bejuco, evitando golpes en la fruta y preservando su integridad.",
            SostenibilidadDescripcion = "Preservación del bejuco nativo y banco comunitario de semillas.",
            ImagenPrincipal = "imagenes/productores/paula_andrea_mejia.jpg",
            ImagenUrl = "imagenes/productores/paula_andrea_mejia.jpg",
            Procedimientos = new List<ProcedimientoModel>()
        },
        new ProductorModel
        {
            Id = 21,
            Nombre = "Álvaro Enrique Duque",
            Ubicacion = "Risaralda",
            IdOrganizacion = 1,
            OrganizacionNombre = "Tres Nevados",
            Destacado = false,
            Frase = "Desde el corazón del viento cuidamos la biodiversidad y el buen café.",
            HistoriaTitulo = "El Colina de Apía",
            HistoriaTexto = "En Apía, mirador del Eje Cafetero, cultivamos Bourbon Chiroso con fermentación aeróbica en cereza entera que despierta notas a frutos amarillos.",
            SostenibilidadDescripcion = "Reserva privada de la sociedad civil con avistamiento de aves.",
            ImagenPrincipal = "imagenes/productores/jose_luis_herrera.jpg",
            ImagenUrl = "imagenes/productores/jose_luis_herrera.jpg",
            Procedimientos = new List<ProcedimientoModel>()
        },
        new ProductorModel
        {
            Id = 22,
            Nombre = "Mauricio Beltrán Cruz",
            Ubicacion = "Tolima",
            IdOrganizacion = 1,
            OrganizacionNombre = "Tres Nevados",
            Destacado = false,
            Frase = "Montañas indomables que dan un café complejo, achocolatado y cítrico.",
            HistoriaTitulo = "Fuerza Cafetera en Chaparral",
            HistoriaTexto = "Finca La Esperanza en Chaparral produce cafés especiales con certificación Rainforest Alliance y prácticas de conservación hídrica estricta.",
            SostenibilidadDescripcion = "Tratamiento de aguas residuales y cero desforestación.",
            ImagenPrincipal = "imagenes/productores/andres_felipe_torres.jpg",
            ImagenUrl = "imagenes/productores/andres_felipe_torres.jpg",
            Procedimientos = new List<ProcedimientoModel>()
        },
        new ProductorModel
        {
            Id = 23,
            Nombre = "Martha Cecilia Ortiz",
            Ubicacion = "Huila",
            IdOrganizacion = 2,
            OrganizacionNombre = "FAMYCAFE",
            Destacado = false,
            Frase = "Fermentaciones cuidadosas para resaltar notas a jazmín y frutas.",
            HistoriaTitulo = "Garzón: Alma de Café Especial",
            HistoriaTexto = "Desarrollamos procesos naturales en camas parabólicas que acentúan notas viníferas y cuerpo cremoso, deleitando a catadores de todo el mundo.",
            SostenibilidadDescripcion = "Uso de paneles solares para el movimiento de aire en secadores.",
            ImagenPrincipal = "imagenes/productores/maria_elena_gomez.jpg",
            ImagenUrl = "imagenes/productores/maria_elena_gomez.jpg",
            Procedimientos = new List<ProcedimientoModel>()
        },
        new ProductorModel
        {
            Id = 24,
            Nombre = "William Javier Rincón",
            Ubicacion = "Caldas",
            IdOrganizacion = 1,
            OrganizacionNombre = "Tres Nevados",
            Destacado = false,
            Frase = "Dedicación silenciosa desde la madrugada para el mejor café del mundo.",
            HistoriaTitulo = "El Legado de Neira",
            HistoriaTexto = "En las estribaciones de Neira, cuidamos lotes seleccionados de Castillo con fermentación láctica que aportan notas a caramelo suave y mantequilla.",
            SostenibilidadDescripcion = "Conservación de franjas de bosque nativo en linderos.",
            ImagenPrincipal = "imagenes/productores/carlos_alberto_rojas.jpg",
            ImagenUrl = "imagenes/productores/carlos_alberto_rojas.jpg",
            Procedimientos = new List<ProcedimientoModel>()
        },
        new ProductorModel
        {
            Id = 25,
            Nombre = "Claudia Inés Serna",
            Ubicacion = "Quindío",
            IdOrganizacion = 2,
            OrganizacionNombre = "FAMYCAFE",
            Destacado = false,
            Frase = "Tierras fértiles del Quindío convertidas en tazas inolvidables.",
            HistoriaTitulo = "Pasión por el Honey en Montenegro",
            HistoriaTexto = "Aplicamos el proceso Yellow Honey que conserva el mucílago exacto para obtener una acidez dulce balanceada con regusto a miel pura de abejas.",
            SostenibilidadDescripcion = "Apiarios familiares para favorecer la polinización de los cafetales.",
            ImagenPrincipal = "imagenes/productores/paula_andrea_mejia.jpg",
            ImagenUrl = "imagenes/productores/paula_andrea_mejia.jpg",
            Procedimientos = new List<ProcedimientoModel>()
        },
        new ProductorModel
        {
            Id = 26,
            Nombre = "Jaime Alberto Cardona",
            Ubicacion = "Tolima",
            IdOrganizacion = 1,
            OrganizacionNombre = "Tres Nevados",
            Destacado = false,
            Frase = "La cordillera central guarda en cada fruto el compromiso campesino.",
            HistoriaTitulo = "Orgullo Cafetero en Líbano",
            HistoriaTexto = "Finca El Recuerdo en Líbano Tolima, con más de 70 años de historia cafetera, combina variedades tradicionales con procesos de secado lento.",
            SostenibilidadDescripcion = "Manejo integrado de plagas biológico y protección de fuentes de agua.",
            ImagenPrincipal = "imagenes/productores/diego_fernando_sanchez.jpg",
            ImagenUrl = "imagenes/productores/diego_fernando_sanchez.jpg",
            Procedimientos = new List<ProcedimientoModel>()
        },
        new ProductorModel
        {
            Id = 27,
            Nombre = "Néstor Raúl Peñuela",
            Ubicacion = "Caldas",
            IdOrganizacion = 1,
            OrganizacionNombre = "Tres Nevados",
            Destacado = false,
            Frase = "Montañas donde la neblina abraza la maduración perfecta de la cereza.",
            HistoriaTitulo = "Café de Cordillera en Pensilvania",
            HistoriaTexto = "Cultivamos en las altas cumbres de Pensilvania Caldas, donde el microclima húmedo y templado acentúa notas florales y cítricas en taza.",
            SostenibilidadDescripcion = "Protección de laderas y mantenimiento de bosques de galería.",
            ImagenPrincipal = "imagenes/productores/juan_carlos_restrepo.jpg",
            ImagenUrl = "imagenes/productores/juan_carlos_restrepo.jpg",
            Procedimientos = new List<ProcedimientoModel>()
        },
        new ProductorModel
        {
            Id = 28,
            Nombre = "Beatriz Elena Montoya",
            Ubicacion = "Quindío",
            IdOrganizacion = 2,
            OrganizacionNombre = "FAMYCAFE",
            Destacado = false,
            Frase = "Cada amanecer en el cafetal es una bendición que compartimos con amor.",
            HistoriaTitulo = "Amor por la Tierra en Circasia",
            HistoriaTexto = "En Circasia lideramos procesos de café natural secado en camas elevadas que permiten obtener un dulzor pronunciado a frutos rojos silvestres.",
            SostenibilidadDescripcion = "Compostaje de pulpa de café y abonos verdes orgánicos.",
            ImagenPrincipal = "imagenes/productores/maria_elena_gomez.jpg",
            ImagenUrl = "imagenes/productores/maria_elena_gomez.jpg",
            Procedimientos = new List<ProcedimientoModel>()
        },
        new ProductorModel
        {
            Id = 29,
            Nombre = "Guillermo León Henao",
            Ubicacion = "Risaralda",
            IdOrganizacion = 1,
            OrganizacionNombre = "Tres Nevados",
            Destacado = false,
            Frase = "Café cultivado en armonía con comunidades y la naturaleza.",
            HistoriaTitulo = "Armonía en Mistrató",
            HistoriaTexto = "Respetamos las costumbres ancestrales de cultivo bajo sombra, produciendo un café orgánico limpio con cuerpo denso y acidez sedosa.",
            SostenibilidadDescripcion = "Sistemas agroforestales con especies maderables nativas.",
            ImagenPrincipal = "imagenes/productores/jose_luis_herrera.jpg",
            ImagenUrl = "imagenes/productores/jose_luis_herrera.jpg",
            Procedimientos = new List<ProcedimientoModel>()
        },
        new ProductorModel
        {
            Id = 30,
            Nombre = "Rosalba Morales Castro",
            Ubicacion = "Tolima",
            IdOrganizacion = 1,
            OrganizacionNombre = "Tres Nevados",
            Destacado = false,
            Frase = "En las faldas de la cordillera la paciencia se transforma en dulzura.",
            HistoriaTitulo = "Dulzura en Anzoátegui",
            HistoriaTexto = "Finca El Paraíso en Anzoátegui cultiva Geisha y Caturra a 1.940 msnm con fermentaciones aeróbicas prolongadas que revelan notas a jazmín y durazno.",
            SostenibilidadDescripcion = "Cero uso de agroquímicos y protección de fuentes de agua pura.",
            ImagenPrincipal = "imagenes/productores/lucia_fernandez.jpg",
            ImagenUrl = "imagenes/productores/lucia_fernandez.jpg",
            Procedimientos = new List<ProcedimientoModel>()
        },
        new ProductorModel
        {
            Id = 31,
            Nombre = "Héctor Fabio Agudelo",
            Ubicacion = "Huila",
            IdOrganizacion = 2,
            OrganizacionNombre = "FAMYCAFE",
            Destacado = false,
            Frase = "Acevedo es tierra fértil de campeones de taza de excelencia.",
            HistoriaTitulo = "Excelencia en Acevedo Huila",
            HistoriaTexto = "Productores con múltiples reconocimientos en Taza de la Excelencia gracias a la selección meticulosa y fermentación controlada de cerezas.",
            SostenibilidadDescripcion = "Manejo de coberturas nobles para retención de humedad y vida microbiana.",
            ImagenPrincipal = "imagenes/productores/andres_felipe_torres.jpg",
            ImagenUrl = "imagenes/productores/andres_felipe_torres.jpg",
            Procedimientos = new List<ProcedimientoModel>()
        },
        new ProductorModel
        {
            Id = 32,
            Nombre = "Luz Dary Betancourt",
            Ubicacion = "Caldas",
            IdOrganizacion = 1,
            OrganizacionNombre = "Tres Nevados",
            Destacado = false,
            Frase = "Cosecha artesanal en laderas volcánicas bajo el aroma del azahar.",
            HistoriaTitulo = "Cenizas Volcánicas en Villamaría",
            HistoriaTexto = "Los suelos enriquecidos con ceniza volcánica del Ruiz otorgan a nuestros cafetales minerales únicos que enriquecen el sabor final.",
            SostenibilidadDescripcion = "Protección de cuencas abastecedoras y siembra de árboles protectores.",
            ImagenPrincipal = "imagenes/productores/paula_andrea_mejia.jpg",
            ImagenUrl = "imagenes/productores/paula_andrea_mejia.jpg",
            Procedimientos = new List<ProcedimientoModel>()
        },
        new ProductorModel
        {
            Id = 33,
            Nombre = "César Augusto Giraldo",
            Ubicacion = "Quindío",
            IdOrganizacion = 2,
            OrganizacionNombre = "FAMYCAFE",
            Destacado = false,
            Frase = "Mirador natural donde el sol y el viento secan lentamente cada lote.",
            HistoriaTitulo = "El Balcón del Quindío en Buenavista",
            HistoriaTexto = "Desde Buenavista, con vista a todo el valle, aprovechamos las corrientes de aire cálido para un secado solar homogéneo y lento.",
            SostenibilidadDescripcion = "Energía solar para el beneficiadero ecológico y reciclaje de agua.",
            ImagenPrincipal = "imagenes/productores/diego_fernando_sanchez.jpg",
            ImagenUrl = "imagenes/productores/diego_fernando_sanchez.jpg",
            Procedimientos = new List<ProcedimientoModel>()
        }
    };
}
