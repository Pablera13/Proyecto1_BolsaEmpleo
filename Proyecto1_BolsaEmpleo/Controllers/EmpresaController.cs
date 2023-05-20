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

            return listEmpresa != null ? Ok(listEmpresa) : NotFound();

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Empresa>> GetEmpresa(int id)
        {
            var empresa = await _empresaService.GetById(id);
            return empresa != null ? Ok(empresa) : NotFound();

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmpresa(int id, EmpresaVm empresavm)
        {
            if (empresavm == null)
            {
                return BadRequest();
            }

            var candidato = await _empresaService.GetById(id);

            if (candidato == null)
            {
                return NotFound();
            }

            await _empresaService.Update(id, empresavm);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Empresa>> PostAuthor(EmpresaVm empresavm)
        {

            if (empresavm == null)
            {
                return BadRequest();
            }

            Empresa newEmpresa = await _empresaService.Create(empresavm);

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
