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
            
            return listHabilidad != null ? Ok(listHabilidad) : NotFound();

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HabilidadVm>> GetHabilidad(int id)
        {
            var habilidad = await _habilidadService.GetById2(id);
            
            return habilidad != null ? Ok(habilidad) : NotFound();

        }

        [HttpPost]
        public async Task<ActionResult<Habilidad>> PostHabilidad(HabilidadVm habilidadvm)
        {
            if (habilidadvm == null)
            {
                return BadRequest();
            }

            Habilidad newHabilidad = await _habilidadService.Create(habilidadvm);

            return CreatedAtAction("GetHabilidad", new { id = newHabilidad.Id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutHabilidad(int id, HabilidadVm habilidadvm)
        {
            if (habilidadvm == null)
            {
                return BadRequest();
            }

            var habilidad = await _habilidadService.GetById(id);

            if (habilidad == null)
            {
                return NotFound();
            }

            await _habilidadService.Update(id, habilidadvm);
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
