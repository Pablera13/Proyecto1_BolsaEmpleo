using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto1_BolsaEmpleo.Data;
using Proyecto1_BolsaEmpleo.Models;
using Proyecto1_BolsaEmpleo.RequestObjects;

namespace Proyecto1_BolsaEmpleo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatoOfertaController : Controller
    {
            private readonly MyApiContext _context;
            public CandidatoOfertaController(MyApiContext context)
            {
                _context = context;
            }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CandidatoOferta>>> GetCandidatoOferta()
        {
            if (_context.CandidatoOferta == null)
            {
                return NotFound();
            }

            List<CandidatoOferta> listaCandidatoOferta = await _context.CandidatoOferta.ToListAsync();

            return listaCandidatoOferta;
        }

        [HttpPost]
        public async Task<ActionResult<CandidatoOferta>> PostCandidatoOferta(CandidatoOfertaVm candidatoofertaRequest)
        {
            CandidatoOferta newCandidatoOferta = new CandidatoOferta();
            newCandidatoOferta.CandidatoId = candidatoofertaRequest.CandidatoId;
            newCandidatoOferta.OfertaId = candidatoofertaRequest.OfertaId;

            if (_context.CandidatoOferta == null)
            {
                return Problem("Entity set 'MyApiContext.CandidatoOferta'  is null.");
            }
            _context.CandidatoOferta.Add(newCandidatoOferta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCandidatoOferta", new { id = newCandidatoOferta.CandidatoId }, newCandidatoOferta);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCandidatoOferta(int id)
        {
            if (_context.CandidatoOferta == null)
            {
                return NotFound();
            }
            var candidatooferta = await _context.CandidatoOferta.FindAsync(id);
            if (candidatooferta == null)
            {
                return NotFound();
            }

            _context.CandidatoOferta.Remove(candidatooferta);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
