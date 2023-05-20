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
    public class OfertaHabilidadService : IOfertaHabilidadService
    {
        private readonly MyApiContext _context;

        public OfertaHabilidadService(MyApiContext context)
        {
            _context = context;
        }

        public async Task<List<OfertaHabilidadVm>> GetAll()
        {

            List<OfertaHabilidad> listaOfertaHabilidad = await _context.OfertaHabilidad.ToListAsync();

            List<OfertaHabilidadVm> listaOfertaHabilidadVm = new List<OfertaHabilidadVm>();

            foreach (OfertaHabilidad ofertaHabilidad in listaOfertaHabilidad)
            {
                OfertaHabilidadVm newOfertaHabilidadVm = new OfertaHabilidadVm();
                newOfertaHabilidadVm.OfertaId = ofertaHabilidad.OfertaId;
                newOfertaHabilidadVm.HabilidadId = ofertaHabilidad.HabilidadId;
                listaOfertaHabilidadVm.Add(newOfertaHabilidadVm);
            }

            return listaOfertaHabilidadVm;
        }

        public async Task<OfertaHabilidad> GetById(int id_oferta, int id_habilidad)
        {
            OfertaHabilidad newOfertaHabilidad = new OfertaHabilidad();
            newOfertaHabilidad = _context.OfertaHabilidad.SingleOrDefault(pc => pc.OfertaId == id_oferta && pc.HabilidadId == id_habilidad);

            return newOfertaHabilidad;
        }

        public async Task<OfertaHabilidad> Create(OfertaHabilidadVm ofertahabilidadvm)
        {
            OfertaHabilidad newOfertaHabilidad = new OfertaHabilidad();
            newOfertaHabilidad.OfertaId = ofertahabilidadvm.OfertaId;
            newOfertaHabilidad.HabilidadId = ofertahabilidadvm.HabilidadId;

            _context.OfertaHabilidad.Add(newOfertaHabilidad);
            await _context.SaveChangesAsync();

            return newOfertaHabilidad;
        }


        public async Task Delete(int id_oferta, int id_habilidad)
        {        

            OfertaHabilidad newOfertaHabilidad = new OfertaHabilidad();
            newOfertaHabilidad = _context.OfertaHabilidad.SingleOrDefault(pc => pc.OfertaId == id_oferta && pc.HabilidadId == id_habilidad);         

            _context.OfertaHabilidad.Remove(newOfertaHabilidad);
            await _context.SaveChangesAsync();
        }

    }
}


