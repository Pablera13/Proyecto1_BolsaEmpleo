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
    public class OfertaController : ControllerBase
    {
        private readonly IOfertaService _ofertaService;

        public OfertaController(IOfertaService ofertaService)
        {
            _ofertaService = ofertaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Oferta>>> GetOferta()
        {
            List<Oferta> listOferta = await _ofertaService.GetAll();

            if (listOferta == null)
            {
                return NotFound();
            }

            return Ok(listOferta);
        }

        [HttpPost]
        public async Task<ActionResult<Oferta>> PostOferta(OfertaVm ofertaRequest)
        {

            if (ofertaRequest == null)
            {
                return BadRequest();
            }

            Oferta newOferta = await _ofertaService.Create(ofertaRequest);
            return CreatedAtAction("GetOferta", new { id = newOferta.Id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutOferta(int id, OfertaVm ofertaRequest)
        {
            if (ofertaRequest == null)
            {
                return BadRequest();
            }

            var candidato = await _ofertaService.GetById(id);

            if (candidato == null)
            {
                return NotFound();
            }

            await _ofertaService.Update(id, ofertaRequest);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOferta(int id)
        {
            var oferta = await _ofertaService.GetById(id);
            if (oferta == null)
            {
                return NotFound();
            }

            await _ofertaService.Delete(id);
            return NoContent();
        }

    }
}
