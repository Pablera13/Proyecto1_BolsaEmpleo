using DataAccess.Data;
using DataAccess.Models;
using DataAccess.RequestObjects;
using Microsoft.EntityFrameworkCore;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class FormacionService : IFormacionService
    {
        private readonly MyApiContext _context;

        public FormacionService(MyApiContext context)
        {
            _context = context;
        }

        public async Task<Formacion> GetById(int id)
        {
            var formacion = await _context.Formacion.FindAsync(id);

            return formacion;
        }

        public async Task<Formacion> Create(FormacionVm formacionRequest)
        {

            Formacion newFormacion = new Formacion();
            newFormacion.Id = formacionRequest.Id;
            newFormacion.CandidatoId = formacionRequest.CandidatoId;
            newFormacion.Nombre = formacionRequest.Nombre;
            newFormacion.Años_Estudio = formacionRequest.Años_Estudio;
            newFormacion.Fecha_Culminacion = formacionRequest.Fecha_Culminacion;

            _context.Formacion.Add(newFormacion);
            await _context.SaveChangesAsync();

            return newFormacion;
        }

        public async Task Update(int id, FormacionVm formacionRequest)
        {
            Formacion FormacionEdit = await _context.Formacion.FindAsync(id);

            FormacionEdit.Nombre = formacionRequest.Nombre;
            FormacionEdit.Años_Estudio = formacionRequest.Años_Estudio;
            FormacionEdit.Fecha_Culminacion = formacionRequest.Fecha_Culminacion;

            _context.Entry(FormacionEdit).State = EntityState.Modified;

            await _context.SaveChangesAsync();
      
        }

        public async Task Delete(int id)
        {

            var formacion = await _context.Formacion.FindAsync(id);

            _context.Formacion.Remove(formacion);
            await _context.SaveChangesAsync();
        }

    }
}
