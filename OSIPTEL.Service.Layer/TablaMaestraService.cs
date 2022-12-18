using AutoMapper;
using Microsoft.Extensions.Logging;
using OSIPTEL.Domain.Layer;
using OSIPTEL.DomainDto.Layer;
using OSIPTEL.Persistence.Layer;

namespace OSIPTEL.Service.Layer
{
    public interface ITablaMaestraService
    {
        Task<List<TablaMaestraDto>> GetAllTablaMaestra();
        Task<List<TablaMaestraMantDto>> GetTablaMaestraMantenimiento();
        Task<List<TablaMaestraDetalleMantDto>> GetTablaMaestraDetalleMantenimiento(int idTablaMaestra);
        Task InsertarTablaMaestraDetalle(TablaMaestraDetalleMantRequestDto request);
        Task ActualizarTablaMaestraDetalle(TablaMaestraDetalleMantRequestDto request);
        Task EliminarTablaMaestraDetalle(int idTablaMaestraDetalle);
    }
    public class TablaMaestraService : ITablaMaestraService
    {
        private readonly IAplicacionTablaMaestraAdo _aplicacionTablaMaestraAdo;
        private readonly ILogger _logger;

        public TablaMaestraService(
            IAplicacionTablaMaestraAdo aplicacionTablaMaestraAdo,
            ILogger<TablaMaestraService> logger
        )
        {
            _aplicacionTablaMaestraAdo = aplicacionTablaMaestraAdo;
            _logger = logger;
        }

        public async Task<List<TablaMaestraDto>> GetAllTablaMaestra()
        {
            var result = new List<TablaMaestraDto>();
            try
            {
                //var entry = Mapper.Map<CentroPobladoDto>();
                result = Mapper.Map<List<TablaMaestraDto>>(
                    await _aplicacionTablaMaestraAdo.GetAllTablaMaestra()
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return result;
        }

        public async Task<List<TablaMaestraMantDto>> GetTablaMaestraMantenimiento()
        {
            var result = new List<TablaMaestraMantDto>();
            try
            {
                //var entry = Mapper.Map<CentroPobladoDto>();
                result = Mapper.Map<List<TablaMaestraMantDto>>(
                    await _aplicacionTablaMaestraAdo.GetTablaMaestraMantenimiento()
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return result;
        }
        public async Task<List<TablaMaestraDetalleMantDto>> GetTablaMaestraDetalleMantenimiento(int idTablaMaestra)
        {
            var result = new List<TablaMaestraDetalleMantDto>();
            try
            {
                //var entry = Mapper.Map<CentroPobladoDto>();
                result = Mapper.Map<List<TablaMaestraDetalleMantDto>>(
                    await _aplicacionTablaMaestraAdo.GetTablaMaestraDetalleMantenimiento(idTablaMaestra)
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return result;
        }

        public async Task InsertarTablaMaestraDetalle(TablaMaestraDetalleMantRequestDto request)
        {
            try
            {
                var entry = Mapper.Map<TablaMaestraDetalleMantRequest>(request);
                await _aplicacionTablaMaestraAdo.InsertarTablaMaestraDetalle(entry);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

        }

        public async Task ActualizarTablaMaestraDetalle(TablaMaestraDetalleMantRequestDto request)
        {
            try
            {
                var entry = Mapper.Map<TablaMaestraDetalleMantRequest>(request);
                await _aplicacionTablaMaestraAdo.ActualizarTablaMaestraDetalle(entry);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public async Task EliminarTablaMaestraDetalle(int idTablaMaestraDetalle)
        {
            try
            {
                await _aplicacionTablaMaestraAdo.EliminarTablaMaestraDetalle(idTablaMaestraDetalle);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
