using AutoMapper;
using Microsoft.Extensions.Logging;
using OSIPTEL.Domain.Layer;
using OSIPTEL.DomainDto.Layer;
using OSIPTEL.Persistence.Layer;

namespace OSIPTEL.Service.Layer
{
    public interface ITablaMaestraService
    {
        /// <summary>
        /// Obtiene todo el listado de tabla maestra detalle. Sirve para llenar combos en la aplicación.
        /// </summary>
        /// <returns></returns>
        Task<List<TablaMaestraDto>> GetAllTablaMaestra();

        /// <summary>
        /// Obtiene el listado de la tabla maestra. Se usa en la opción de mantenimiento
        /// </summary>
        /// <returns></returns>
        Task<List<TablaMaestraMantDto>> GetTablaMaestraMantenimiento();

        /// <summary>
        /// Obtiene el listado de detalle de cada tabla maestra.
        /// </summary>
        /// <param name="idTablaMaestra"></param>
        /// <returns></returns>
        Task<List<TablaMaestraDetalleMantDto>> GetTablaMaestraDetalleMantenimiento(int idTablaMaestra);

        /// <summary>
        /// Inserta un nuevo detalle en la tabla "TABLA_MAESTRA_DETALLE"
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task InsertarTablaMaestraDetalle(TablaMaestraDetalleMantRequestDto request);

        /// <summary>
        /// Actualiza un registro de la tabla TABLA_MAESTRA_DETALLE
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task ActualizarTablaMaestraDetalle(TablaMaestraDetalleMantRequestDto request);

        /// <summary>
        /// Elimina un registro de la tabla TABLA_MAESTRA_DETALLE
        /// </summary>
        /// <param name="idTablaMaestraDetalle"></param>
        /// <returns></returns>
        Task EliminarTablaMaestraDetalle(int idTablaMaestraDetalle, TablaMaestraDetalleMantRequestDto request);
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

        /// <summary>
        /// Obtiene todo el listado de tabla maestra detalle. Sirve para llenar combos en la aplicación.
        /// </summary>
        /// <returns></returns>
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


        /// <summary>
        /// Obtiene el listado de la tabla maestra. Se usa en la opción de mantenimiento
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Obtiene el listado de detalle de cada tabla maestra.
        /// </summary>
        /// <param name="idTablaMaestra"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Inserta un nuevo detalle en la tabla "TABLA_MAESTRA_DETALLE"
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Actualiza un registro de la tabla TABLA_MAESTRA_DETALLE
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Elimina un registro de la tabla TABLA_MAESTRA_DETALLE
        /// </summary>
        /// <param name="idTablaMaestraDetalle"></param>
        /// <returns></returns>
        public async Task EliminarTablaMaestraDetalle(int idTablaMaestraDetalle, TablaMaestraDetalleMantRequestDto request)
        {
            try
            {
                var entry = Mapper.Map<TablaMaestraDetalleMantRequest>(request);
                await _aplicacionTablaMaestraAdo.EliminarTablaMaestraDetalle(idTablaMaestraDetalle, entry);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
