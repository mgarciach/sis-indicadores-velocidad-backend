using AutoMapper;
using Microsoft.Extensions.Logging;
using OSIPTEL.Common.Layer;
using OSIPTEL.Domain.Layer;
using OSIPTEL.DomainDto.Layer;
using OSIPTEL.Persistence.Layer;

namespace OSIPTEL.Service.Layer
{
    public interface ITelefonoService
    {
        /// <summary>
        /// Obtiene la lista de todos los teléfonos
        /// </summary>
        /// <returns></returns>
        Task<List<TelefonoDto>> GetAllTelefono();

        /// <summary>
        /// Obtiene una lista de telefonos de forma paginada
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PaginateResponse<TelefonoDto>> PaginarTelefono(PageTelefonoRequestDto request);

        /// <summary>
        /// Actualiza un registro de telefono
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task ActualizarTelefono(TelefonoRequestDto request);

        /// <summary>
        /// Elimina un registro de telefono
        /// </summary>
        /// <param name="idTelefonoCelular"></param>
        /// <returns></returns>
        Task EliminarTelefono(int idTelefonoCelular);
    }
    public class TelefonoService : ITelefonoService
    {
        private readonly IAplicacionTelefonoAdo _aplicacionTelefonoAdo;
        private readonly ILogger _logger;

        public TelefonoService(
            IAplicacionTelefonoAdo aplicacionTelefonoAdo,
            ILogger<TelefonoService> logger
        )
        {
            _aplicacionTelefonoAdo = aplicacionTelefonoAdo;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene la lista de todos los teléfonos
        /// </summary>
        /// <returns></returns>
        public async Task<List<TelefonoDto>> GetAllTelefono() {
            var result = new List<TelefonoDto>();
            try
            {
                result = Mapper.Map<List<TelefonoDto>>(
                    await _aplicacionTelefonoAdo.GetAllTelefono()
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return result;
        }

        /// <summary>
        /// Obtiene una lista de telefonos de forma paginada
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PaginateResponse<TelefonoDto>> PaginarTelefono(PageTelefonoRequestDto request) {
            var result = new PaginateResponse<TelefonoDto>();
            try
            {
                var entry = Mapper.Map<PageTelefonoRequest>(request);
                result = Mapper.Map<PaginateResponse<TelefonoDto>>(
                    await _aplicacionTelefonoAdo.PaginarTelefono(entry)
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return result;
        }

        /// <summary>
        /// Actualiza un registro de telefono
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task ActualizarTelefono(TelefonoRequestDto request)
        {
            try
            {
                var entry = Mapper.Map<TelefonoRequest>(request);
                await _aplicacionTelefonoAdo.ActualizarTelefono(entry);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        /// <summary>
        /// Elimina un registro de telefono
        /// </summary>
        /// <param name="idTelefonoCelular"></param>
        /// <returns></returns>
        public async Task EliminarTelefono(int idTelefonoCelular)
        {
            try
            {
                await _aplicacionTelefonoAdo.EliminarTelefono(idTelefonoCelular);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
