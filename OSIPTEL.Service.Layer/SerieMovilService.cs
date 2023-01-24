using AutoMapper;
using Microsoft.Extensions.Logging;
using OSIPTEL.DomainDto.Layer;
using OSIPTEL.Persistence.Layer;

namespace OSIPTEL.Service.Layer
{
    public interface ISerieMovilService
    {
        /// <summary>
        /// Obtiene la lista de serie móvil válidos
        /// </summary>
        /// <returns></returns>
        Task<List<SerieMovilDto>> GetAllSerieMovil();
    }
    public class SerieMovilService: ISerieMovilService
    {
        private readonly IAplicacionSerieMovilAdo _aplicacionSerieMovilAdo;
        private readonly ILogger _logger;

        public SerieMovilService(
            IAplicacionSerieMovilAdo aplicacionSerieMovilAdo,
            ILogger<SerieMovilService> logger
        )
        {
            _aplicacionSerieMovilAdo = aplicacionSerieMovilAdo;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene la lista de serie móvil válidos
        /// </summary>
        /// <returns></returns>
        public async Task<List<SerieMovilDto>> GetAllSerieMovil()
        {
            var result = new List<SerieMovilDto>();
            try
            {
                //var entry = Mapper.Map<CentroPobladoDto>();
                result = Mapper.Map<List<SerieMovilDto>>(
                    await _aplicacionSerieMovilAdo.GetAllSerieMovil()
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return result;
        }
    }
}
