using DataAccess.RequestObjects;
using DataAccess.Response_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IConsultaService 
    {
        public Task<List<CandidatoVmGET>> Ver_potenciales_candidatos(int id);

        public Task<List<OfertaVmGET>> Ver_potenciales_ofertas(int id);
    }
}
