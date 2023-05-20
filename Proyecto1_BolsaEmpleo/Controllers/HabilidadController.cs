using DataAccess.Models;
using DataAccess.RequestObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.IServices;
using Services.Services;

namespace Proyecto1_BolsaEmpleo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class HabilidadController : ControllerBase
    {

        private readonly IHabilidadService _habilidadService;

        public HabilidadController(IHabilidadService habilidadService)
        {
            _habilidadService = habilidadService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HabilidadVm>>> GetHabilidad()
        {
            List<HabilidadVm> listHabilidad = await _habilidadService.GetAll();

            if (listHabilidad == null)
            {
                return NotFound();
            }

            return Ok(listHabilidad);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HabilidadVm>> GetHabilidad(int id)
        {
            var habilidad = await _habilidadService.GetById2(id);
            if (habilidad == null)
            {
                return NotFound();
            }

            return Ok(habilidad);
        }

        [HttpPost]
        public async Task<ActionResult<Habilidad>> PostHabilidad(HabilidadVm habilidadRequest)
        {
            if (habilidadRequest == null)
            {
                return BadRequest();
            }

            Habilidad newHabilidad = await _habilidadService.Create(habilidadRequest);

            return CreatedAtAction("GetHabilidad", new { id = newHabilidad.Id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutHabilidad(int id, HabilidadVm habilidadRequest)
        {
            if (habilidadRequest == null)
            {
                return BadRequest();
            }

            var habilidad = await _habilidadService.GetById(id);

            if (habilidad == null)
            {
                return NotFound();
            }

            await _habilidadService.Update(id, habilidadRequest);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHabilidad(int id)
        {
            var habilidad = await _habilidadService.GetById(id);
            if (habilidad == null)
            {
                return NotFound();
            }

            await _habilidadService.Delete(id);
            return NoContent();
        }
        
    }
}
