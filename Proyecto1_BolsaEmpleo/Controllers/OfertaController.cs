using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto1_BolsaEmpleo.Data;
using Proyecto1_BolsaEmpleo.Models;
using Proyecto1_BolsaEmpleo.RequestObjects;

namespace Proyecto1_BolsaEmpleo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfertaController : ControllerBase
    {
        private readonly MyApiContext _context;

        public OfertaController(MyApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Oferta>>> GetOferta()
        {
            if (_context.Oferta == null)
            {
                return NotFound();
            }

            List<Oferta> listaOfertas = await _context.Oferta
            //.Include(c => c.Empresa)
            .Select(c => new Oferta
            {
                Id = c.Id,
                Descripcion = c.Descripcion,
                EmpresaId = c.EmpresaId,
                OfertaHabilidades = c.OfertaHabilidades,
                CandidatoOfertas = c.CandidatoOfertas,
            })
                   .ToListAsync();


            //reunirse con el profe para preguntarle como hacer bien el select column

            return listaOfertas;
        }

        [HttpPost]
        public async Task<ActionResult<Oferta>> PostOferta(OfertaVm ofertaRequest)
        {
            if (_context.Oferta == null)
            {
                return Problem("Entity set 'MyApiContext.Oferta'  is null.");
            }

            Oferta newOferta = new Oferta();
            newOferta.Id = ofertaRequest.Id;
            newOferta.EmpresaId = ofertaRequest.EmpresaId;
            newOferta.Descripcion = ofertaRequest.Descripcion;


            _context.Oferta.Add(newOferta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("PostOferta", new { id = newOferta.Id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutOferta(int id, OfertaVm ofertaRequest)
        {

            Oferta OfertaEdit = await _context.Oferta.FindAsync(id);

            OfertaEdit.Descripcion = ofertaRequest.Descripcion;

            //if (id != oferta.Id)
            //{
            //    return BadRequest();
            //}

            _context.Entry(OfertaEdit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OfertaExists(id))
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
        public async Task<IActionResult> DeleteOferta(int id)
        {
            if (_context.Oferta == null)
            {
                return NotFound();
            }
            var oferta = await _context.Oferta.FindAsync(id);
            if (oferta == null)
            {
                return NotFound();
            }

            _context.Oferta.Remove(oferta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OfertaExists(int id)
        {
            return (_context.Oferta?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
