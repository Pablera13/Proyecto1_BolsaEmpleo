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
    public class EmpresaService : IEmpresaService
    {
        private readonly MyApiContext _context;
        public EmpresaService(MyApiContext context)
        {
            _context = context;
        }

        public async Task<List<Empresa>> GetAll()
        {
         
            List<Empresa> listaEmpresas = await _context.Empresa
            .Include(c => c.ofertas)
            .Select(c => new Empresa
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Direccion = c.Direccion,
                Telefono = c.Telefono,

                ofertas = c.ofertas.Select(f => new Oferta
                {
                    Descripcion = f.Descripcion,
                    OfertaHabilidades = f.OfertaHabilidades,
                    CandidatoOfertas = f.CandidatoOfertas,

                }).ToList(),
            })
                   .ToListAsync();


            //reunirse con el profe para preguntarle como hacer bien el select column

            return listaEmpresas;
        }

        public async Task<Empresa> GetById(int id)
        {
              var empresa = await _context.Empresa
             .Include(c => c.ofertas)
             .FirstOrDefaultAsync(c => c.Id == id);

            return empresa;
        }
        public async Task<Empresa> Create(EmpresaVm empresaRequest)
        {
            Empresa newEmpresa = new Empresa();
            newEmpresa.Id = empresaRequest.Id;
            newEmpresa.Nombre = empresaRequest.Nombre;
            newEmpresa.Direccion = empresaRequest.Direccion;
            newEmpresa.Telefono = empresaRequest.Telefono;

            _context.Empresa.Add(newEmpresa);
            await _context.SaveChangesAsync();

            return newEmpresa;
        }
        public async Task Update(int id, EmpresaVm empresaRequest)
        {
            Empresa EmpresaEdit = await _context.Empresa.FindAsync(id);

            EmpresaEdit.Nombre = empresaRequest.Nombre;
            EmpresaEdit.Direccion = empresaRequest.Direccion;
            EmpresaEdit.Telefono = empresaRequest.Telefono;

            _context.Entry(EmpresaEdit).State = EntityState.Modified;
        
             await _context.SaveChangesAsync();         
        }
        public async Task Delete(int id)
        {
     
            var empresa = await _context.Empresa.FindAsync(id);

            _context.Empresa.Remove(empresa);
            await _context.SaveChangesAsync();
        }

    }
}
