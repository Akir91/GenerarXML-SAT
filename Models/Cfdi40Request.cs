using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;


namespace GenerarXML_SAT.Models
{
    public class Cfdi40Request
    {
        [Required]
        public int TipoDocumento { get; set; }
        public string Serie { get; set; }
        [Required]
        public string Folio { get; set; }
        [Required]
        public string Fecha { get; set; }
        public string FormaPago { get; set; }
        public string NoCertificado { get; set; }
        public string CondicionesDePago { get; set; }
        [Required]
        public decimal SubTotal { get; set; }
        public decimal Descuento { get; set; }
        [Required]
        public string Moneda { get; set; }
        public decimal TipoCambio { get; set; }
        [Required]
        public decimal Total { get; set; }
        [Required]
        public string TipoDeComprobante { get; set; }
        [Required]
        public string Exportacion { get; set; }
        public string MetodoPago { get; set; }
        [Required]
        public string LugarExpedicion { get; set; }

        [Required]
        public Emisor4 Emisor { get; set; }

        [Required]
        public Receptor4 Receptor { get; set; }

        [Required]
        public List<Concepto4> Conceptos { get; set; } = new List<Concepto4>();

        public Impuestos4 Impuestos { get; set; } = new Impuestos4();

        public CfdiRelacionados4 CfdiRelacionados { get; set; } = new CfdiRelacionados4();

        public Complemento4 Complemento { get; set; } = new Complemento4();

    }

    public class Emisor4
    {
        public string Rfc { get; set; }
        public string Nombre { get; set; }
        public string RegimenFiscal { get; set; }
    }

    public class Receptor4
    {
        public string Rfc { get; set; }
        public string Nombre { get; set; }
        public string DomicilioFiscalReceptor { get; set; }
        public string RegimenFiscalReceptor { get; set; }
        public string UsoCFDI { get; set; }
    }


    public class Concepto4
    {
        public cImpuestos4 Impuestos { get; set; }
        public string ClaveProdServ { get; set; }
        public decimal Cantidad { get; set; }
        public string ClaveUnidad { get; set; }
        public string Descripcion { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal Importe { get; set; }
        public decimal Descuento { get; set; }
        public string ObjetoImp { get; set; }
    }



    public class Impuestos4
    {
        public List<Traslado4> Traslados { get; set; }
        public decimal TotalImpuestosTrasladados { get; set; }
    }

    public class cImpuestos4
    {
        public List<Traslado4> Traslados { get; set; }
    }

    public class Traslado4
    {
        public decimal Base { get; set; }
        public string Impuesto { get; set; }
        public string TipoFactor { get; set; }
        public decimal TasaOCuota { get; set; }
        public decimal Importe { get; set; }
    }

    public class CfdiRelacionados4
    {
        public string TipoRelacion { get; set; }
        public List<string> UUIDs { get; set; } = new List<string>();
    }

    public class Complemento4
    {
        public List<Any> Any { get; set; }
    }

    public class Any
    {
        public CartaPorte CartaPorte { get; set; }
    }



    public class CartaPorte
    {

        public string Version { get; set; }


        public string IdCCP { get; set; }

        public string TranspInternac { get; set; }


        public string TotalDistRec { get; set; }
        public Ubicaciones Ubicaciones { get; set; }
        public Mercancias Mercancias { get; set; }
        public FiguraTransporte FiguraTransporte { get; set; }
    }

    public class Ubicaciones
    {
        public List<Ubicacion> Ubicacion { get; set; }
    }

    public class Ubicacion
    {

        public string TipoUbicacion { get; set; }


        public string RFCRemitenteDestinatario { get; set; }


        public string NombreRemitenteDestinatario { get; set; }


        public DateTime FechaHoraSalidaLlegada { get; set; }
        public Domicilio Domicilio { get; set; }


        public string DistanciaRecorrida { get; set; }
    }


    public class Domicilio
    {

        public string Calle { get; set; }


        public string Colonia { get; set; }


        public string Localidad { get; set; }


        public string Municipio { get; set; }


        public string Estado { get; set; }


        public string Pais { get; set; }

        public string CodigoPostal { get; set; }

        public string NumeroExterior { get; set; }


        public string NumeroInterior { get; set; }
    }

    public class Mercancias
    {

        public decimal PesoBrutoTotal { get; set; }
        public string UnidadPeso { get; set; }
        public int NumTotalMercancias { get; set; }
        public List<Mercancia> Mercancia { get; set; }
        public Autotransporte Autotransporte { get; set; }
    }

    public class Mercancia
    {
        public string BienesTransp { get; set; }
        public string Descripcion { get; set; }
        public decimal Cantidad { get; set; }
        public string ClaveUnidad { get; set; }
        public decimal PesoEnKg { get; set; }
        public decimal ValorMercancia { get; set; }
        public string Moneda { get; set; }
    }


    public class Autotransporte
    {
        public string PermSCT { get; set; }
        public string NumPermisoSCT { get; set; }
        public IdentificacionVehicular IdentificacionVehicular { get; set; }
        public Seguros Seguros { get; set; }
    }

    public class IdentificacionVehicular
    {
        public string ConfigVehicular { get; set; }
        public decimal PesoBrutoVehicular { get; set; }
        public string PlacaVM { get; set; }
        public int AnioModeloVM { get; set; }
    }

    public class Seguros
    {
        public string AseguraRespCivil { get; set; }
        public string PolizaRespCivil { get; set; }
    }

    public class FiguraTransporte
    {
        public TiposFigura TiposFigura { get; set; }
    }


    public class TiposFigura
    {
        public string TipoFigura { get; set; }
        public string RFCFigura { get; set; }
        public string NumLicencia { get; set; }
        public string NombreFigura { get; set; }
    }
}

