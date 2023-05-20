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
    public class FormacionController : ControllerBase
    {
        private readonly IFormacionService _formacionService;

        public FormacionController(IFormacionService formacionServiceService)
        {
            _formacionService = formacionServiceService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Formacion>>> GetAll()
        {
            List<FormacionVm> listFormacion = await _formacionService.GetAll();
            
            return listFormacion != null ? Ok(listFormacion) : NotFound();

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Formacion>> GetFormacion(int id)
        {
            var formacion = await _formacionService.GetById2(id);
            
            return formacion != null ? Ok(formacion) : NotFound();

        }

        [HttpPost]
        public async Task<ActionResult<Formacion>> PostFormacion(FormacionVm formacionvm)
        {
            if (formacionvm == null)
            {
                return BadRequest();
            }

            Formacion newFormacion = await _formacionService.Create(formacionvm);

            return CreatedAtAction("PostFormacion", new { id = newFormacion.Id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutFormacion(int id, FormacionVm formacionvm)
        {
            if (formacionvm == null)
            {
                return BadRequest();
            }

            var formacion = await _formacionService.GetById(id);

            if (formacion == null)
            {
                return NotFound();
            }

            await _formacionService.Update(id, formacionvm);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFormacion(int id)
        {
            var formacion = await _formacionService.GetById(id);
            if (formacion == null)
            {
                return NotFound();
            }

            await _formacionService.Delete(id);
            return NoContent();
        }

    }
}
