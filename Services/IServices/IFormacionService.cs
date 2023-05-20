using DataAccess.Models;
using DataAccess.RequestObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IFormacionService
    {

        public Task<List<FormacionVm>> GetAll();
        public Task<Formacion> GetById(int id);
        public Task<FormacionVm> GetById2(int id);
        public Task<Formacion> Create(FormacionVm formacionvm);

        public Task Update(int id, FormacionVm formacionvm);

        public Task Delete(int id);
    }
}
