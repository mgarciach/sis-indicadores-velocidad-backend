using AutoMapper;
using Microsoft.Extensions.Logging;
using OSIPTEL.DomainDto.Layer;
using OSIPTEL.Persistence.Layer;

namespace OSIPTEL.Service.Layer
{
    public interface IPoligonoService
    {
        Task<List<PoligonoDto>> GetAllPoligonos();
    }
    public class PoligonoService: IPoligonoService
    {
        private readonly IAplicacionPoligonoAdo _aplicacionPoligonoAdo;
        private readonly ILogger _logger;

        public PoligonoService(
            IAplicacionPoligonoAdo aplicacionPoligonoAdo,
            ILogger<PoligonoService> logger
            )
        {
            _aplicacionPoligonoAdo = aplicacionPoligonoAdo;
            _logger = logger;
        }
        public async Task<List<PoligonoDto>> GetAllPoligonos()
        {
            var result = new List<PoligonoDto>();
            try
            {
                //var entry = Mapper.Map<CentroPobladoDto>();
                result = Mapper.Map<List<PoligonoDto>>(
                    await _aplicacionPoligonoAdo.GetAllPoligono()
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
