using DataAccess.Models;
using DataAccess.RequestObjects;
using DataAccess.Response_Objects;
using Microsoft.AspNetCore.Mvc;
using Services.IServices;

namespace Proyecto1_BolsaEmpleo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultaController : Controller
    {
        private readonly IConsultaService _consultaService;
        public ConsultaController(IConsultaService consultaService)
        {
            _consultaService = consultaService;
        }

        [HttpGet("Candidato/{id_oferta}")]
        public async Task<ActionResult<IEnumerable<Candidato>>> Ver_potenciales_candidatos(int id_oferta)
        {
            List<CandidatoVmGET> listCandidato = await _consultaService.Ver_potenciales_candidatos(id_oferta);

            if (listCandidato == null)
            {
                return NotFound();
            }

            return Ok(listCandidato);
        }

        [HttpGet("Oferta/{id_candidato}")]
        public async Task<ActionResult<IEnumerable<Oferta>>> Ver_potenciales_ofertas(int id_candidato)
        {
            List<OfertaVmGET> listOferta = await _consultaService.Ver_potenciales_ofertas(id_candidato);

            if (listOferta == null)
            {
                return NotFound();
            }

            return Ok(listOferta);
        }

    }
}
