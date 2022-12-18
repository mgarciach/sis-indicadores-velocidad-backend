using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OSIPTEL.DomainDto.Layer;
using OSIPTEL.Service.Layer;

namespace OSIPTEL.Essiv.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TablaMaestraController : ControllerBase
    {
        private readonly ITablaMaestraService _tablaMaestraService;
        private readonly ILogger _logger;

        public TablaMaestraController(
            ITablaMaestraService tablaMaestraService,
            ILogger<TablaMaestraController> logger
        )
        {
            _tablaMaestraService = tablaMaestraService;
            _logger = logger;
        }

        [HttpGet("")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAllTablaMaestra()
        {
            try
            {
                var list = await _tablaMaestraService.GetAllTablaMaestra();
                if (list == null)
                {
                    return BadRequest("Acceso denegado");
                }
                else
                {
                    return Ok(list);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " - " + ex.InnerException);
                throw ex.InnerException;
            }
        }

        [HttpGet("mantenimiento")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GettablaManestraMantenimiento()
        {
            try
            {
                var list = await _tablaMaestraService.GetTablaMaestraMantenimiento();
                if (list == null)
                {
                    return BadRequest("Acceso denegado");
                }
                else
                {
                    return Ok(list);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " - " + ex.InnerException);
                throw ex.InnerException;
            }
        }

        [HttpGet("mantenimiento/{id}/detalle")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GettablaManestraDetalleMantenimiento([FromRoute] int id)
        {
            try
            {
                var list = await _tablaMaestraService.GetTablaMaestraDetalleMantenimiento(id);
                if (list == null)
                {
                    return BadRequest("Acceso denegado");
                }
                else
                {
                    return Ok(list);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " - " + ex.InnerException);
                throw ex.InnerException;
            }
        }

        [HttpPost("mantenimiento/{id}/detalle")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> InsertarTablaMaestraDetalle([FromRoute] int id, [FromBody] TablaMaestraDetalleMantRequestDto request)
        {
            try
            {
                request.IdTablaMaestra = id;
                await _tablaMaestraService.InsertarTablaMaestraDetalle(request);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " - " + ex.InnerException);
                throw ex.InnerException;
            }
        }

        [HttpPut("mantenimiento/{id}/detalle/{idTablaMaestraDetalle}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> ActualizarTablaMaestraDetalle([FromRoute] int idTablaMaestraDetalle, [FromBody] TablaMaestraDetalleMantRequestDto request)
        {
            try
            {
                request.IdTablaMaestraDetalle = idTablaMaestraDetalle;
                await _tablaMaestraService.ActualizarTablaMaestraDetalle(request);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " - " + ex.InnerException);
                throw ex.InnerException;
            }
        }

        [HttpDelete("mantenimiento/{id}/detalle/{idTablaMaestraDetalle}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> EliminarTablaMaestraDetalle([FromRoute] int idTablaMaestraDetalle)
        {
            try
            {
                await _tablaMaestraService.EliminarTablaMaestraDetalle(idTablaMaestraDetalle);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " - " + ex.InnerException);
                throw ex.InnerException;
            }
        }

    }
}
