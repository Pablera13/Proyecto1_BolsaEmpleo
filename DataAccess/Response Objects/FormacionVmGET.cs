using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Response_Objects
{
    public class FormacionVmGET
    {
        public string Nombre { get; set; }
        public int Años_Estudio { get; set; }
        public string Fecha_Culminacion { get; set; }
    }
}
