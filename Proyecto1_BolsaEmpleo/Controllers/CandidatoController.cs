using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto1_BolsaEmpleo.Data;
using Proyecto1_BolsaEmpleo.Models;
using Proyecto1_BolsaEmpleo.RequestObjects;

namespace Proyecto1_BolsaEmpleo.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CandidatoController : ControllerBase
    {
        private readonly MyApiContext _context;
        public CandidatoController(MyApiContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Candidato>>> GetCandidato()
        {
            if (_context.Candidato == null)
            {
                return NotFound();
            }

            //List<Candidato> listaCandidatos = await _context.Candidato.Include("formaciones").ToListAsync();
            List<Candidato> listaCandidatos = await _context.Candidato.Include(c => c.formaciones).Include(c => c.habilidades).ToListAsync();


            return listaCandidatos;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Candidato>> GetCandidato(int id)
        {
            if (_context.Candidato == null)
            {
                return NotFound();
            }
            var candidato = await _context.Candidato.FindAsync(id);

            if (candidato == null)
            {
                return NotFound();
            }

            return candidato;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCandidato(int id, Candidato candidato)
        {
            if (id != candidato.Id)
            {
                return BadRequest();
            }

            _context.Entry(candidato).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CandidatoExists(id))
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
        public async Task<ActionResult<Candidato>> PostAuthor(CandidatoVm candidatoRequest)
        {

            Candidato newCandidato = new Candidato();
            newCandidato.Nombre = candidatoRequest.Nombre ;
            newCandidato.Apellido1 = candidatoRequest.Apellido1;
            newCandidato.Apellido2 = candidatoRequest.Apellido2;
            newCandidato.Fecha_Nacimiento = candidatoRequest.Fecha_Nacimiento;
            newCandidato.Direccion = candidatoRequest.Direccion;
            newCandidato.Telefono = candidatoRequest.Telefono;
            newCandidato.Descripcion = candidatoRequest.Descripcion;


            if (_context.Candidato == null)
            {
                return Problem("Entity set 'MyApiContext.Candidato'  is null.");
            }
            _context.Candidato.Add(newCandidato);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCandidato", new { id = newCandidato.Id }, newCandidato);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCandidato(int id)
        {
            if (_context.Candidato == null)
            {
                return NotFound();
            }
            var candidato = await _context.Candidato.FindAsync(id);
            if (candidato == null)
            {
                return NotFound();
            }

            _context.Candidato.Remove(candidato);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CandidatoExists(int id)
        {
            return (_context.Candidato?.Any(e => e.Id == id)).GetValueOrDefault();
        }







    }
}
