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
    public class CandidatoHabilidadController : Controller
    {
        private readonly ICandidatoHabilidadService _candidatohabilidadService;

        public CandidatoHabilidadController(ICandidatoHabilidadService candidatohabilidadService)
        {
            _candidatohabilidadService = candidatohabilidadService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<CandidatoHabilidadVm>>> GetCandidatoHabilidad()
        {
            List<CandidatoHabilidadVm> listCandidatoHabilidadVm = await _candidatohabilidadService.GetAll();

            if (listCandidatoHabilidadVm == null)
            {
                return NotFound();
            }

            return Ok(listCandidatoHabilidadVm);
        }

        [HttpPost]
        public async Task<ActionResult<CandidatoHabilidad>> PostCandidatoHabilidad(CandidatoHabilidadVm candidatohabilidadRequest)
        {
            if (candidatohabilidadRequest == null)
            {
                return BadRequest();
            }

            CandidatoHabilidad newCandidatoHabilidad = await _candidatohabilidadService.Create(candidatohabilidadRequest);

            return CreatedAtAction("GetCandidatoHabilidad", new { id = newCandidatoHabilidad.CandidatoId }, newCandidatoHabilidad);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCandidatoHabilidad(int id_candidato, int id_habilidad)
        {
            var candidatohabilidad = await _candidatohabilidadService.GetById(id_candidato, id_habilidad);
            if (candidatohabilidad == null)
            {
                return NotFound();
            }

            await _candidatohabilidadService.Delete(id_candidato, id_habilidad);
            return NoContent();
        }
    }
}
