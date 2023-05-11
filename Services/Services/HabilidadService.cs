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
    public class HabilidadService : IHabilidadService
    {
        private readonly MyApiContext _context;

        public HabilidadService(MyApiContext context)
        {
            _context = context;
        }

        public async Task<List<HabilidadVm>> GetAll()
        {

            List<Habilidad> listaHabilidad = await _context.Habilidad.ToListAsync();

            List<HabilidadVm> listaHabilidadVm = new List<HabilidadVm>();

            foreach (Habilidad habilidad in listaHabilidad)
            {
                HabilidadVm newHabilidadVm = new HabilidadVm();
                newHabilidadVm.Id = habilidad.Id;
                newHabilidadVm.Nombre = habilidad.Nombre;
                listaHabilidadVm.Add(newHabilidadVm);
            }

            return listaHabilidadVm;
        }

        public async Task<Habilidad> GetById(int id)
        {
            var habilidad = await _context.Habilidad.FindAsync(id);

            return habilidad;
        }

        public async Task<Habilidad> Create(HabilidadVm habilidadRequest)
        {

            Habilidad newHabilidad = new Habilidad();
            newHabilidad.Id = habilidadRequest.Id;
            newHabilidad.Nombre = habilidadRequest.Nombre;

            _context.Habilidad.Add(newHabilidad);
            await _context.SaveChangesAsync();

            return newHabilidad;
        }

        public async Task Update(int id, HabilidadVm habilidadRequest)
        {
            Habilidad HabilidadEdit = await _context.Habilidad.FindAsync(id);

            HabilidadEdit.Nombre = habilidadRequest.Nombre;

            _context.Entry(HabilidadEdit).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {

            var habilidad = await _context.Habilidad.FindAsync(id);

            _context.Habilidad.Remove(habilidad);
            await _context.SaveChangesAsync();
        }


    }
}
