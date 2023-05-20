using DataAccess.Data;
using DataAccess.Models;
using DataAccess.RequestObjects;
using DataAccess.Response_Objects;
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

        public async Task<HabilidadVm> GetById2(int id)
        {
            var habilidad = await _context.Habilidad
           .FirstOrDefaultAsync(c => c.Id == id);

            if (habilidad == null)
            {
                return null;
            }

            HabilidadVm newHabilidad = new HabilidadVm();

            newHabilidad.Id = habilidad.Id;
            newHabilidad.Nombre = habilidad.Nombre;

            return newHabilidad;

        }

            public async Task<Habilidad> Create(HabilidadVm habilidadvm)
        {

            Habilidad newHabilidad = new Habilidad();
            newHabilidad.Id = habilidadvm.Id;
            newHabilidad.Nombre = habilidadvm.Nombre;

            _context.Habilidad.Add(newHabilidad);
            await _context.SaveChangesAsync();

            return newHabilidad;
        }

        public async Task Update(int id, HabilidadVm habilidadvm)
        {
            Habilidad HabilidadEdit = await _context.Habilidad.FindAsync(id);

            HabilidadEdit.Nombre = habilidadvm.Nombre;

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
