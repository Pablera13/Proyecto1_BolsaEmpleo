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

        public Task<OfertaVmGET2> GetById(int id);

        public Task<Oferta> Create(OfertaVm ofertavm);

        public Task Update(int id, OfertaVm ofertavm);

        public Task Delete(int id);
    }
}
