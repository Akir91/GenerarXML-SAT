using System.IO;
using System.Text;

namespace GenerarXML_SAT.Utilidades
{
    public class StringWriterWithEncoding : StringWriter
    {
        public StringWriterWithEncoding(Encoding encoding) : base()
        {
            this.m_Encoding = encoding;
        }
        private readonly Encoding m_Encoding;
        public override Encoding Encoding
        {
            get
            {
                return this.m_Encoding;
            }
        }
    }
}
