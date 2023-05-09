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
        public async Task<ActionResult<IEnumerable<CandidatoOfertaVm>>> GetCandidatoOferta()
        {
            if (_context.CandidatoOferta == null)
            {
                return NotFound();
            }

            //List<CandidatoOferta> listaCandidatoOferta = await _context.CandidatoOferta.ToListAsync();

            //return listaCandidatoOferta;

            List<CandidatoOferta> listaCandidatoOferta = await _context.CandidatoOferta.ToListAsync();

            List<CandidatoOfertaVm> listaCandidatoOfertaVm = new List<CandidatoOfertaVm>();

            foreach (CandidatoOferta candidatoOferta in listaCandidatoOferta)
            {
                CandidatoOfertaVm newCandidatoOfertaVm = new CandidatoOfertaVm();
                newCandidatoOfertaVm.CandidatoId = candidatoOferta.CandidatoId;
                newCandidatoOfertaVm.OfertaId = candidatoOferta.OfertaId;
                listaCandidatoOfertaVm.Add(newCandidatoOfertaVm);
            }

            return listaCandidatoOfertaVm;
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

        [HttpDelete]
        public async Task<IActionResult> DeleteCandidatoOferta(int id_candidato, int id_oferta)
        {
            if (_context.CandidatoOferta == null)
            {
                return NotFound();
            }

            CandidatoOferta newCandidatoOferta = new CandidatoOferta();
            newCandidatoOferta = _context.CandidatoOferta.SingleOrDefault(pc => pc.CandidatoId == id_candidato && pc.OfertaId == id_oferta);

            if (newCandidatoOferta == null)
            {
                return NotFound();
            }

            _context.CandidatoOferta.Remove(newCandidatoOferta);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
