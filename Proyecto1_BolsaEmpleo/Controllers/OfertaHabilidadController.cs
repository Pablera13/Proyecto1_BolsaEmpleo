using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto1_BolsaEmpleo.Data;
using Proyecto1_BolsaEmpleo.Models;
using Proyecto1_BolsaEmpleo.RequestObjects;

namespace Proyecto1_BolsaEmpleo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfertaHabilidadController : Controller
    {
        private readonly MyApiContext _context;
        public OfertaHabilidadController(MyApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OfertaHabilidad>>> GetOfertaHabilidad()
        {
            if (_context.OfertaHabilidad == null)
            {
                return NotFound();
            }

            List<OfertaHabilidad> listaOfertaHabilidad = await _context.OfertaHabilidad.ToListAsync();

            return listaOfertaHabilidad;
        }

        [HttpPost]
        public async Task<ActionResult<OfertaHabilidad>> PostOfertaHabilidad(OfertaHabilidadVm ofertahabilidadRequest)
        {
            OfertaHabilidad newOfertaHabilidad = new OfertaHabilidad();
            newOfertaHabilidad.OfertaId = ofertahabilidadRequest.OfertaId;
            newOfertaHabilidad.HabilidadId = ofertahabilidadRequest.HabilidadId;

            if (_context.OfertaHabilidad == null)
            {
                return Problem("Entity set 'MyApiContext.OfertaHabilidad'  is null.");
            }
            _context.OfertaHabilidad.Add(newOfertaHabilidad);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOfertaHabilidad", new { id = newOfertaHabilidad.OfertaId }, newOfertaHabilidad);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOfertaHabilidad(int id)
        {
            if (_context.OfertaHabilidad == null)
            {
                return NotFound();
            }
            var ofertahabilidad = await _context.OfertaHabilidad.FindAsync(id);
            if (ofertahabilidad == null)
            {
                return NotFound();
            }

            _context.OfertaHabilidad.Remove(ofertahabilidad);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
