using AutoMapper;
using Microsoft.Extensions.Logging;
using OSIPTEL.DomainDto.Layer;
using OSIPTEL.Persistence.Layer;

namespace OSIPTEL.Service.Layer
{
    public interface ICuadriculaService
    {
        Task<List<CuadriculaDto>> GetAllCuadriculas();
    }
    public class CuadriculaService : ICuadriculaService
    {
        private readonly IAplicacionCuadriculaAdo _aplicacionCuadriculaAdo;
        private readonly ILogger _logger;

        public CuadriculaService(
            IAplicacionCuadriculaAdo aplicacionCentroPobladoAdo,
            ILogger<CentroPobladoService> logger
            )
        {
            _aplicacionCuadriculaAdo = aplicacionCentroPobladoAdo;
            _logger = logger;
        }
        public async Task<List<CuadriculaDto>> GetAllCuadriculas()
        {
            var result = new List<CuadriculaDto>();
            try
            {
                //var entry = Mapper.Map<CentroPobladoDto>();
                result = Mapper.Map<List<CuadriculaDto>>(
                    await _aplicacionCuadriculaAdo.GetAllCuadricula()
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
