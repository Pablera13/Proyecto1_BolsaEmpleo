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

        public async Task<Candidato> Create(CandidatoVm candidatovm)
        {
            Candidato newCandidato = new Candidato();
            newCandidato.Id = candidatovm.Id; 
            newCandidato.Nombre = candidatovm.Nombre;
            newCandidato.Apellido1 = candidatovm.Apellido1;
            newCandidato.Apellido2 = candidatovm.Apellido2;
            newCandidato.Fecha_Nacimiento = candidatovm.Fecha_Nacimiento;
            newCandidato.Direccion = candidatovm.Direccion;
            newCandidato.Telefono = candidatovm.Telefono;
            newCandidato.Descripcion = candidatovm.Descripcion;

            _context.Candidato.Add(newCandidato);
            await _context.SaveChangesAsync();

            return newCandidato;

        }

        public async Task Update(int id, CandidatoVm candidatovm)
        {
            Candidato CandidatoEdit = await _context.Candidato.FindAsync(id);

            CandidatoEdit.Nombre = candidatovm.Nombre;
            CandidatoEdit.Apellido1 = candidatovm.Apellido1;
            CandidatoEdit.Apellido2 = candidatovm.Apellido2;
            CandidatoEdit.Fecha_Nacimiento = candidatovm.Fecha_Nacimiento;
            CandidatoEdit.Direccion = candidatovm.Direccion;
            CandidatoEdit.Telefono = candidatovm.Telefono;
            CandidatoEdit.Descripcion = candidatovm.Descripcion;

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
