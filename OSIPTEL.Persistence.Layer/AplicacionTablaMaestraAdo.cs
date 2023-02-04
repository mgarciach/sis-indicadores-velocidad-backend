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
        /// <summary>
        /// Obtiene todo el listado de tabla maestra detalle. Sirve para llenar combos en la aplicación.
        /// </summary>
        /// <returns></returns>
        Task<List<TablaMaestra>> GetAllTablaMaestra();

        /// <summary>
        /// Obtiene el listado de la tabla maestra. Se usa en la opción de mantenimiento
        /// </summary>
        /// <returns></returns>
        Task<List<TablaMaestraMant>> GetTablaMaestraMantenimiento();

        /// <summary>
        /// Obtiene el listado de detalle de cada tabla maestra.
        /// </summary>
        /// <param name="idTablaMaestra"></param>
        /// <returns></returns>
        Task<List<TablaMaestraDetalleMant>> GetTablaMaestraDetalleMantenimiento(int idTablaMaestra);

        /// <summary>
        /// Inserta un nuevo detalle en la tabla "TABLA_MAESTRA_DETALLE"
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task InsertarTablaMaestraDetalle(TablaMaestraDetalleMantRequest request);

        /// <summary>
        /// Actualiza un registro de la tabla TABLA_MAESTRA_DETALLE
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task ActualizarTablaMaestraDetalle(TablaMaestraDetalleMantRequest request);

        /// <summary>
        /// Elimina un registro de la tabla TABLA_MAESTRA_DETALLE
        /// </summary>
        /// <param name="idTablaMaestraDetalle"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Obtiene todo el listado de tabla maestra detalle. Sirve para llenar combos en la aplicación.
        /// </summary>
        /// <returns></returns>
        public async Task<List<TablaMaestra>> GetAllTablaMaestra()
        {
            OracleConnection context = null;
            List<TablaMaestra> response = null;
            try
            {
                Environment.SetEnvironmentVariable("NLS_LANG", ".UTF8");
                using (context = new OracleConnection(_dbConnection.ConnectionString))
                {
                    using (OracleCommand cmd = new OracleCommand("PKG_SSIGAII.SP_LISTAR_TABLAS_MAESTRAS", context))
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

        /// <summary>
        /// Obtiene el listado de la tabla maestra. Se usa en la opción de mantenimiento
        /// </summary>
        /// <returns></returns>
        public async Task<List<TablaMaestraMant>> GetTablaMaestraMantenimiento()
        {
            OracleConnection context = null;
            List<TablaMaestraMant> response = null;
            try
            {
                Environment.SetEnvironmentVariable("NLS_LANG", ".UTF8");
                using (context = new OracleConnection(_dbConnection.ConnectionString))
                {
                    using (OracleCommand cmd = new OracleCommand("PKG_SSIGAII.SP_LISTAR_TABLA_MAESTRA_MANT", context))
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

        /// <summary>
        /// Obtiene el listado de detalle de cada tabla maestra.
        /// </summary>
        /// <param name="idTablaMaestra"></param>
        /// <returns></returns>
        public async Task<List<TablaMaestraDetalleMant>> GetTablaMaestraDetalleMantenimiento(int idTablaMaestra)
        {
            OracleConnection context = null;
            List<TablaMaestraDetalleMant> response = null;
            try
            {
                Environment.SetEnvironmentVariable("NLS_LANG", ".UTF8");
                using (context = new OracleConnection(_dbConnection.ConnectionString))
                {
                    using (OracleCommand cmd = new OracleCommand("PKG_SSIGAII.SP_LISTAR_TABLA_MAE_DET_MANT", context))
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

        /// <summary>
        /// Inserta un nuevo detalle en la tabla "TABLA_MAESTRA_DETALLE"
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task InsertarTablaMaestraDetalle(TablaMaestraDetalleMantRequest request)
        {
            OracleConnection context = null;

            try
            {
                Environment.SetEnvironmentVariable("NLS_LANG", ".UTF8");
                using (context = new OracleConnection(_dbConnection.ConnectionString))
                {
                    using (OracleCommand cmd = new OracleCommand("PKG_SSIGAII.SP_INSERTAR_TABLA_MAESTRA_DET", context))
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

        /// <summary>
        /// Actualiza un registro de la tabla TABLA_MAESTRA_DETALLE
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task ActualizarTablaMaestraDetalle(TablaMaestraDetalleMantRequest request)
        {
            OracleConnection context = null;

            try
            {
                Environment.SetEnvironmentVariable("NLS_LANG", ".UTF8");
                using (context = new OracleConnection(_dbConnection.ConnectionString))
                {
                    using (OracleCommand cmd = new OracleCommand("PKG_SSIGAII.SP_ACTUALIZAR_TABLA_MAE_DET", context))
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

        /// <summary>
        /// Elimina un registro de la tabla TABLA_MAESTRA_DETALLE
        /// </summary>
        /// <param name="idTablaMaestraDetalle"></param>
        /// <returns></returns>
        public async Task EliminarTablaMaestraDetalle(int idTablaMaestraDetalle)
        {
            OracleConnection context = null;

            try
            {
                Environment.SetEnvironmentVariable("NLS_LANG", ".UTF8");
                using (context = new OracleConnection(_dbConnection.ConnectionString))
                {
                    using (OracleCommand cmd = new OracleCommand("PKG_SSIGAII.SP_ELIMINAR_TABLA_MAE_DET", context))
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
