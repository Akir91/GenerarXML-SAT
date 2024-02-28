﻿using GenerarXML_SAT.Models;
using GenerarXML_SAT.Services;
using GenerarXML_SAT.Utilidades;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SP_Entities.Responses;
using System.Text;
using System.Text.Json.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace GenerarXML_SAT
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var oComprobante = GenerateCDFI4WithCartaPorteAsync().Result;
                string outputXmlFilePath = $"{Utils.RegresarRutaApp()}/Resources/XmlEjemplo.xml";
                CreateXML(oComprobante, outputXmlFilePath);
                Console.WriteLine("XML creado exitosamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al generar XML: {ex}");
            }
        }

        private static async Task<Comprobante> GenerateCDFI4WithCartaPorteAsync()
        {
            var oComprobante = new Comprobante();

            //Obligatorios
            oComprobante.Version = "4.0";
            oComprobante.Sello = "FirmaSAT";
            oComprobante.NoCertificado = "FirmaSAT";
            oComprobante.Certificado = "FirmaSAT";

            oComprobante.Folio = "435";
            oComprobante.Fecha = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
            oComprobante.Moneda = "XXX";
            oComprobante.SubTotal = 0m;
            oComprobante.Total = 0m;
            oComprobante.TipoDeComprobante = "T";
            oComprobante.Exportacion = "01";
            oComprobante.LugarExpedicion = "44190";

            //Opcionales
            oComprobante.Serie = "TB";
            oComprobante.TipoCambio = 1;


            //Nodo Emisor 
            ComprobanteEmisor oEmisor = new ComprobanteEmisor();

            //Obligatorios
            oEmisor.Rfc = "EKU9003173C9";
            oEmisor.Nombre = "ESCUELA KEMPER URGATE";
            oEmisor.RegimenFiscal = "601";

            //Nodo receptor
            ComprobanteReceptor oReceptor = new ComprobanteReceptor();
            oReceptor.Rfc = "EKU9003173C9";
            oReceptor.Nombre = "ESCUELA KEMPER URGATE";
            oReceptor.DomicilioFiscalReceptor = "02300";
            oReceptor.RegimenFiscalReceptor = "601";
            oReceptor.UsoCFDI = "S01";

            //Nodo Conceptos
            List<ComprobanteConcepto> listConceptos = new List<ComprobanteConcepto>();
            ComprobanteConcepto oConcepto = new ComprobanteConcepto();
            oConcepto.ClaveProdServ = "48101604";
            oConcepto.Cantidad = 1;
            oConcepto.ClaveUnidad = "H87";
            oConcepto.Descripcion = "MOLINO ELEC. FIORENZATO F4 EVO ROJO 110V TOLVA 500g";
            oConcepto.ValorUnitario = 0;
            oConcepto.Importe = Convert.ToDecimal(0.ToString("0.00"));
            oConcepto.ObjetoImp = "01";
            listConceptos.Add(oConcepto);

            //Se añaden objetos a la estructura del XML
            oComprobante.Receptor = oReceptor;
            oComprobante.Emisor = oEmisor;
            oComprobante.Conceptos = listConceptos.ToArray();

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //Nodo Complemento
            CartaPorte oCartaPorte = new CartaPorte();
            oCartaPorte.Version = "3.0";
            oCartaPorte.TranspInternac = "No";
            oCartaPorte.TotalDistRec = 803.000000m;

            List<CartaPorteUbicacion> listUbicaciones = new List<CartaPorteUbicacion>();
            CartaPorteUbicacion oUbicacion = new CartaPorteUbicacion();
            oUbicacion.TipoUbicacion = "Origen";
            oUbicacion.RFCRemitenteDestinatario = oEmisor.Rfc;
            oUbicacion.NombreRemitenteDestinatario = oEmisor.Nombre;
            oUbicacion.FechaHoraSalidaLlegada = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"));
            CartaPorteUbicacionDomicilio oDomicilio = new CartaPorteUbicacionDomicilio();
            oDomicilio.Calle = "AV. NIÑOS HÉROES";
            oDomicilio.Colonia = "0017";
            oDomicilio.Localidad = "03";
            oDomicilio.Municipio = "039";
            oDomicilio.Estado = "JAL";
            oDomicilio.Pais = "MEX";
            oDomicilio.CodigoPostal = "44190";
            oUbicacion.Domicilio = oDomicilio;
            listUbicaciones.Add(oUbicacion);


            oUbicacion = new CartaPorteUbicacion();
            oUbicacion.TipoUbicacion = "Destino";
            oUbicacion.RFCRemitenteDestinatario = oReceptor.Rfc;
            oUbicacion.NombreRemitenteDestinatario = oReceptor.Nombre;
            oUbicacion.FechaHoraSalidaLlegada = Convert.ToDateTime(DateTime.Now.AddHours(9).ToString("yyyy-MM-ddTHH:mm:ss"));
            oUbicacion.DistanciaRecorrida = 803;
            oDomicilio = new CartaPorteUbicacionDomicilio();
            oDomicilio.Calle = "AVENIDA IGNACIO DE LA LLAVE";
            oDomicilio.NumeroExterior = "35";
            oDomicilio.NumeroInterior = "5 PLAZA DEL TEATRO";
            oDomicilio.Colonia = "0063";
            oDomicilio.Localidad = "10";
            oDomicilio.Municipio = "087";
            oDomicilio.Estado = "VER";
            oDomicilio.Pais = "MEX";
            oDomicilio.CodigoPostal = "91055";
            oUbicacion.Domicilio = oDomicilio;
            listUbicaciones.Add(oUbicacion);

            oCartaPorte.Ubicaciones = listUbicaciones.ToArray();


            CartaPorteMercancias oMercancias = new CartaPorteMercancias();
            oMercancias.NumTotalMercancias = 1;
            oMercancias.PesoBrutoTotal = 9000;
            oMercancias.UnidadPeso = "KGM";

            List<CartaPorteMercanciasMercancia> listMercancias = new List<CartaPorteMercanciasMercancia>();

            foreach (var concepto in oComprobante.Conceptos)
            {
                CartaPorteMercanciasMercancia oMercancia = new CartaPorteMercanciasMercancia();
                oMercancia.BienesTransp = concepto.ClaveProdServ;
                oMercancia.Descripcion = concepto.Descripcion;
                oMercancia.Cantidad = concepto.Cantidad;
                oMercancia.ClaveUnidad = concepto.ClaveUnidad;
                oMercancia.PesoEnKg = 9000;
                oMercancia.ValorMercancia = 17744.82m;
                oMercancia.Moneda = "MXN";
                listMercancias.Add(oMercancia);
            }
            oMercancias.Mercancia = listMercancias.ToArray();

            CartaPorteMercanciasAutotransporte oAutotransporte = new CartaPorteMercanciasAutotransporte();
            oAutotransporte.PermSCT = "TPAF02";
            oAutotransporte.NumPermisoSCT = "77777777777";

            CartaPorteMercanciasAutotransporteIdentificacionVehicular oIdentificacionVehicular = new CartaPorteMercanciasAutotransporteIdentificacionVehicular();
            oIdentificacionVehicular.ConfigVehicular = "VL";
            oIdentificacionVehicular.PesoBrutoVehicular = 3100;
            oIdentificacionVehicular.PlacaVM = "XX7158A";
            oIdentificacionVehicular.AnioModeloVM = 2021;
            oAutotransporte.IdentificacionVehicular = oIdentificacionVehicular;

            CartaPorteMercanciasAutotransporteSeguros oSeguros = new CartaPorteMercanciasAutotransporteSeguros();
            oSeguros.AseguraRespCivil = "GNP";
            oSeguros.PolizaRespCivil = "000004517700945";
            oAutotransporte.Seguros = oSeguros;
            oMercancias.Autotransporte = oAutotransporte;

            oCartaPorte.Mercancias = oMercancias;


            List<CartaPorteTiposFigura> listTiposFigura = new List<CartaPorteTiposFigura>();
            CartaPorteTiposFigura oTiposFigura = new CartaPorteTiposFigura();
            oTiposFigura.TipoFigura = "01";
            oTiposFigura.RFCFigura = "MASR9411026I6";
            oTiposFigura.NumLicencia = "1N63236196";
            oTiposFigura.NombreFigura = "RICARDO MARTINEZ SILVA";
            listTiposFigura.Add(oTiposFigura);
            oCartaPorte.FiguraTransporte = listTiposFigura.ToArray();

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////


            string json = JsonConvert.SerializeObject(oCartaPorte);


            XmlDocument docCartaPorte = new XmlDocument();
            var namespaceCartaPorte = new XmlSerializerNamespaces();
            namespaceCartaPorte.Add("cartaporte30", "http://www.sat.gob.mx/CartaPorte30");

            using (XmlWriter writer = docCartaPorte.CreateNavigator().AppendChild())
            {
                new XmlSerializer(oCartaPorte.GetType()).Serialize(writer, oCartaPorte, namespaceCartaPorte);
            }

            /*************************************
             * Modificacion Alejandro 20/02/2024
             * ***********************************/
            //Pinto lo que voy a añador en Any, que es la carta porte en xml
            //Console.WriteLine(docCartaPorte.OuterXml);
            //Extraigo los tags Mercancia
            var merca = docCartaPorte.GetElementsByTagName("cartaporte30:Mercancia");
            //Checo cuantos trae
            //Console.WriteLine("Elementos cartaporte30:Mercancia: " + merca.Count);
            //si es uno, agrego al nodo el force para Array
            if (merca.Count == 1)
            {
                //Creo un atributo que se llama json:Array con el NS indicado y el valor true
                var attribute = docCartaPorte.CreateAttribute("json", "Array", "http://james.newtonking.com/projects/json");
                attribute.InnerText = "true";
                //Se lo añado al item (0) porque solo hay uno)
                var node = merca.Item(0) as XmlElement;
                //Y lo añadimos
                node.Attributes.Append(attribute);
            }
            //Al agregar el doc carta porte al Objeto otro, no se perderá que es documento.
            ComprobanteComplemento oComplemento = new ComprobanteComplemento();
            oComplemento.Any = new XmlElement[]
            {
                docCartaPorte.DocumentElement
            };

            oComprobante.Complemento = oComplemento;

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore, 
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy() 
                }
            };
            json = JsonConvert.SerializeObject(oComprobante, Newtonsoft.Json.Formatting.Indented, settings);
            json = json.Replace("@", "");
            json = json.Replace("cartaporte30:", "");


            /*
            //Mandar json a Web API FAE MX
            await ApiRquest(oComprobante);     
            */

            return oComprobante;
        }


        private static void CreateXML(Comprobante oComprobante, string xmlFilePath)
        {
            var xmlNamespaceManager = new XmlSerializerNamespaces();
            xmlNamespaceManager.Add("cfdi", "http://www.sat.gob.mx/cfd/4");
            xmlNamespaceManager.Add("xs", "http://www.w3.org/2001/XMLSchema");
            xmlNamespaceManager.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");

            if (oComprobante.TipoDeComprobante != "T")
            {
                xmlNamespaceManager.Add("tfd", "http://www.sat.gob.mx/TimbreFiscalDigital");
            }

            if (oComprobante.Complemento.Any.Count() > 0)
            {

                xmlNamespaceManager.Add("cartaporte30", "http://www.sat.gob.mx/CartaPorte30");

            }


            string xmlContent = "";
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Comprobante));

            using (var stringWriter = new StringWriterWithEncoding(Encoding.UTF8))
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter))
                {
                    xmlSerializer.Serialize(xmlWriter, oComprobante, xmlNamespaceManager);
                    xmlContent = stringWriter.ToString();
                }
            }

            string directoryPath = Path.GetDirectoryName(xmlFilePath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string fullFilePath = Path.Combine(directoryPath, Path.GetFileName(xmlFilePath));

            try
            {
                System.IO.File.WriteAllText(fullFilePath, xmlContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al generar el XML: {ex}");
            }
        }

        private static async Task ApiRquest(Comprobante oComprobante)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                //Formatting = Newtonsoft.Json.Formatting.Indented
            };

            string jsonData = JsonConvert.SerializeObject(oComprobante, settings);

            string apiBaseUrl = "https://localhost:44385/api";
            string endpoint = "ComprobanteFiscal/Agregar";

            using (var apiClient = new ApiClient(apiBaseUrl))
            {
                var responseAPI = await apiClient.PostDatos(endpoint, jsonData);
                string responseBody = await responseAPI.Content.ReadAsStringAsync();

                if (responseAPI.IsSuccessStatusCode)
                {
                    var response = JsonConvert.DeserializeObject<FacturacionModel>(responseBody);
                    Console.WriteLine($"Respuesta exitosa: {response}");
                }
                else
                {
                    var response = JsonConvert.DeserializeObject<ContentResponse<FacturacionModel>>(responseBody) ?? new ContentResponse<FacturacionModel>
                    {
                        Message = "Respuesta inválida",
                        Success = false
                    };

                    Console.WriteLine($"Error en la respuesta: {response.Message}");
                }
            }
        }
    }




















}


