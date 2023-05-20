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
    internal class CandidatoOfertaService : ICandidatoOfertaService
    {
        private readonly MyApiContext _context;

        public CandidatoOfertaService(MyApiContext context)
        {
            _context = context;
        }

        public async Task<List<CandidatoOfertaVm>> GetAll()
        {
        
            List<CandidatoOferta> listaCandidatoOferta = await _context.CandidatoOferta.ToListAsync();

            List<CandidatoOfertaVm> listaCandidatoOfertaVm = new List<CandidatoOfertaVm>();

            foreach (CandidatoOferta candidatoOferta in listaCandidatoOferta)
            {
                CandidatoOfertaVm newCandidatoOfertaVm = new CandidatoOfertaVm();
                newCandidatoOfertaVm.CandidatoId = candidatoOferta.CandidatoId;
                newCandidatoOfertaVm.OfertaId = candidatoOferta.OfertaId;
                listaCandidatoOfertaVm.Add(newCandidatoOfertaVm);
            }

            return listaCandidatoOfertaVm;
        }

        public async Task<CandidatoOferta> GetById(int id_candidato, int id_oferta)
        {
            CandidatoOferta newCandidatoOferta = new CandidatoOferta();
            newCandidatoOferta = _context.CandidatoOferta.SingleOrDefault(pc => pc.CandidatoId == id_candidato && pc.OfertaId == id_oferta);

            return newCandidatoOferta;
        }

        public async Task<CandidatoOferta> Create(CandidatoOfertaVm candidatoofertavm)
        {
            CandidatoOferta newCandidatoOferta = new CandidatoOferta();
            newCandidatoOferta.CandidatoId = candidatoofertavm.CandidatoId;
            newCandidatoOferta.OfertaId = candidatoofertavm.OfertaId;

            _context.CandidatoOferta.Add(newCandidatoOferta);
            await _context.SaveChangesAsync();

            return newCandidatoOferta;

        }

        public async Task Delete(int id_candidato, int id_oferta)
        {
          
            CandidatoOferta newCandidatoOferta = new CandidatoOferta();
            newCandidatoOferta = _context.CandidatoOferta.SingleOrDefault(pc => pc.CandidatoId == id_candidato && pc.OfertaId == id_oferta);

            _context.CandidatoOferta.Remove(newCandidatoOferta);
            await _context.SaveChangesAsync();
        }
    }
}
