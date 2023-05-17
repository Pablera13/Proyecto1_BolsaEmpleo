using DataAccess.Models;
using DataAccess.RequestObjects;
using DataAccess.Response_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IOfertaService
    {
        public Task<List<OfertaVmGET>> GetAll();

        public Task<Oferta> GetById(int id);
        public Task<List<OfertaVmGET>> Ver_potenciales_ofertas(int id);

        public Task<Oferta> Create(OfertaVm ofertaRequest);

        public Task Update(int id, OfertaVm ofertaRequest);

        public Task Delete(int id);
    }
}
