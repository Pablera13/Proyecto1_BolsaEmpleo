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

        public async Task<List<FormacionVm>> GetAll()
        {

            List<Formacion> listaFormacion = await _context.Formacion.ToListAsync();

            List<FormacionVm> listaFormacionVm = new List<FormacionVm>();

            foreach (Formacion formacion in listaFormacion)
            {
                FormacionVm newFormacionVm = new FormacionVm();
                newFormacionVm.Id = formacion.Id;
                newFormacionVm.Nombre = formacion.Nombre;
                newFormacionVm.Años_Estudio = formacion.Años_Estudio;
                newFormacionVm.Fecha_Culminacion = formacion.Fecha_Culminacion;
                newFormacionVm.CandidatoId = formacion.CandidatoId;

                listaFormacionVm.Add(newFormacionVm);
            }

            return listaFormacionVm;
        }
        public async Task<Formacion> GetById(int id)
        {
            var formacion = await _context.Formacion.FindAsync(id);

            return formacion;
        }

        public async Task<FormacionVm> GetById2(int id)
        {
            var formacion = await _context.Formacion
           .FirstOrDefaultAsync(c => c.Id == id);

            if (formacion == null)
            {
                return null;
            }

            FormacionVm newFormacion = new FormacionVm();

            newFormacion.Id = formacion.Id;
            newFormacion.Nombre = formacion.Nombre;
            newFormacion.Años_Estudio = formacion.Años_Estudio;
            newFormacion.Fecha_Culminacion = formacion.Fecha_Culminacion;
            newFormacion.CandidatoId = formacion.CandidatoId;

            return newFormacion;

        }

        public async Task<Formacion> Create(FormacionVm formacionvm)
        {

            Formacion newFormacion = new Formacion();
            newFormacion.Id = formacionvm.Id;
            newFormacion.CandidatoId = formacionvm.CandidatoId;
            newFormacion.Nombre = formacionvm.Nombre;
            newFormacion.Años_Estudio = formacionvm.Años_Estudio;
            newFormacion.Fecha_Culminacion = formacionvm.Fecha_Culminacion;

            _context.Formacion.Add(newFormacion);
            await _context.SaveChangesAsync();

            return newFormacion;
        }

        public async Task Update(int id, FormacionVm formacionvm)
        {
            Formacion FormacionEdit = await _context.Formacion.FindAsync(id);

            FormacionEdit.Nombre = formacionvm.Nombre;
            FormacionEdit.Años_Estudio = formacionvm.Años_Estudio;
            FormacionEdit.Fecha_Culminacion = formacionvm.Fecha_Culminacion;

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
