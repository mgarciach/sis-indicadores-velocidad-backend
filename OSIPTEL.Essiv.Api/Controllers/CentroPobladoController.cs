using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OSIPTEL.DomainDto.Layer;
using OSIPTEL.Service.Layer;
using System.Text;
using System.Text.Json;

namespace OSIPTEL.Essiv.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class CentroPobladoController : ControllerBase
    {
        private readonly ICentroPobladoService _centroPobladoService;
        private readonly ILogger _logger;
        private static string cachePath = Path.Combine(Environment.CurrentDirectory, "Files/Cache", "centro-poblado.json");

        public CentroPobladoController(
            ICentroPobladoService centroPobladoService,
            ILogger<CentroPobladoController> logger
        )
        {
            _centroPobladoService = centroPobladoService;
            _logger = logger;
        }

        [HttpGet("")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAllCentroPoblado()
        {
            try
            {
                var cache = GetFromCache();

                if (cache != null)
                {
                    return Ok(cache);
                }

                var list = await _centroPobladoService.GetAllCentroPoblado();
                if (list == null)
                {
                    return BadRequest("Acceso denegado");
                }
                else
                {
                    this.GenerateCache(list);
                    return Ok(list);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " - " + ex.InnerException);
                throw ex.InnerException;
            }
        }

        private void GenerateCache(List<CentroPobladoDto> list)
        {
            string dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Cache");
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            if (!System.IO.File.Exists(cachePath))
            {
                System.IO.File.WriteAllText(cachePath, JsonSerializer.Serialize(list), Encoding.UTF8);

            }
        }

        private List<CentroPobladoDto>? GetFromCache()
        {
            if (!System.IO.File.Exists(cachePath))
            {
                return null;
            }

            ReadOnlySpan<byte> data = System.IO.File.ReadAllBytes(cachePath);

            ReadOnlySpan<byte> utf8Bom = new byte[] { 0xEF, 0xBB, 0xBF };

            if (data.StartsWith(utf8Bom))
            {
                data = data.Slice(utf8Bom.Length);
            }
            var strJson = Encoding.UTF8.GetString(data);
            return JsonSerializer.Deserialize<List<CentroPobladoDto>?>(strJson);
        }
    }
}
