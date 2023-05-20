using DataAccess.Models;
using DataAccess.RequestObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IHabilidadService
    {
        public Task<List<HabilidadVm>> GetAll();

        public Task<Habilidad> GetById(int id);
        public Task<HabilidadVm> GetById2(int id);

        public Task<Habilidad> Create(HabilidadVm habilidadRequest);

        public Task Update(int id, HabilidadVm habilidadRequest);

        public Task Delete(int id);
    }
}
