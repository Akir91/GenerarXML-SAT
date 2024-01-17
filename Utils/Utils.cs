using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace GenerarXML_SAT.Utilidades
{
    public class Utils
    {
        public static string RegresarRutaApp()
        {
            string sRutaApp = Environment.CurrentDirectory;
            return sRutaApp;
        }
    }
}
