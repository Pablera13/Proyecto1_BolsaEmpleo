using Microsoft.AspNetCore.Mvc;
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

        [HttpPost]
        public async Task<ActionResult<Habilidad>> PostHabilidad(HabilidadVm habilidadRequest)
        {
            if (_context.Habilidad == null)
            {
                return Problem("Entity set 'MyApiContext.Habilidad'  is null.");
            }

            Habilidad newHabilidad = new Habilidad();
            newHabilidad.Nombre = habilidadRequest.Nombre;
         
            _context.Habilidad.Add(newHabilidad);
            await _context.SaveChangesAsync();

            return CreatedAtAction("PostHabilidad", new { id = newHabilidad.Id });
        }



    }
}
