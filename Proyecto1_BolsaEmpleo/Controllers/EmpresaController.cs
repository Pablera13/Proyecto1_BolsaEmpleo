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
    public class EmpresaController : ControllerBase
    {
        private readonly IEmpresaService _empresaService;

        public EmpresaController(IEmpresaService empresaService)
        {
            _empresaService = empresaService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Empresa>>> GetEmpresa()
        {
            List<EmpresaVmGET> listEmpresa = await _empresaService.GetAll();

            if (listEmpresa == null)
            {
                return NotFound();
            }

            return Ok(listEmpresa);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Empresa>> GetEmpresa(int id)
        {
            var empresa = await _empresaService.GetById(id);
            if (empresa == null)
            {
                return NotFound();
            }

            return Ok(empresa);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmpresa(int id, EmpresaVm empresaRequest)
        {
            if (empresaRequest == null)
            {
                return BadRequest();
            }

            var candidato = await _empresaService.GetById(id);

            if (candidato == null)
            {
                return NotFound();
            }

            await _empresaService.Update(id, empresaRequest);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Empresa>> PostAuthor(EmpresaVm empresaRequest)
        {

            if (empresaRequest == null)
            {
                return BadRequest();
            }

            Empresa newEmpresa = await _empresaService.Create(empresaRequest);

            return CreatedAtAction("GetEmpresa", new { id = newEmpresa.Id }, newEmpresa);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpresa(int id)
        {
            var empresa = await _empresaService.GetById(id);
            if (empresa == null)
            {
                return NotFound();
            }

            await _empresaService.Delete(id);
            return NoContent();
        }


    }
}
