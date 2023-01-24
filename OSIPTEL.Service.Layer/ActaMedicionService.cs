using AutoMapper;
using Microsoft.Extensions.Logging;
using OSIPTEL.Domain.Layer;
using OSIPTEL.DomainDto.Layer;
using OSIPTEL.Persistence.Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSIPTEL.Service.Layer
{
    public interface IActaMedicionService
    {
        /// <summary>
        /// Inserta un registro de la tabla ACTA junto con 
        /// todas sus mediciones en la tabla MEDICION
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task InsertarActaMedicion(ActaDto request);

        /// <summary>
        /// Lista todos los ids de actas y sus mediciones por usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        Task<List<ActaIdsDto>> GetAllActasIdsPorUsuario(string usuario);

    }
    public class ActaMedicionService : IActaMedicionService
    {
        private readonly IAplicacionActaMedicionAdo _aplicacionActaMedicionAdo;
        private readonly ILogger _logger;

        public ActaMedicionService(
            IAplicacionActaMedicionAdo aplicacionActaMedicionAdo,
            ILogger<ActaMedicionService> logger
        )
        {
            _aplicacionActaMedicionAdo = aplicacionActaMedicionAdo;
            _logger = logger;
        }

        /// <summary>
        /// Inserta un registro de la tabla ACTA junto con 
        /// todas sus mediciones en la tabla MEDICION
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task InsertarActaMedicion(ActaDto request)
        {
            
            try
            {
                var entry = Mapper.Map<Acta>(request);
                var idActa = await _aplicacionActaMedicionAdo.InsertarActa(entry);

                if (idActa != null)
                {
                    await _aplicacionActaMedicionAdo.InsertarMediciones(idActa.Value, entry.Mediciones, request.Usuario);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

        }

        /// <summary>
        /// Lista todos los ids de actas y sus mediciones por usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public async Task<List<ActaIdsDto>> GetAllActasIdsPorUsuario(string usuario) {
            var result = new List<ActaIdsDto>();
            try
            {
                result = Mapper.Map<List<ActaIdsDto>>(
                    await _aplicacionActaMedicionAdo.GetAllActasIdsPorUsuario(usuario)
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
