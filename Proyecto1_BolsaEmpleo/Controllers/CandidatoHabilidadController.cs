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
        public async Task<ActionResult<IEnumerable<CandidatoHabilidadVm>>> GetCandidatoHabilidad()
        {
            if (_context.CandidatoHabilidad == null)
            {
                return NotFound();
            }

            //List<CandidatoHabilidad> listaCandidatoHabilidad = await _context.CandidatoHabilidad.ToListAsync();

            //return listaCandidatoHabilidad;

            List<CandidatoHabilidad> listaCandidatoHabilidad = await _context.CandidatoHabilidad.ToListAsync();

            List<CandidatoHabilidadVm> listaHabilidadVm = new List<CandidatoHabilidadVm>();

            foreach (CandidatoHabilidad candidatoHabilidad in listaCandidatoHabilidad)
            {
                CandidatoHabilidadVm newCandidatoHabilidadVm = new CandidatoHabilidadVm();
                newCandidatoHabilidadVm.HabilidadId = candidatoHabilidad.HabilidadId;
                newCandidatoHabilidadVm.CandidatoId = candidatoHabilidad.CandidatoId;
                listaHabilidadVm.Add(newCandidatoHabilidadVm);
            }

            return listaHabilidadVm;
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

        [HttpDelete]
        public async Task<IActionResult> DeleteCandidatoHabilidad(int id_candidato, int id_habilidad)
        {
            if (_context.CandidatoHabilidad == null)
            {
                return NotFound();
            }

            CandidatoHabilidad newCandidatoHabilidad = new CandidatoHabilidad();
            newCandidatoHabilidad = _context.CandidatoHabilidad.SingleOrDefault(pc => pc.CandidatoId == id_candidato && pc.HabilidadId == id_habilidad);

            if (newCandidatoHabilidad == null)
            {
                return NotFound();
            }

            _context.CandidatoHabilidad.Remove(newCandidatoHabilidad);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
