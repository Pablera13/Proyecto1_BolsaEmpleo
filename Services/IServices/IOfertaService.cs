using DataAccess.Models;
using DataAccess.RequestObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IOfertaService
    {
        public Task<List<Oferta>> GetAll();

        public Task<Oferta> GetById(int id);

        public Task<Oferta> Create(OfertaVm ofertaRequest);

        public Task Update(int id, OfertaVm ofertaRequest);

        public Task Delete(int id);
    }
}
