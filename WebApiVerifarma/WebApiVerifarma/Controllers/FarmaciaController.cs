using Microsoft.AspNetCore.Mvc;
using WebApiVeriframa.DTOs;
using WebApiVeriframa.Models;
using WebApiVeriframa.Services.Implementation;
using WebApiVeriframa.Services.Interfaces;

namespace WebApiVeriframa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FarmaciaController : ControllerBase
    {
        private readonly IFarmaciaService _objectService;

        public FarmaciaController(IFarmaciaService objectService)
        {
            _objectService = objectService;
        }
        // GET: api/Farmacia
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FarmaciaReadDTO>>> Get()
        {
            var resultados = await _objectService.GetAllAsync();
            return Ok(resultados);
        }

        // GET: api/Farmacia/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FarmaciaReadDTO>> Get(int id)
        {
            var resultado = await _objectService.GetByIdAsync(id);

            if (resultado == null)
            {
                return NotFound();
            }

            return Ok(resultado);
        }

        // GET: api/FarmaciaMasCercana
        [HttpGet("FarmaciaMasCercana")]
        public async Task<FarmaciaReadDTO> GetFarmaciaMasCercana([FromQuery] decimal latitud, [FromQuery] decimal longitud)
        {
            var resultado = await _objectService.GetFarmaciaMasCercana(latitud, longitud);
            return resultado;
        }

    }
}
