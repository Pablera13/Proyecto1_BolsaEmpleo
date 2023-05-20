using DataAccess.Models;
using DataAccess.RequestObjects;
using DataAccess.Response_Objects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.IServices;

namespace Proyecto1_BolsaEmpleo.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CandidatoController : ControllerBase
    {
        private readonly ICandidatoService _candidatoService;

        public CandidatoController(ICandidatoService candidatoService)
        {
            _candidatoService = candidatoService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Candidato>>> GetAll()
        {
            List<CandidatoVmGET> listCandidato = await _candidatoService.GetAll();

            return listCandidato != null ? Ok(listCandidato) : NotFound();

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Candidato>> GetCandidato(int id)
        {
            var candidato = await _candidatoService.GetById(id);

            return (candidato == null) ? NotFound() : Ok(candidato);

        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutCandidato(int id, CandidatoVm candidatovm)
        {

            if (candidatovm == null)
            {
                return BadRequest();
            }

            var candidato = await _candidatoService.GetById(id);

            if (candidato == null)
            {
                return NotFound();
            }

            await _candidatoService.Update(id, candidatovm);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Candidato>> PostAuthor(CandidatoVm candidatovm)
        {
            if (candidatovm == null)
            {
                return BadRequest();
            }

            Candidato newCandidato = await _candidatoService.Create(candidatovm);

            return CreatedAtAction("GetCandidato", new { id = newCandidato.Id }, newCandidato);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCandidato(int id)
        {
            var candidato = await _candidatoService.GetById(id);
            if (candidato == null)
            {
                return NotFound();
            }

            await _candidatoService.Delete(id);
            return NoContent();
        }

    }
}
