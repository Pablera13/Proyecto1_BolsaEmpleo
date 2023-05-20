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
    public interface ICandidatoService
    {
        public Task<List<CandidatoVmGET>> GetAll();

        public Task<CandidatoVmGET> GetById(int id);

        public Task<Candidato> Create(CandidatoVm candidatoRequest);

        public Task Update(int id, CandidatoVm candidatoRequest);

        public Task Delete(int id);

    }
}
