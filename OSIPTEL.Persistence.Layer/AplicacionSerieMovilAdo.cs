using Microsoft.Extensions.Logging;
using OSIPTEL.Common.Layer;
using OSIPTEL.Domain.Layer;
using OSIPTEL.Persistence.Layer.Mapping;
using System.Data;
using System.Data.OracleClient;

namespace OSIPTEL.Persistence.Layer
{
    public interface IAplicacionSerieMovilAdo
    {
        /// <summary>
        /// Obtiene la lista de serie móvil válidos
        /// </summary>
        /// <returns></returns>
        Task<List<SerieMovil>> GetAllSerieMovil();
    }

    public class AplicacionSerieMovilAdo : IAplicacionSerieMovilAdo
    {
        private readonly IDbConnection _dbConnection;
        private readonly ILogger _logger;
        private readonly OracleHelper _oracleHelper;

        public AplicacionSerieMovilAdo(
           IDbConnection dbConnection,
           ILogger<AplicacionSerieMovilAdo> logger,
           OracleHelper oracleHelper
           )
        {
            _dbConnection = dbConnection;
            _logger = logger;
            _oracleHelper = oracleHelper;
        }

        /// <summary>
        /// Obtiene la lista de serie móvil válidos
        /// </summary>
        /// <returns></returns>
        public async Task<List<SerieMovil>> GetAllSerieMovil()
        {
            OracleConnection context = null;
            List<SerieMovil> response = null;
            try
            {
                Environment.SetEnvironmentVariable("NLS_LANG", ".UTF8");
                using (context = new OracleConnection(_dbConnection.ConnectionString))
                {
                    using (OracleCommand cmd = new OracleCommand("ESIGAII.PKG_ESIGAII.SP_LISTAR_SERIE_MOVIL", context))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.Add(_oracleHelper.getParam("sUsuario", OracleType.VarChar, ParameterDirection.Input, model.UserName));
                        cmd.Parameters.Add(_oracleHelper.getParam("oCursor", OracleType.Cursor));

                        await context.OpenAsync();
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            response = await MappingsDB.MapToValueListSerieMovil(_oracleHelper, reader);

                            reader.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            finally
            {
                context.Close();
            }
            return response;
        }


    }
}
