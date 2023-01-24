using Microsoft.Extensions.Logging;
using OSIPTEL.Common.Layer;
using OSIPTEL.Domain.Layer;
using OSIPTEL.Persistence.Layer.Mapping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSIPTEL.Persistence.Layer
{

    public interface IAplicacionCoberturaAdo
    {
        /// <summary>
        /// Obtiene todo el listado de latabla COBERTURA
        /// </summary>
        /// <returns></returns>
        Task<List<Cobertura>> GetAllCobertura();
    }

    public class AplicacionCoberturaAdo : IAplicacionCoberturaAdo
    {
        private readonly IDbConnection _dbConnection;
        private readonly ILogger _logger;
        private readonly OracleHelper _oracleHelper;

        public AplicacionCoberturaAdo(
           IDbConnection dbConnection,
           ILogger<AplicacionCoberturaAdo> logger,
           OracleHelper oracleHelper
           )
        {
            _dbConnection = dbConnection;
            _logger = logger;
            _oracleHelper = oracleHelper;
        }

        /// <summary>
        /// Obtiene todo el listado de latabla COBERTURA
        /// </summary>
        /// <returns></returns>
        public async Task<List<Cobertura>> GetAllCobertura()
        {
            OracleConnection context = null;
            List<Cobertura> response = null;
            try
            {
                Environment.SetEnvironmentVariable("NLS_LANG", ".UTF8");
                using (context = new OracleConnection(_dbConnection.ConnectionString))
                {
                    using (OracleCommand cmd = new OracleCommand("ESIGAII.PKG_ESIGAII.SP_LISTAR_COBERTURA", context))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.Add(_oracleHelper.getParam("sUsuario", OracleType.VarChar, ParameterDirection.Input, model.UserName));
                        cmd.Parameters.Add(_oracleHelper.getParam("oCursor", OracleType.Cursor));

                        await context.OpenAsync();
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {

                            response = await MappingsDB.MapToValueListCobertura(_oracleHelper, reader);

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
