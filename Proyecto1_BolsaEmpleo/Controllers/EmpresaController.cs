using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto1_BolsaEmpleo.Data;
using Proyecto1_BolsaEmpleo.Models;
using Proyecto1_BolsaEmpleo.RequestObjects;

namespace Proyecto1_BolsaEmpleo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private readonly MyApiContext _context;
        public EmpresaController(MyApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Empresa>>> GetEmpresa()
        {
            if (_context.Empresa == null)
            {
                return NotFound();
            }

            //List<Empresa> listaEmpresas = await _context.Empresa.Include("ofertas").ToListAsync();
            List<Empresa> listaEmpresas = await _context.Empresa.Include(c => c.ofertas).Include(c => c.habilidades).ToListAsync();

            return listaEmpresas;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Empresa>> GetEmpresa(int id)
        {
            if (_context.Empresa == null)
            {
                return NotFound();
            }
            var empresa = await _context.Empresa.FindAsync(id);

            if (empresa == null)
            {
                return NotFound();
            }

            return empresa;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmpresa(int id, Empresa empresa)
        {
            if (id != empresa.Id)
            {
                return BadRequest();
            }

            _context.Entry(empresa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpresaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Empresa>> PostAuthor(EmpresaVm empresaRequest)
        {

            Empresa newEmpresa = new Empresa();
            newEmpresa.Nombre = empresaRequest.Nombre;
            newEmpresa.Direccion = empresaRequest.Direccion;
            newEmpresa.Telefono = empresaRequest.Telefono;


            if (_context.Empresa == null)
            {
                return Problem("Entity set 'MyApiContext.Empresa'  is null.");
            }
            _context.Empresa.Add(newEmpresa);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmpresa", new { id = newEmpresa.Id }, newEmpresa);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpresa(int id)
        {
            if (_context.Empresa == null)
            {
                return NotFound();
            }
            var empresa = await _context.Empresa.FindAsync(id);
            if (empresa == null)
            {
                return NotFound();
            }

            _context.Empresa.Remove(empresa);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmpresaExists(int id)
        {
            return (_context.Empresa?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
