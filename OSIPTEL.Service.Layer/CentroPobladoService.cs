using AutoMapper;
using Microsoft.Extensions.Logging;
using OSIPTEL.DomainDto;
using OSIPTEL.DomainDto.Layer;
using OSIPTEL.Persistence.Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSIPTEL.Service.Layer
{
    public interface ICentroPobladoService {
        Task<List<CentroPobladoDto>> GetAllCentroPoblado();
    }

    public class CentroPobladoService : ICentroPobladoService
    {
        private readonly IAplicacionCentroPobladoAdo _aplicacionCentroPobladoAdo;
        private readonly ILogger _logger;

        public CentroPobladoService(
            IAplicacionCentroPobladoAdo aplicacionCentroPobladoAdo,
            ILogger<CentroPobladoService> logger
            )
        {
            _aplicacionCentroPobladoAdo = aplicacionCentroPobladoAdo;
            _logger = logger;
        }
        public async Task<List<CentroPobladoDto>> GetAllCentroPoblado() {
            var result = new List<CentroPobladoDto>();
            try
            {
                //var entry = Mapper.Map<CentroPobladoDto>();
                result = Mapper.Map<List<CentroPobladoDto>>(
                    await _aplicacionCentroPobladoAdo.GetAllUsuarioServicio()
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
