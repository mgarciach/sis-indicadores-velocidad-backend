using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OSIPTEL.DomainDto.Layer;
using OSIPTEL.Service.Layer;

namespace OSIPTEL.Essiv.Api.Controllers
{

    public class ReqActas
    {
        public List<ActaDto> Actas { get; set; }

    }
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ActaController : ControllerBase
    {

        private readonly IActaMedicionService _actaMedicionService;
        private readonly ILogger _logger;

        public ActaController(
            IActaMedicionService actaMedicionService,
            ILogger<TablaMaestraController> logger
        )
        {
            _actaMedicionService = actaMedicionService;
            _logger = logger;
        }

        [HttpPost("")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> InsertarActa([FromBody] ReqActas requestActas)
        {
            try
            {
                foreach (var request in requestActas.Actas)
                {
                    await _actaMedicionService.InsertarActaMedicion(request);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " - " + ex.InnerException);
                throw ex.InnerException;
            }
        }


        [HttpGet("")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAllActasIdsPorUsuario([FromQuery] string usuario)
        {
            try
            {
                var list = await _actaMedicionService.GetAllActasIdsPorUsuario(usuario);
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

    }

}
