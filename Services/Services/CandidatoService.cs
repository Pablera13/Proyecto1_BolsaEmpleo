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
    public class CandidatoService : ICandidatoService
    {
        private readonly MyApiContext _context;

        public CandidatoService(MyApiContext context)
        {
            _context = context;
        }

        public async Task<List<Candidato>> GetAll()
        {
            List<Candidato> listaCandidatos = await _context.Candidato
            .Include(c => c.formaciones)
            .Select(c => new Candidato
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Apellido1 = c.Apellido1,
                Apellido2 = c.Apellido2,
                Fecha_Nacimiento = c.Fecha_Nacimiento,
                Direccion = c.Direccion,
                Telefono = c.Telefono,
                Descripcion = c.Descripcion,
                CandidatoHabilidades = c.CandidatoHabilidades,
                CandidatoOfertas = c.CandidatoOfertas,


                formaciones = c.formaciones.Select(f => new Formacion
                {
                    Nombre = f.Nombre,
                    Años_Estudio = f.Años_Estudio,
                    Fecha_Culminacion = f.Fecha_Culminacion

                }).ToList(),
            })
                   .ToListAsync();


            //reunirse con el profe para preguntarle como hacer bien el select column

            return listaCandidatos;
        }

        public async Task<Candidato> GetById(int id)
        {
            var candidato = await _context.Candidato
           .Include(c => c.formaciones).Include(c => c.CandidatoHabilidades).Include(c => c.CandidatoOfertas)
           .FirstOrDefaultAsync(c => c.Id == id);

            return candidato;
        }

        public async Task<Candidato> Create(CandidatoVm candidatoRequest)
        {
            Candidato newCandidato = new Candidato();
            newCandidato.Id = candidatoRequest.Id; //Si se usa SQL server, no hay que llenar id en el swagger
            newCandidato.Nombre = candidatoRequest.Nombre;
            newCandidato.Apellido1 = candidatoRequest.Apellido1;
            newCandidato.Apellido2 = candidatoRequest.Apellido2;
            newCandidato.Fecha_Nacimiento = candidatoRequest.Fecha_Nacimiento;
            newCandidato.Direccion = candidatoRequest.Direccion;
            newCandidato.Telefono = candidatoRequest.Telefono;
            newCandidato.Descripcion = candidatoRequest.Descripcion;

            //if (_context.Candidato == null)
            //{
            //    return Problem("Entity set 'MyApiContext.Candidato'  is null.");
            //}

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
