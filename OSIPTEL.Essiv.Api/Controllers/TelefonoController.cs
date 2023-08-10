using Microsoft.AspNetCore.Authorization;
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
    public class TelefonoController : ControllerBase
    {
        private readonly ITelefonoService _telefonoService;
        private readonly ILogger _logger;
        private static string cachePath = Path.Combine(Environment.CurrentDirectory, "Files/Cache", "telefono.json");

        public TelefonoController(
            ITelefonoService telefonoService,
            ILogger<TelefonoController> logger
        )
        {
            _telefonoService = telefonoService;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene la lista de todos los teléfonos
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAllTelefono()
        {
            try
            {
                //var cache = GetFromCache();

                //if (cache != null)
                //{
                //    return Ok(cache);
                //}

                var list = await _telefonoService.GetAllTelefono();
                //if (list == null)
                //{
                //    return BadRequest("Acceso denegado");
                //}
                //else
                //{
                //    this.GenerateCache(list);
                return Ok(list);
                //}

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " - " + ex.InnerException);
                throw ex.InnerException;
            }
        }

        /// <summary>
        /// Genera la lista en cache, si no existe, para un acceso mas rapido
        /// </summary>
        /// <param name="list"></param>
        private void GenerateCache(List<TelefonoDto> list)
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

        /// <summary>
        /// Obtiene la lista que esta en cache, si existe.
        /// </summary>
        /// <param name="list"></param>
        private List<TelefonoDto>? GetFromCache()
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
            return JsonSerializer.Deserialize<List<TelefonoDto>?>(strJson);
        }

        /// <summary>
        /// Obtiene una lista de telefonos de forma paginada
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("paginar")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PaginarTelefono([FromQuery] PageTelefonoRequestDto request)
        {
            try
            {
                var list = await _telefonoService.PaginarTelefono(request);
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
        /// Actualiza un registro de telefono
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{idTelefonoCelular}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> ActualizarTelefono([FromRoute] int idTelefonoCelular, [FromBody] TelefonoRequestDto request)
        {
            try
            {
                request.IdTelefonoCelular = idTelefonoCelular;
                await _telefonoService.ActualizarTelefono(request);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " - " + ex.InnerException);
                throw ex.InnerException;
            }
        }

        /// <summary>
        /// Elimina un registro de telefono
        /// </summary>
        /// <param name="idTelefonoCelular"></param>
        /// <returns></returns>
        [HttpDelete("{idTelefonoCelular}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> EliminarTelefono([FromRoute] int idTelefonoCelular, [FromBody] TelefonoRequestDto request)
        {
            try
            {
                await _telefonoService.EliminarTelefono(idTelefonoCelular, request);
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
