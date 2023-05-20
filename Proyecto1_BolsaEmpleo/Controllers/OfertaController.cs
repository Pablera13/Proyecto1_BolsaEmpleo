using DataAccess.Models;
using DataAccess.RequestObjects;
using DataAccess.Response_Objects;
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
            List<OfertaVmGET> listOferta = await _ofertaService.GetAll();
            
            return listOferta != null ? Ok(listOferta) : NotFound();

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Oferta>> GetOferta(int id)
        {
            var oferta = await _ofertaService.GetById(id);
            
            return oferta != null ? Ok(oferta) : NotFound();

        }


        [HttpPost]
        public async Task<ActionResult<Oferta>> PostOferta(OfertaVm ofertavm)
        {

            if (ofertavm == null)
            {
                return BadRequest();
            }

            Oferta newOferta = await _ofertaService.Create(ofertavm);
            return CreatedAtAction("GetOferta", new { id = newOferta.Id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutOferta(int id, OfertaVm ofertavm)
        {
            if (ofertavm == null)
            {
                return BadRequest();
            }

            var candidato = await _ofertaService.GetById(id);

            if (candidato == null)
            {
                return NotFound();
            }

            await _ofertaService.Update(id, ofertavm);
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
