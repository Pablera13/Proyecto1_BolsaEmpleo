using DataAccess.Models;
using DataAccess.RequestObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IOfertaHabilidadService
    {
        public Task<List<OfertaHabilidadVm>> GetAll();
        public Task<OfertaHabilidad> GetById(int id_oferta, int id_habilidad);
        public Task<OfertaHabilidad> Create(OfertaHabilidadVm ofertahabilidadvm);
        public Task Delete(int id_oferta, int id_habilidad);
    }
}
