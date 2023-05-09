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
        public async Task<ActionResult<IEnumerable<OfertaHabilidadVm>>> GetOfertaHabilidad()
        {
            if (_context.OfertaHabilidad == null)
            {
                return NotFound();
            }

            //List<OfertaHabilidad> listaOfertaHabilidad = await _context.OfertaHabilidad.ToListAsync();

            //return listaOfertaHabilidad;

            List<OfertaHabilidad> listaOfertaHabilidad = await _context.OfertaHabilidad.ToListAsync();

            List<OfertaHabilidadVm> listaOfertaHabilidadVm = new List<OfertaHabilidadVm>();

            foreach (OfertaHabilidad ofertaHabilidad in listaOfertaHabilidad)
            {
                OfertaHabilidadVm newOfertaHabilidadVm = new OfertaHabilidadVm();
                newOfertaHabilidadVm.OfertaId = ofertaHabilidad.OfertaId;
                newOfertaHabilidadVm.HabilidadId = ofertaHabilidad.HabilidadId;
                listaOfertaHabilidadVm.Add(newOfertaHabilidadVm);
            }

            return listaOfertaHabilidadVm;
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

        [HttpDelete]
        public async Task<IActionResult> DeleteOfertaHabilidad(int id_oferta, int id_habilidad)
        {
            if (_context.OfertaHabilidad == null)
            {
                return NotFound();
            }

            OfertaHabilidad newOfertaHabilidad = new OfertaHabilidad();
            newOfertaHabilidad = _context.OfertaHabilidad.SingleOrDefault(pc => pc.OfertaId == id_oferta && pc.HabilidadId == id_habilidad);

            if (newOfertaHabilidad == null)
            {
                return NotFound();
            }

            _context.OfertaHabilidad.Remove(newOfertaHabilidad);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
