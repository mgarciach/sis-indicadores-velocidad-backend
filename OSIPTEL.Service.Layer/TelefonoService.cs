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
        Task<List<TelefonoDto>> GetAllTelefono();
        Task<PaginateResponse<TelefonoDto>> PaginarTelefono(PageTelefonoRequestDto request);

        Task ActualizarTelefono(TelefonoRequestDto request);

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
        public async Task<List<TelefonoDto>> GetAllTelefono() {
            var result = new List<TelefonoDto>();
            try
            {
                //var entry = Mapper.Map<CentroPobladoDto>();
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
