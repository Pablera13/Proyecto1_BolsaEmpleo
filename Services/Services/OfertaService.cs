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

        public async Task<OfertaVmGET2> GetById(int id)
        {
            var oferta = await _context.Oferta
           .Include(c => c.CandidatoOfertas).Include(c => c.OfertaHabilidades)
           .FirstOrDefaultAsync(c => c.Id == id);

            if (oferta == null)
            {
                return null;
            }

            OfertaVmGET2 newOferta = new OfertaVmGET2();
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

            foreach (CandidatoOferta candidatoOfertas in oferta.CandidatoOfertas)
            {
                CandidatoVm newCandidatoVm = new CandidatoVm();

                Candidato candidato = await _context.Candidato
                .FirstOrDefaultAsync(c => c.Id == candidatoOfertas.CandidatoId);

                newCandidatoVm.Id = candidato.Id;
                newCandidatoVm.Nombre = candidato.Nombre;
                newCandidatoVm.Apellido1 = candidato.Apellido1;
                newCandidatoVm.Apellido2 = candidato.Apellido2;
                newCandidatoVm.Fecha_Nacimiento = candidato.Fecha_Nacimiento;
                newCandidatoVm.Direccion = candidato.Direccion;
                newCandidatoVm.Telefono = candidato.Telefono;
                newCandidatoVm.Descripcion = candidato.Descripcion;

                newOferta.Candidatos.Add(newCandidatoVm);

            }



            return newOferta;
        }

        
        public async Task<Oferta> Create(OfertaVm ofertavm)
        {

            Oferta newOferta = new Oferta();
            newOferta.Id = ofertavm.Id;
            newOferta.EmpresaId = ofertavm.EmpresaId;
            newOferta.Descripcion = ofertavm.Descripcion;

            _context.Oferta.Add(newOferta);
            await _context.SaveChangesAsync();

            return newOferta;
        }
        public async Task Update(int id, OfertaVm ofertavm)
        {
            Oferta OfertaEdit = await _context.Oferta.FindAsync(id);

            OfertaEdit.Descripcion = ofertavm.Descripcion;

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
