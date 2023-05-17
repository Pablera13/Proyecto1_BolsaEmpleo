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

            if (listCandidato == null)
            {
                return NotFound();
            }

            return Ok(listCandidato);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Candidato>> GetCandidato(int id)
        {
            var candidato = await _candidatoService.GetById(id);
            if (candidato == null)
            {
                return NotFound();
            }

            return Ok(candidato);
        }

        [HttpPatch]
        public async Task<ActionResult<IEnumerable<Candidato>>> Ver_potenciales_candidatos(int id_oferta)
        {
            List<CandidatoVmGET> listCandidato = await _candidatoService.Ver_potenciales_candidatos(id_oferta);

            if (listCandidato == null)
            {
                return NotFound();
            }

            return Ok(listCandidato);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCandidato(int id, CandidatoVm candidatoRequest)
        {

            if (candidatoRequest == null)
            {
                return BadRequest();
            }

            var candidato = await _candidatoService.GetById(id);

            if (candidato == null)
            {
                return NotFound();
            }

            await _candidatoService.Update(id, candidatoRequest);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Candidato>> PostAuthor(CandidatoVm candidatoRequest)
        {
            if (candidatoRequest == null)
            {
                return BadRequest();
            }

            Candidato newCandidato = await _candidatoService.Create(candidatoRequest);

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
