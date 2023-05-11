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
    internal class OfertaService : IOfertaService
    {
        private readonly MyApiContext _context;

        public OfertaService(MyApiContext context)
        {
            _context = context;
        }

        public async Task<List<Oferta>> GetAll()
        {
          
            List<Oferta> listaOfertas = await _context.Oferta
            .Include(c => c.Empresa)
            .Select(c => new Oferta
            {
                Id = c.Id,
                Descripcion = c.Descripcion,
                EmpresaId = c.EmpresaId,
                OfertaHabilidades = c.OfertaHabilidades,
                CandidatoOfertas = c.CandidatoOfertas,
            })
                   .ToListAsync();


            //reunirse con el profe para preguntarle como hacer bien el select column

            return listaOfertas;
        }

        public async Task<Oferta> GetById(int id)
        {
            var oferta = await _context.Oferta.FindAsync(id);

            return oferta;
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
