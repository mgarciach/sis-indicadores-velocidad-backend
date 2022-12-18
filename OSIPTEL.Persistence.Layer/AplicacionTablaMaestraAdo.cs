using Microsoft.Extensions.Logging;
using OSIPTEL.Common.Layer;
using OSIPTEL.Domain.Layer;
using OSIPTEL.Persistence.Layer.Mapping;
using System.Data;
using System.Data.OracleClient;

namespace OSIPTEL.Persistence.Layer
{
    public interface IAplicacionTablaMaestraAdo
    {
        Task<List<TablaMaestra>> GetAllTablaMaestra();
        Task<List<TablaMaestraMant>> GetTablaMaestraMantenimiento();
        Task<List<TablaMaestraDetalleMant>> GetTablaMaestraDetalleMantenimiento(int idTablaMaestra);
        Task InsertarTablaMaestraDetalle(TablaMaestraDetalleMantRequest request);
        Task ActualizarTablaMaestraDetalle(TablaMaestraDetalleMantRequest request);
        Task EliminarTablaMaestraDetalle(int idTablaMaestraDetalle);
    }
    public class AplicacionTablaMaestraAdo : IAplicacionTablaMaestraAdo
    {
        private readonly IDbConnection _dbConnection;
        private readonly ILogger _logger;
        private readonly OracleHelper _oracleHelper;

        public AplicacionTablaMaestraAdo(
           IDbConnection dbConnection,
           ILogger<AplicacionTablaMaestraAdo> logger,
           OracleHelper oracleHelper
           )
        {
            _dbConnection = dbConnection;
            _logger = logger;
            _oracleHelper = oracleHelper;
        }

        public async Task<List<TablaMaestra>> GetAllTablaMaestra()
        {
            OracleConnection context = null;
            List<TablaMaestra> response = null;
            try
            {
                Environment.SetEnvironmentVariable("NLS_LANG", ".UTF8");
                using (context = new OracleConnection(_dbConnection.ConnectionString))
                {
                    using (OracleCommand cmd = new OracleCommand("ESSIV.PKG_ESSIV.SP_LISTAR_TABLAS_MAESTRAS", context))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.Add(_oracleHelper.getParam("sUsuario", OracleType.VarChar, ParameterDirection.Input, model.UserName));
                        cmd.Parameters.Add(_oracleHelper.getParam("oCursor", OracleType.Cursor));

                        await context.OpenAsync();
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {

                            response = await MappingsDB.MapToValueListTablaMaestra(_oracleHelper, reader);

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

        public async Task<List<TablaMaestraMant>> GetTablaMaestraMantenimiento()
        {
            OracleConnection context = null;
            List<TablaMaestraMant> response = null;
            try
            {
                Environment.SetEnvironmentVariable("NLS_LANG", ".UTF8");
                using (context = new OracleConnection(_dbConnection.ConnectionString))
                {
                    using (OracleCommand cmd = new OracleCommand("ESSIV.PKG_ESSIV.SP_LISTAR_TABLA_MAESTRA_MANT", context))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.Add(_oracleHelper.getParam("sUsuario", OracleType.VarChar, ParameterDirection.Input, model.UserName));
                        cmd.Parameters.Add(_oracleHelper.getParam("oCursor", OracleType.Cursor));

                        await context.OpenAsync();
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {

                            response = await MappingsDB.MapToValueListTablaMaestraMant(_oracleHelper, reader);

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
        public async Task<List<TablaMaestraDetalleMant>> GetTablaMaestraDetalleMantenimiento(int idTablaMaestra)
        {
            OracleConnection context = null;
            List<TablaMaestraDetalleMant> response = null;
            try
            {
                Environment.SetEnvironmentVariable("NLS_LANG", ".UTF8");
                using (context = new OracleConnection(_dbConnection.ConnectionString))
                {
                    using (OracleCommand cmd = new OracleCommand("ESSIV.PKG_ESSIV.SP_LISTAR_TABLA_MAE_DET_MANT", context))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(_oracleHelper.getParam("sIdTablaMaestra", OracleType.Number, ParameterDirection.Input, idTablaMaestra));
                        cmd.Parameters.Add(_oracleHelper.getParam("oCursor", OracleType.Cursor));

                        await context.OpenAsync();
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {

                            response = await MappingsDB.MapToValueListTablaMaestraDetalleMant(_oracleHelper, reader);

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

        public async Task InsertarTablaMaestraDetalle(TablaMaestraDetalleMantRequest request)
        {
            OracleConnection context = null;

            try
            {
                Environment.SetEnvironmentVariable("NLS_LANG", ".UTF8");
                using (context = new OracleConnection(_dbConnection.ConnectionString))
                {
                    using (OracleCommand cmd = new OracleCommand("ESSIV.PKG_ESSIV.SP_INSERTAR_TABLA_MAESTRA_DET", context))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(_oracleHelper.getParam("sIdTablaMaestra", OracleType.Number, ParameterDirection.Input, request.IdTablaMaestra));
                        cmd.Parameters.Add(_oracleHelper.getParam("sDescripcion", OracleType.VarChar, ParameterDirection.Input, request.Descripcion));
                        cmd.Parameters.Add(_oracleHelper.getParam("sDetalle", OracleType.VarChar, ParameterDirection.Input, request.Detalle));
                        cmd.Parameters.Add(_oracleHelper.getParam("sDetalle2", OracleType.VarChar, ParameterDirection.Input, request.Detalle2));
                        cmd.Parameters.Add(_oracleHelper.getParam("sUsuario", OracleType.VarChar, ParameterDirection.Input, request.Usuario));
                        cmd.Parameters.Add(_oracleHelper.getParam("sFecha", OracleType.DateTime, ParameterDirection.Input, DateTime.Now));

                        await context.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();
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
        }
        public async Task ActualizarTablaMaestraDetalle(TablaMaestraDetalleMantRequest request)
        {
            OracleConnection context = null;

            try
            {
                Environment.SetEnvironmentVariable("NLS_LANG", ".UTF8");
                using (context = new OracleConnection(_dbConnection.ConnectionString))
                {
                    using (OracleCommand cmd = new OracleCommand("ESSIV.PKG_ESSIV.SP_ACTUALIZAR_TABLA_MAE_DET", context))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(_oracleHelper.getParam("sIdTablaMaestraDetalle", OracleType.Number, ParameterDirection.Input, request.IdTablaMaestraDetalle));
                        cmd.Parameters.Add(_oracleHelper.getParam("sDescripcion", OracleType.VarChar, ParameterDirection.Input, request.Descripcion));
                        cmd.Parameters.Add(_oracleHelper.getParam("sDetalle", OracleType.VarChar, ParameterDirection.Input, request.Detalle));
                        cmd.Parameters.Add(_oracleHelper.getParam("sDetalle2", OracleType.VarChar, ParameterDirection.Input, request.Detalle2));
                        cmd.Parameters.Add(_oracleHelper.getParam("sUsuario", OracleType.VarChar, ParameterDirection.Input, request.Usuario));
                        cmd.Parameters.Add(_oracleHelper.getParam("sFecha", OracleType.DateTime, ParameterDirection.Input, DateTime.Now));

                        await context.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();
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
        }

        public async Task EliminarTablaMaestraDetalle(int idTablaMaestraDetalle)
        {
            OracleConnection context = null;

            try
            {
                Environment.SetEnvironmentVariable("NLS_LANG", ".UTF8");
                using (context = new OracleConnection(_dbConnection.ConnectionString))
                {
                    using (OracleCommand cmd = new OracleCommand("ESSIV.PKG_ESSIV.SP_ELIMINAR_TABLA_MAE_DET", context))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(_oracleHelper.getParam("sIdTablaMaestraDetalle", OracleType.Number, ParameterDirection.Input, idTablaMaestraDetalle));

                        await context.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();
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
        }
    }
}
