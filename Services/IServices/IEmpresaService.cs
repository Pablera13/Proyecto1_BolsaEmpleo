using DataAccess.Models;
using DataAccess.RequestObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IEmpresaService
    {
        public Task<List<Empresa>> GetAll();

        public Task<Empresa> GetById(int id);

        public Task<Empresa> Create(EmpresaVm empresaRequest);

        public Task Update(int id, EmpresaVm empresaRequest);

        public Task Delete(int id);
    }
}
