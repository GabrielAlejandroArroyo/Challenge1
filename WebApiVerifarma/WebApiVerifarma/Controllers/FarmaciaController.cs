using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiVeriframa.Services.Interfaces;
using System.Text.Json;
using WebApiVerifarma.DTOs.Farmacia;
using Microsoft.AspNetCore.Authorization;

namespace WebApiVeriframa.Controllers
{
    //[Authorize] // TODO Falta aplicar authenticacion basica aplica sobre todo el endpoint
    [Route("api/[controller]")]
    [ApiController]
    public class FarmaciaController : ControllerBase
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(FarmaciaController));
        private readonly IFarmaciaService _objectService;

        public FarmaciaController(IFarmaciaService objectService)
        {
            _objectService = objectService;
        }


        // GET: api/Farmacia
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FarmaciaReadDTO>>> Get()
        {
            log.Info("Obteniendo todas Farmacia");
            var resultados = await _objectService.GetAllAsync();
            log.Info("Total Farmacias obtenidas : "+ resultados.Count() + "  - Farmacias :" + JsonSerializer.Serialize(resultados));
            return Ok(resultados);
        }

        // GET: api/Farmacia/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FarmaciaReadDTO>> Get(int id)
        {
            log.Info("Obteniendo la Farmacia con id = " + id);
            var resultado = await _objectService.GetByIdAsync(id);

            if (resultado == null)
            {
                log.Info("Farmacia con id= "+ id +" - No encontrada " );
                return NotFound();
            }
            log.Info("Farmacia con id= " + id + "  - Encontrada " + JsonSerializer.Serialize(resultado));
            return Ok(resultado);
        }

        // GET: api/FarmaciaMasCercana
        [HttpGet("FarmaciaMasCercana")]
        public async Task<FarmaciaReadDTO> GetFarmaciaMasCercana([FromQuery] decimal latitud, [FromQuery] decimal longitud)
        {
            log.Info("Obteniendo la Farmacia mas cercana latitud: " + latitud + " - Longitud: "+ longitud);
            var resultado = await _objectService.GetFarmaciaMasCercana(latitud, longitud);
            log.Info("Farmacia con id= " + resultado.Id + "  - Encontrada " + JsonSerializer.Serialize(resultado));
            return resultado;
        }

        [HttpPost]
        public async Task<ActionResult<FarmaciaReadDTO>> Create(FarmaciaCreateDTO objectDto)
        {
            if (objectDto == null)
            {
                log.Error("Error Creando farmacia");
                return BadRequest();
            }
            try
            {
                log.Info("Creando farmacia : " + JsonSerializer.Serialize(objectDto));
                var newObject = await _objectService.CreateAsync(objectDto);
                log.Info("Farmacia creada : " + JsonSerializer.Serialize(newObject));
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
