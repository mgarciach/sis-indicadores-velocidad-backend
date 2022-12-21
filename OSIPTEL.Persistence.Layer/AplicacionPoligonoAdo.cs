using Microsoft.Extensions.Logging;
using OSIPTEL.Common.Layer;
using OSIPTEL.Domain.Layer;
using OSIPTEL.Persistence.Layer.Mapping;
using System.Data;
using System.Data.OracleClient;

namespace OSIPTEL.Persistence.Layer
{
    public interface IAplicacionPoligonoAdo
    {
        Task<List<Poligono>> GetAllPoligono();
    }

    public class AplicacionPoligonoAdo: IAplicacionPoligonoAdo
    {
        private readonly IDbConnection _dbConnection;
        private readonly ILogger _logger;
        private readonly OracleHelper _oracleHelper;

        public AplicacionPoligonoAdo(
           IDbConnection dbConnection,
           ILogger<AplicacionPoligonoAdo> logger,
           OracleHelper oracleHelper
           )
        {
            _dbConnection = dbConnection;
            _logger = logger;
            _oracleHelper = oracleHelper;
        }

        public async Task<List<Poligono>> GetAllPoligono()
        {
            OracleConnection context = null;
            List<Poligono> response = null;
            try
            {
                Environment.SetEnvironmentVariable("NLS_LANG", ".UTF8");
                using (context = new OracleConnection(_dbConnection.ConnectionString))
                {
                    using (OracleCommand cmd = new OracleCommand("ESIGAII.PKG_ESIGAII.SP_LISTAR_POLIGONOS", context))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.Add(_oracleHelper.getParam("sUsuario", OracleType.VarChar, ParameterDirection.Input, model.UserName));
                        cmd.Parameters.Add(_oracleHelper.getParam("oCursor", OracleType.Cursor));

                        await context.OpenAsync();
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {                           
                            
                            response = await MappingsDB.MapToValueListPoligono(_oracleHelper, reader);
                            
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
