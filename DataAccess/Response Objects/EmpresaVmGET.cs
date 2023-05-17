using DataAccess.RequestObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Response_Objects
{
    public class EmpresaVmGET
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public int Telefono { get; set; }

        public List<OfertaVm> Ofertas { get; set; }

        public EmpresaVmGET()
        {
            Ofertas = new List<OfertaVm>();
        }
    }
}
