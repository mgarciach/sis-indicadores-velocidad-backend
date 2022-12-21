using AutoMapper;
using Microsoft.Extensions.Logging;
using OSIPTEL.DomainDto.Layer;
using OSIPTEL.Persistence.Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSIPTEL.Service.Layer
{
    public interface ICoberturaService
    {
        Task<List<CoberturaDto>> GetAllCobertura();
    }
    public class CoberturaService : ICoberturaService
    {
        private readonly IAplicacionCoberturaAdo _aplicacionCoberturaAdo;
        private readonly ILogger _logger;

        public CoberturaService(
            IAplicacionCoberturaAdo aplicacionCoberturaAdo,
            ILogger<CoberturaService> logger
            )
        {
            _aplicacionCoberturaAdo = aplicacionCoberturaAdo;
            _logger = logger;
        }
        public async Task<List<CoberturaDto>> GetAllCobertura()
        {
            var result = new List<CoberturaDto>();
            try
            {
                //var entry = Mapper.Map<CentroPobladoDto>();
                result = Mapper.Map<List<CoberturaDto>>(
                    await _aplicacionCoberturaAdo.GetAllCobertura()
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
