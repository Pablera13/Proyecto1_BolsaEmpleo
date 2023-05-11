﻿using DataAccess.Models;
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

        public Task<Formacion> GetById(int id);

        public Task<Formacion> Create(FormacionVm formacionRequest);

        public Task Update(int id, FormacionVm formacionRequest);

        public Task Delete(int id);
    }
}
