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

            List<Candidato> listaCandidatos = await _context.Candidato
            .Include(c => c.formaciones)
            .Select(c => new Candidato
             {
                 Id = c.Id,
                 Nombre = c.Nombre,
                 Apellido1 = c.Apellido1,
                 Apellido2 = c.Apellido2,
                 Fecha_Nacimiento = c.Fecha_Nacimiento,
                 Direccion = c.Direccion,
                 Telefono = c.Telefono,
                 Descripcion = c.Descripcion,
                 CandidatoHabilidades = c.CandidatoHabilidades,
                 CandidatoOfertas = c.CandidatoOfertas,


                formaciones = c.formaciones.Select(f => new Formacion
                 {
                     Nombre = f.Nombre,
                     Años_Estudio = f.Años_Estudio,
                     Fecha_Culminacion = f.Fecha_Culminacion

                }).ToList(),
             })
                   .ToListAsync();


            //reunirse con el profe para preguntarle como hacer bien el select column

            return listaCandidatos;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Candidato>> GetCandidato(int id)
        {
            if (_context.Candidato == null)
            {
                return NotFound();
            }
              var candidato = await _context.Candidato
             .Include(c => c.formaciones).Include(c => c.CandidatoHabilidades).Include(c => c.CandidatoOfertas)
             .FirstOrDefaultAsync(c => c.Id == id);

            if (candidato == null)
            {
                return NotFound();
            }

            return candidato;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCandidato(int id, CandidatoVm candidatoRequest)
        {

            Candidato CandidatoEdit = await _context.Candidato.FindAsync(id);

            CandidatoEdit.Nombre = candidatoRequest.Nombre;
            CandidatoEdit.Apellido1 = candidatoRequest.Apellido1;
            CandidatoEdit.Apellido2 = candidatoRequest.Apellido2;
            CandidatoEdit.Fecha_Nacimiento = candidatoRequest.Fecha_Nacimiento;
            CandidatoEdit.Direccion = candidatoRequest.Direccion;
            CandidatoEdit.Telefono = candidatoRequest.Telefono;
            CandidatoEdit.Descripcion = candidatoRequest.Descripcion;

            //if (id != candidato.Id)
            //{
            //    return BadRequest();
            //}

            _context.Entry(CandidatoEdit).State = EntityState.Modified;

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
            newCandidato.Id = candidatoRequest.Id; //Si se usa SQL server, no hay que llenar id en el swagger
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
