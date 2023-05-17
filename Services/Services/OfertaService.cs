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
    internal class OfertaService : IOfertaService
    {
        private readonly MyApiContext _context;

        public OfertaService(MyApiContext context)
        {
            _context = context;
        }

        public async Task<List<OfertaVmGET>> GetAll()
        {
            List<Oferta> listaOferta = await _context.Oferta
            .Include(o => o.OfertaHabilidades)
            .ToListAsync();

            List<OfertaVmGET> listaOfertaVmGET = new List<OfertaVmGET>();

            foreach (Oferta oferta in listaOferta)
            {
                OfertaVmGET newOferta = new OfertaVmGET();
                newOferta.Id = oferta.Id;
                newOferta.Descripcion = oferta.Descripcion;

                Empresa empresa = await _context.Empresa
               .FirstOrDefaultAsync(c => c.Id == oferta.EmpresaId);

                newOferta.Empresa = empresa.Nombre;

                foreach (OfertaHabilidad ofertaHabilidad in oferta.OfertaHabilidades)
                {
                    OfertaHabilidadVmGET newOfertaHabilidadVmGET = new OfertaHabilidadVmGET();

                    Habilidad habilidad = await _context.Habilidad
                    .FirstOrDefaultAsync(c => c.Id == ofertaHabilidad.HabilidadId);

                    newOfertaHabilidadVmGET.Nombre = habilidad.Nombre;

                    newOferta.Habilidades.Add(newOfertaHabilidadVmGET);

                }

                listaOfertaVmGET.Add(newOferta);
            }

            return listaOfertaVmGET;

        }

        public async Task<Oferta> GetById(int id)
        {
            var oferta = await _context.Oferta
           .Include(c => c.CandidatoOfertas).Include(c => c.OfertaHabilidades)
           .FirstOrDefaultAsync(c => c.Id == id);

            return oferta;
        }

        public async Task<List<OfertaVmGET>> Ver_potenciales_ofertas(int id_Candidato)
        {

            var candidato = await _context.Candidato
           .Include(c => c.CandidatoHabilidades).FirstOrDefaultAsync(c => c.Id == id_Candidato);

            var habilidades = candidato.CandidatoHabilidades.Select(ch => ch.HabilidadId).ToArray();

            List<Oferta> listaOferta = await _context.Oferta
           .Include(o => o.OfertaHabilidades)
           .ToListAsync();

            List<OfertaVmGET> listaOfertaVm = new List<OfertaVmGET>();

            foreach (var habilidadId in habilidades)
            {
                foreach (Oferta oferta in listaOferta)
                {
                    OfertaVmGET newOfertaVm = new OfertaVmGET();

                    Empresa empresa = await _context.Empresa
                    .FirstOrDefaultAsync(c => c.Id == oferta.EmpresaId);
                    
                    newOfertaVm.Id = oferta.Id;
                    newOfertaVm.Descripcion = oferta.Descripcion;
                    newOfertaVm.Empresa = empresa.Nombre;

                    foreach (OfertaHabilidad ofertahabilidad in oferta.OfertaHabilidades)
                    {
                        OfertaHabilidadVmGET newOfertaHabilidadVm = new OfertaHabilidadVmGET();

                        Habilidad habilidad = await _context.Habilidad
                        .FirstOrDefaultAsync(c => c.Id == ofertahabilidad.HabilidadId);

                        newOfertaHabilidadVm.Nombre = habilidad.Nombre;

                        newOfertaVm.Habilidades.Add(newOfertaHabilidadVm);

                        if (ofertahabilidad.HabilidadId == habilidadId)
                        {
                            listaOfertaVm.Add(newOfertaVm);
                        }
                    }
                }
            }    
            return listaOfertaVm;
        }

        public async Task<Oferta> Create(OfertaVm ofertaRequest)
        {

            Oferta newOferta = new Oferta();
            newOferta.Id = ofertaRequest.Id;
            newOferta.EmpresaId = ofertaRequest.EmpresaId;
            newOferta.Descripcion = ofertaRequest.Descripcion;

            _context.Oferta.Add(newOferta);
            await _context.SaveChangesAsync();

            return newOferta;
        }
        public async Task Update(int id, OfertaVm ofertaRequest)
        {
            Oferta OfertaEdit = await _context.Oferta.FindAsync(id);

            OfertaEdit.Descripcion = ofertaRequest.Descripcion;

            _context.Entry(OfertaEdit).State = EntityState.Modified;

            await _context.SaveChangesAsync();
          
        }

        public async Task Delete(int id)
        {
   
            var oferta = await _context.Oferta.FindAsync(id);

            _context.Oferta.Remove(oferta);
            await _context.SaveChangesAsync();

        }

    }
}
