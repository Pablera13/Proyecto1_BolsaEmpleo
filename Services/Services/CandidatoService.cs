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
    public class CandidatoService : ICandidatoService
    {
        private readonly MyApiContext _context;

        public CandidatoService(MyApiContext context)
        {
            _context = context;
        }

        public async Task<List<CandidatoVmGET>> GetAll()
        {
            List<Candidato> listaCandidato = await _context.Candidato
           .Include(o => o.formaciones)
           .Include(o => o.CandidatoHabilidades)
           .Include(o => o.CandidatoOfertas)
           .ToListAsync();

            List<CandidatoVmGET> listaCandidatoVmGET = new List<CandidatoVmGET>();

            foreach (Candidato candidato in listaCandidato)
            {
                CandidatoVmGET newCandidato = new CandidatoVmGET();
                newCandidato.Id = candidato.Id;
                newCandidato.Nombre = candidato.Nombre;
                newCandidato.Apellido1 = candidato.Apellido1;
                newCandidato.Apellido2 = candidato.Apellido2;
                newCandidato.Fecha_Nacimiento = candidato.Fecha_Nacimiento;
                newCandidato.Direccion = candidato.Direccion;
                newCandidato.Telefono = candidato.Telefono;
                newCandidato.Descripcion = candidato.Descripcion;

                foreach (Formacion formacion in candidato.formaciones)
                {
                    FormacionVmGET newFormacion = new FormacionVmGET();
                    newFormacion.Nombre = formacion.Nombre;
                    newFormacion.Años_Estudio = formacion.Años_Estudio;
                    newFormacion.Fecha_Culminacion = formacion.Fecha_Culminacion;

                    newCandidato.Formaciones.Add(newFormacion);

                }

                foreach (CandidatoHabilidad candidatoHabilidad in candidato.CandidatoHabilidades)
                {
                    CandidatoHabilidadVmGET newCandidatoHabilidad = new CandidatoHabilidadVmGET();

                    Habilidad habilidad = await _context.Habilidad
                    .FirstOrDefaultAsync(c => c.Id == candidatoHabilidad.HabilidadId);

                    newCandidatoHabilidad.Nombre = habilidad.Nombre;

                    newCandidato.Habilidades.Add(newCandidatoHabilidad);

                }

                foreach (CandidatoOferta candidatoOferta in candidato.CandidatoOfertas)
                {
                    CandidatoOfertaVmGET newCandidatoOferta = new CandidatoOfertaVmGET();
                    newCandidatoOferta.OfertaId = candidatoOferta.OfertaId;

                    Oferta oferta = await _context.Oferta
                    .FirstOrDefaultAsync(c => c.Id == candidatoOferta.OfertaId);

                    newCandidatoOferta.Descripcion = oferta.Descripcion;

                    newCandidato.Ofertas.Add(newCandidatoOferta);

                }

                listaCandidatoVmGET.Add(newCandidato);
            }

            return listaCandidatoVmGET;

        }

        public async Task<CandidatoVmGET> GetById(int id)
        {
            var candidato = await _context.Candidato
           .Include(c => c.formaciones).Include(c => c.CandidatoHabilidades).Include(c => c.CandidatoOfertas)
           .FirstOrDefaultAsync(c => c.Id == id);

            if (candidato == null)
            {
               return null ;
            }

            CandidatoVmGET newCandidato = new CandidatoVmGET();
            newCandidato.Id = candidato.Id;
            newCandidato.Nombre = candidato.Nombre;
            newCandidato.Apellido1 = candidato.Apellido1;
            newCandidato.Apellido2 = candidato.Apellido2;
            newCandidato.Fecha_Nacimiento = candidato.Fecha_Nacimiento;
            newCandidato.Direccion = candidato.Direccion;
            newCandidato.Telefono = candidato.Telefono;
            newCandidato.Descripcion = candidato.Descripcion;

            foreach (Formacion formacion in candidato.formaciones)
            {
                FormacionVmGET newFormacion = new FormacionVmGET();
                newFormacion.Nombre = formacion.Nombre;
                newFormacion.Años_Estudio = formacion.Años_Estudio;
                newFormacion.Fecha_Culminacion = formacion.Fecha_Culminacion;

                newCandidato.Formaciones.Add(newFormacion);

            }

            foreach (CandidatoHabilidad candidatoHabilidad in candidato.CandidatoHabilidades)
            {
                CandidatoHabilidadVmGET newCandidatoHabilidad = new CandidatoHabilidadVmGET();

                Habilidad habilidad = await _context.Habilidad
                .FirstOrDefaultAsync(c => c.Id == candidatoHabilidad.HabilidadId);

                newCandidatoHabilidad.Nombre = habilidad.Nombre;

                newCandidato.Habilidades.Add(newCandidatoHabilidad);

            }

            foreach (CandidatoOferta candidatoOferta in candidato.CandidatoOfertas)
            {
                CandidatoOfertaVmGET newCandidatoOferta = new CandidatoOfertaVmGET();
                newCandidatoOferta.OfertaId = candidatoOferta.OfertaId;

                Oferta oferta = await _context.Oferta
                .FirstOrDefaultAsync(c => c.Id == candidatoOferta.OfertaId);

                newCandidatoOferta.Descripcion = oferta.Descripcion;

                newCandidato.Ofertas.Add(newCandidatoOferta);

            }


            return newCandidato;
        }

        public async Task<Candidato> Create(CandidatoVm candidatoRequest)
        {
            Candidato newCandidato = new Candidato();
            newCandidato.Id = candidatoRequest.Id; 
            newCandidato.Nombre = candidatoRequest.Nombre;
            newCandidato.Apellido1 = candidatoRequest.Apellido1;
            newCandidato.Apellido2 = candidatoRequest.Apellido2;
            newCandidato.Fecha_Nacimiento = candidatoRequest.Fecha_Nacimiento;
            newCandidato.Direccion = candidatoRequest.Direccion;
            newCandidato.Telefono = candidatoRequest.Telefono;
            newCandidato.Descripcion = candidatoRequest.Descripcion;

            _context.Candidato.Add(newCandidato);
            await _context.SaveChangesAsync();

            return newCandidato;

        }

        public async Task Update(int id, CandidatoVm candidatoRequest)
        {
            Candidato CandidatoEdit = await _context.Candidato.FindAsync(id);

            CandidatoEdit.Nombre = candidatoRequest.Nombre;
            CandidatoEdit.Apellido1 = candidatoRequest.Apellido1;
            CandidatoEdit.Apellido2 = candidatoRequest.Apellido2;
            CandidatoEdit.Fecha_Nacimiento = candidatoRequest.Fecha_Nacimiento;
            CandidatoEdit.Direccion = candidatoRequest.Direccion;
            CandidatoEdit.Telefono = candidatoRequest.Telefono;
            CandidatoEdit.Descripcion = candidatoRequest.Descripcion;

            _context.Entry(CandidatoEdit).State = EntityState.Modified;

            await _context.SaveChangesAsync();

        }

        public async Task Delete(int id)
        {
            var candidato = await _context.Candidato.FindAsync(id);

            _context.Candidato.Remove(candidato);
            await _context.SaveChangesAsync();
        }

    }
}
