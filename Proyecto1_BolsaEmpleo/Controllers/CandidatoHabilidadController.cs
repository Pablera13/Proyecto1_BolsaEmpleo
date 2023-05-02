using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto1_BolsaEmpleo.Data;
using Proyecto1_BolsaEmpleo.Models;
using Proyecto1_BolsaEmpleo.RequestObjects;

namespace Proyecto1_BolsaEmpleo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatoHabilidadController : Controller
    {   
            private readonly MyApiContext _context;
            public CandidatoHabilidadController(MyApiContext context)
            {
                _context = context;
            }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CandidatoHabilidad>>> GetCandidatoHabilidad()
        {
            if (_context.CandidatoHabilidad == null)
            {
                return NotFound();
            }

            List<CandidatoHabilidad> listaCandidatoHabilidad = await _context.CandidatoHabilidad.ToListAsync();

            return listaCandidatoHabilidad;
        }

        [HttpPost]
        public async Task<ActionResult<CandidatoHabilidad>> PostCandidatoHabilidad(CandidatoHabilidadVm candidatohabilidadRequest)
        {
            CandidatoHabilidad newCandidatoHabilidad = new CandidatoHabilidad();
            newCandidatoHabilidad.CandidatoId = candidatohabilidadRequest.CandidatoId;
            newCandidatoHabilidad.HabilidadId = candidatohabilidadRequest.HabilidadId;

            if (_context.CandidatoHabilidad == null)
            {
                return Problem("Entity set 'MyApiContext.CandidatoHabilidad'  is null.");
            }
            _context.CandidatoHabilidad.Add(newCandidatoHabilidad);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCandidatoHabilidad", new { id = newCandidatoHabilidad.CandidatoId }, newCandidatoHabilidad);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCandidatoHabilidad(int id)
        {
            if (_context.CandidatoHabilidad == null)
            {
                return NotFound();
            }
            var candidatohabilidad = await _context.CandidatoHabilidad.FindAsync(id);
            if (candidatohabilidad == null)
            {
                return NotFound();
            }

            _context.CandidatoHabilidad.Remove(candidatohabilidad);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
