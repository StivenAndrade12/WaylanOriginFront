public class CitaDto
{
    // HERO (solo lo editable)
    public string Titulo { get; set; }
    public string Descripcion { get; set; }
    public string Fecha { get; set; }
    public string Ubicacion { get; set; }
    public string HeroImageUrl { get; set; }

    // HISTORIA
    public string HistoriaTitulo { get; set; }
    public string HistoriaParrafo1 { get; set; }
    public string HistoriaParrafo2 { get; set; }
    public string Testimonio { get; set; }

    // RAZONES (iconos fijos, textos editables)
    public string RazonesTitulo { get; set; }
    public string Razon1Titulo { get; set; }
    public string Razon1Texto { get; set; }
    public string Razon2Titulo { get; set; }
    public string Razon2Texto { get; set; }
    public string Razon3Titulo { get; set; }
    public string Razon3Texto { get; set; }

    // OPTIMIZACIÓN (numeritos e iconos fijos)
    public string OptCard1ImageUrl { get; set; }
    public string OptCard1Titulo { get; set; }
    public string OptCard1Texto { get; set; }

    public string OptCard2ImageUrl { get; set; }
    public string OptCard2Titulo { get; set; }
    public string OptCard2Texto { get; set; }

    // PROCESO (iconos, numeritos y rayita dorada fijos)
    public string ProcesoEtapa1Titulo { get; set; }
    public string ProcesoEtapa1Texto { get; set; }
    public string ProcesoEtapa2Titulo { get; set; }
    public string ProcesoEtapa2Texto { get; set; }
    public string ProcesoEtapa3Titulo { get; set; }
    public string ProcesoEtapa3Texto { get; set; }
    public string ProcesoEtapa4Titulo { get; set; }
    public string ProcesoEtapa4Texto { get; set; }
    public string ProcesoEtapa5Titulo { get; set; }
    public string ProcesoEtapa5Texto { get; set; }

    // CALLOUT (icono y botón fijos)
    public string CalloutTitulo { get; set; }
    public string CalloutTexto { get; set; }

    // IMPACTO (todo fijo — no va nada aquí)
}
