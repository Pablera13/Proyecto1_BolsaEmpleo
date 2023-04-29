using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto1_BolsaEmpleo.Data;
using Proyecto1_BolsaEmpleo.Models;
using Proyecto1_BolsaEmpleo.RequestObjects;

namespace Proyecto1_BolsaEmpleo.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class FormacionController : ControllerBase
    {
        private readonly MyApiContext _context;

        public FormacionController(MyApiContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Formacion>> PostFormacion(FormacionVm formacionRequest)
        {
            if (_context.Formacion == null)
            {
                return Problem("Entity set 'MyApiContext.Formacion'  is null.");
            }

            Formacion newFormacion = new Formacion();
            newFormacion.CandidatoId = formacionRequest.CandidatoId;
            newFormacion.Nombre = formacionRequest.Nombre;
            newFormacion.Años_Estudio = formacionRequest.Años_Estudio;
            newFormacion.Fecha_Culminacion = formacionRequest.Fecha_Culminacion;


            _context.Formacion.Add(newFormacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("PostFormacion", new { id = newFormacion.Id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutFormacion(int id, Formacion formacion)
        {
            if (id != formacion.Id)
            {
                return BadRequest();
            }

            _context.Entry(formacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FormacionExists(id))
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
        public async Task<IActionResult> DeleteFormacion(int id)
        {
            if (_context.Formacion == null)
            {
                return NotFound();
            }
            var formacion = await _context.Formacion.FindAsync(id);
            if (formacion == null)
            {
                return NotFound();
            }

            _context.Formacion.Remove(formacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FormacionExists(int id)
        {
            return (_context.Formacion?.Any(e => e.Id == id)).GetValueOrDefault();
        }








    }
}
