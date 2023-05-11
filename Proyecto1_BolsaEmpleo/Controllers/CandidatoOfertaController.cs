using DataAccess.Models;
using DataAccess.RequestObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.IServices;

namespace Proyecto1_BolsaEmpleo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatoOfertaController : Controller
    {

        private readonly ICandidatoOfertaService _candidatoofertaService;

        public CandidatoOfertaController(ICandidatoOfertaService candidatoofertaService)
        {
            _candidatoofertaService = candidatoofertaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CandidatoOfertaVm>>> GetCandidatoOferta()
        {
            List<CandidatoOfertaVm> listCandidatoofertaVm = await _candidatoofertaService.GetAll();

            if (listCandidatoofertaVm == null)
            {
                return NotFound();
            }

            return Ok(listCandidatoofertaVm);
        }

        [HttpPost]
        public async Task<ActionResult<CandidatoOferta>> PostCandidatoOferta(CandidatoOfertaVm candidatoofertaRequest)
        {
            if (candidatoofertaRequest == null)
            {
                return BadRequest();
            }

            CandidatoOferta newCandidatoOferta = await _candidatoofertaService.Create(candidatoofertaRequest);

            return CreatedAtAction("GetCandidatoOferta", new { id = newCandidatoOferta.CandidatoId }, newCandidatoOferta);

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCandidatoOferta(int id_candidato, int id_oferta)
        {
            var candidatooferta = await _candidatoofertaService.GetById(id_candidato, id_oferta);
            if (candidatooferta == null)
            {
                return NotFound();
            }

            await _candidatoofertaService.Delete(id_candidato, id_oferta);
            return NoContent();
        }


    }
}
