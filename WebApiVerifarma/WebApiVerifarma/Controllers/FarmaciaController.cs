using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpPost]
        public async Task<ActionResult<FarmaciaReadDTO>> Create(FarmaciaCreateDTO objectDto)
        {
            if (objectDto == null)
            {
                return BadRequest();
            }
            try
            {
                var newObject = await _objectService.CreateAsync(objectDto);
                return CreatedAtAction(nameof(Get), new { id = newObject.Id }, newObject);
            }
            catch (DbUpdateException ex)
            {
                // Manejar otros posibles errores de base de datos
                return StatusCode(500, "Error al guardar los cambios. Por favor, inténtalo de nuevo.");
            }
            catch (Exception ex)
            {
                // Capturar cualquier otra excepción
                return StatusCode(500, $"Ocurrió un error: {ex.Message}");
            }


        }

    }
}
