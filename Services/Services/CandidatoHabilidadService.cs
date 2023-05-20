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
    internal class CandidatoHabilidadService : ICandidatoHabilidadService
    {
        private readonly MyApiContext _context;

        public CandidatoHabilidadService(MyApiContext context)
        {
            _context = context;
        }

        public async Task<List<CandidatoHabilidadVm>> GetAll()
        {
            List<CandidatoHabilidad> listaCandidatoHabilidad = await _context.CandidatoHabilidad.ToListAsync();

            List<CandidatoHabilidadVm> listaHabilidadVm = new List<CandidatoHabilidadVm>();

            foreach (CandidatoHabilidad candidatoHabilidad in listaCandidatoHabilidad)
            {
                CandidatoHabilidadVm newCandidatoHabilidadVm = new CandidatoHabilidadVm();
                newCandidatoHabilidadVm.HabilidadId = candidatoHabilidad.HabilidadId;
                newCandidatoHabilidadVm.CandidatoId = candidatoHabilidad.CandidatoId;
                listaHabilidadVm.Add(newCandidatoHabilidadVm);
            }

            return listaHabilidadVm;
        }

        public async Task<CandidatoHabilidad> GetById(int id_candidato, int id_habilidad)
        {
            CandidatoHabilidad newCandidatoHabilidad = new CandidatoHabilidad();
            newCandidatoHabilidad = _context.CandidatoHabilidad.SingleOrDefault(pc => pc.CandidatoId == id_candidato && pc.HabilidadId == id_habilidad);

            return newCandidatoHabilidad;
        }
        public async Task<CandidatoHabilidad> Create(CandidatoHabilidadVm candidatohabilidadvm)
        {
            CandidatoHabilidad newCandidatoHabilidad = new CandidatoHabilidad();
            newCandidatoHabilidad.CandidatoId = candidatohabilidadvm.CandidatoId;
            newCandidatoHabilidad.HabilidadId = candidatohabilidadvm.HabilidadId;

            _context.CandidatoHabilidad.Add(newCandidatoHabilidad);
            await _context.SaveChangesAsync();

            return newCandidatoHabilidad;
        }

        public async Task Delete(int id_candidato, int id_habilidad) 
        {
    
            CandidatoHabilidad newCandidatoHabilidad = new CandidatoHabilidad();
            newCandidatoHabilidad = _context.CandidatoHabilidad.SingleOrDefault(pc => pc.CandidatoId == id_candidato && pc.HabilidadId == id_habilidad);

            _context.CandidatoHabilidad.Remove(newCandidatoHabilidad);
            await _context.SaveChangesAsync();

        }



    }
}
