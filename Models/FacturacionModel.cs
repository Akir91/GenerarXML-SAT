using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerarXML_SAT.Models
{
    public class FacturacionModel
    {
        public string IdentificadorFuente { get; set; } = null!;
        public string UUID { get; set; } = null!;
        public string XML { get; set; } = null!;
        public string PDF { get; set; } = null!;
        public string ReferenciaUnica { get; set; } = null!;
        public string RFC { get; set; } = null!;
        public string RFCReceptor { get; set; } = null!;
        public DateTime Fecha { get; set; }

    }
}
