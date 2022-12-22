using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OSIPTEL.DomainDto.Layer;
using OSIPTEL.Service.Layer;

namespace OSIPTEL.Essiv.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<IActionResult> InsertarTablaMaestraDetalle([FromBody] ActaDto request)
        {
            try
            {
                await _actaMedicionService.InsertarActaMedicion(request);
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
