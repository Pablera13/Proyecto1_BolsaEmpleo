using DataAccess.RequestObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Response_Objects
{
    public class OfertaVmGET2

    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Empresa { get; set; }
        public List<OfertaHabilidadVmGET> Habilidades { get; set; }
        public List<CandidatoVm> Candidatos { get; set; }

        public OfertaVmGET2()
        {
            Habilidades = new List<OfertaHabilidadVmGET>();
            Candidatos = new List<CandidatoVm>();
        }
    }
}
