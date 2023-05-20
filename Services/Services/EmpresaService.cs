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
    public class EmpresaService : IEmpresaService
    {
        private readonly MyApiContext _context;
        public EmpresaService(MyApiContext context)
        {
            _context = context;
        }

        public async Task<List<EmpresaVmGET>> GetAll()
        {
            List<Empresa> listaEmpresa = await _context.Empresa
           .Include(o => o.ofertas)
           .ToListAsync();

            List<EmpresaVmGET> listaEmpresaVmGET = new List<EmpresaVmGET>();

            foreach (Empresa empresa in listaEmpresa)
            {
                EmpresaVmGET newEmpresa = new EmpresaVmGET();

                newEmpresa.Id = empresa.Id;
                newEmpresa.Nombre = empresa.Nombre;
                newEmpresa.Direccion = empresa.Direccion;
                newEmpresa.Telefono = empresa.Telefono;

                foreach (Oferta oferta in empresa.ofertas)
                {
                    OfertaVm newOferta = new OfertaVm();
                    newOferta.Id = oferta.Id;
                    newOferta.Descripcion = oferta.Descripcion;
                    newOferta.EmpresaId = newEmpresa.Id;

                    newEmpresa.Ofertas.Add(newOferta);

                }

                listaEmpresaVmGET.Add(newEmpresa);
            }

            return listaEmpresaVmGET;
        }

        public async Task<EmpresaVmGET> GetById(int id)
        {
            var empresa = await _context.Empresa
           .Include(c => c.ofertas)
           .FirstOrDefaultAsync(c => c.Id == id);

            if (empresa == null)
            {
                return null;
            }

            EmpresaVmGET newEmpresa = new EmpresaVmGET();

            newEmpresa.Id = empresa.Id;
            newEmpresa.Nombre = empresa.Nombre;
            newEmpresa.Direccion = empresa.Direccion;
            newEmpresa.Telefono = empresa.Telefono;

            foreach (Oferta oferta in empresa.ofertas)
            {
                OfertaVm newOferta = new OfertaVm();
                newOferta.Id = oferta.Id;
                newOferta.Descripcion = oferta.Descripcion;
                newOferta.EmpresaId = newEmpresa.Id;

                newEmpresa.Ofertas.Add(newOferta);

            }

            return newEmpresa;
        }
        public async Task<Empresa> Create(EmpresaVm empresavm)
        {
            Empresa newEmpresa = new Empresa();
            newEmpresa.Id = empresavm.Id;
            newEmpresa.Nombre = empresavm.Nombre;
            newEmpresa.Direccion = empresavm.Direccion;
            newEmpresa.Telefono = empresavm.Telefono;

            _context.Empresa.Add(newEmpresa);
            await _context.SaveChangesAsync();

            return newEmpresa;
        }
        public async Task Update(int id, EmpresaVm empresavm)
        {
            Empresa EmpresaEdit = await _context.Empresa.FindAsync(id);

            EmpresaEdit.Nombre = empresavm.Nombre;
            EmpresaEdit.Direccion = empresavm.Direccion;
            EmpresaEdit.Telefono = empresavm.Telefono;

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
