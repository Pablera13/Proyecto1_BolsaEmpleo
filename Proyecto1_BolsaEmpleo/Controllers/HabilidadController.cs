using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto1_BolsaEmpleo.Data;
using Proyecto1_BolsaEmpleo.Models;
using Proyecto1_BolsaEmpleo.RequestObjects;

namespace Proyecto1_BolsaEmpleo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class HabilidadController : ControllerBase
    {

        private readonly MyApiContext _context;

        public HabilidadController(MyApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Habilidad>>> GetHabilidad()
        {
            if (_context.Habilidad == null)
            {
                return NotFound();
            }

            List<Habilidad> listaHabilidad = await _context.Habilidad.ToListAsync();

            return listaHabilidad;
        }

        [HttpPost]
        public async Task<ActionResult<Habilidad>> PostHabilidad(HabilidadVm habilidadRequest)
        {
            if (_context.Habilidad == null)
            {
                return Problem("Entity set 'MyApiContext.Habilidad'  is null.");
            }

            Habilidad newHabilidad = new Habilidad();
            newHabilidad.Id = habilidadRequest.Id;
            newHabilidad.Nombre = habilidadRequest.Nombre;
         
            _context.Habilidad.Add(newHabilidad);
            await _context.SaveChangesAsync();

            return CreatedAtAction("PostHabilidad", new { id = newHabilidad.Id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutHabilidad(int id, Habilidad habilidad)
        {
            if (id != habilidad.Id)
            {
                return BadRequest();
            }

            _context.Entry(habilidad).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HabilidadExists(id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHabilidad(int id)
        {
            if (_context.Habilidad == null)
            {
                return NotFound();
            }
            var habilidad = await _context.Habilidad.FindAsync(id);
            if (habilidad == null)
            {
                return NotFound();
            }

            _context.Habilidad.Remove(habilidad);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool HabilidadExists(int id)
        {
            return (_context.Habilidad?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
