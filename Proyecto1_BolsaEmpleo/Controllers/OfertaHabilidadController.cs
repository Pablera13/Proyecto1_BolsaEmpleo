using DataAccess.Data;
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
    public class OfertaHabilidadController : Controller
    {
        private readonly IOfertaHabilidadService _candidatohabilidadService;

        public OfertaHabilidadController(IOfertaHabilidadService candidatohabilidadService)
        {
            _candidatohabilidadService = candidatohabilidadService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OfertaHabilidadVm>>> GetOfertaHabilidad()
        {
            List<OfertaHabilidadVm> listOfertaHabilidadVm = await _candidatohabilidadService.GetAll();
            
            return listOfertaHabilidadVm != null ? Ok(listOfertaHabilidadVm) : NotFound();

        }

        [HttpPost]
        public async Task<ActionResult<OfertaHabilidad>> PostOfertaHabilidad(OfertaHabilidadVm ofertahabilidadvm)
        {
            if (ofertahabilidadvm == null)
            {
                return BadRequest();
            }

            OfertaHabilidad newOfertaHabilidad = await _candidatohabilidadService.Create(ofertahabilidadvm);

            return CreatedAtAction("GetOfertaHabilidad", new { id = newOfertaHabilidad.OfertaId }, newOfertaHabilidad);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteOfertaHabilidad(int id_oferta, int id_habilidad)
        {
            var ofertahabilidad = await _candidatohabilidadService.GetById(id_oferta, id_habilidad);
            if (ofertahabilidad == null)
            {
                return NotFound();
            }

            await _candidatohabilidadService.Delete(id_oferta, id_habilidad);
            return NoContent();
        }

    }
}
