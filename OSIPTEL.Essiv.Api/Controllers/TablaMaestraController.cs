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

        /// <summary>
        /// Obtiene todo el listado de tabla maestra detalle. Sirve para llenar combos en la aplicación.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Obtiene el listado de la tabla maestra. Se usa en la opción de mantenimiento
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Obtiene el listado de detalle de cada tabla maestra.
        /// </summary>
        /// <param name="idTablaMaestra"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Inserta un nuevo detalle en la tabla "TABLA_MAESTRA_DETALLE"
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Actualiza un registro de la tabla TABLA_MAESTRA_DETALLE
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Elimina un registro de la tabla TABLA_MAESTRA_DETALLE
        /// </summary>
        /// <param name="idTablaMaestraDetalle"></param>
        /// <returns></returns>
        [HttpDelete("mantenimiento/{id}/detalle/{idTablaMaestraDetalle}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> EliminarTablaMaestraDetalle([FromRoute] int idTablaMaestraDetalle, [FromBody] TablaMaestraDetalleMantRequestDto request)
        {
            try
            {
                await _tablaMaestraService.EliminarTablaMaestraDetalle(idTablaMaestraDetalle, request);
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
