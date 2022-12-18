using Microsoft.Extensions.Logging;
using OSIPTEL.Common.Layer;
using OSIPTEL.Domain.Layer;
using OSIPTEL.Persistence.Layer.Mapping;
using System.Data;
using System.Data.OracleClient;

namespace OSIPTEL.Persistence.Layer
{
    public interface IAplicacionTelefonoAdo
    {
        Task<List<Telefono>> GetAllTelefono();

        Task<PaginateResponse<Telefono>> PaginarTelefono(PageTelefonoRequest request);

        Task ActualizarTelefono(TelefonoRequest request);

        Task EliminarTelefono(int idTelefonoCelular);
    }
    public class AplicacionTelefonoAdo : IAplicacionTelefonoAdo
    {
        private readonly IDbConnection _dbConnection;
        private readonly ILogger _logger;
        private readonly OracleHelper _oracleHelper;

        public AplicacionTelefonoAdo(
           IDbConnection dbConnection,
           ILogger<AplicacionTelefonoAdo> logger,
           OracleHelper oracleHelper
           )
        {
            _dbConnection = dbConnection;
            _logger = logger;
            _oracleHelper = oracleHelper;
        }

        public async Task<List<Telefono>> GetAllTelefono()
        {
            OracleConnection context = null;
            List<Telefono> response = null;
            try
            {
                Environment.SetEnvironmentVariable("NLS_LANG", ".UTF8");
                using (context = new OracleConnection(_dbConnection.ConnectionString))
                {
                    using (OracleCommand cmd = new OracleCommand("ESSIV.PKG_ESSIV.SP_LISTAR_TELEFONOS", context))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.Add(_oracleHelper.getParam("sUsuario", OracleType.VarChar, ParameterDirection.Input, model.UserName));
                        cmd.Parameters.Add(_oracleHelper.getParam("oCursor", OracleType.Cursor));

                        await context.OpenAsync();
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            response = await MappingsDB.MapToValueListTelefono(_oracleHelper, reader);

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

        public async Task<PaginateResponse<Telefono>> PaginarTelefono(PageTelefonoRequest request)
        {
            OracleConnection context = null;
            PaginateResponse<Telefono> response = null;
            try
            {
                Environment.SetEnvironmentVariable("NLS_LANG", ".UTF8");
                using (context = new OracleConnection(_dbConnection.ConnectionString))
                {
                    using (OracleCommand cmd = new OracleCommand("ESSIV.PKG_ESSIV.SP_PAGINAR_TEL_MANT", context))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.Add(_oracleHelper.getParam("sUsuario", OracleType.VarChar, ParameterDirection.Input, model.UserName));
                        cmd.Parameters.Add(_oracleHelper.getParam("oCursor", OracleType.Cursor));
                        cmd.Parameters.Add(_oracleHelper.getParam("sMarca", OracleType.VarChar, ParameterDirection.Input, request.Marca));
                        cmd.Parameters.Add(_oracleHelper.getParam("sSerie", OracleType.VarChar, ParameterDirection.Input, request.Serie));
                        cmd.Parameters.Add(_oracleHelper.getParam("sPage", OracleType.Number, ParameterDirection.Input, request.Page));
                        cmd.Parameters.Add(_oracleHelper.getParam("sPageSize", OracleType.Number, ParameterDirection.Input, request.PageSize));

                        await context.OpenAsync();
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            response = await MappingsDB.MapToValuePaginateTelefono(_oracleHelper, reader);

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

        public async Task ActualizarTelefono(TelefonoRequest request)
        {
            OracleConnection context = null;

            try
            {
                Environment.SetEnvironmentVariable("NLS_LANG", ".UTF8");
                using (context = new OracleConnection(_dbConnection.ConnectionString))
                {
                    using (OracleCommand cmd = new OracleCommand("ESSIV.PKG_ESSIV.SP_ACTUALIZAR_TELEFONO", context))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(_oracleHelper.getParam("sIdTelefonoCelular", OracleType.Number, ParameterDirection.Input, request.IdTelefonoCelular));
                        cmd.Parameters.Add(_oracleHelper.getParam("sControlPatrimonial", OracleType.VarChar, ParameterDirection.Input, request.ControlPatrimonial));
                        cmd.Parameters.Add(_oracleHelper.getParam("sMarca", OracleType.VarChar, ParameterDirection.Input, request.Marca));
                        cmd.Parameters.Add(_oracleHelper.getParam("sModelo", OracleType.VarChar, ParameterDirection.Input, request.Modelo));
                        cmd.Parameters.Add(_oracleHelper.getParam("sModeloComercial", OracleType.VarChar, ParameterDirection.Input, request.ModeloComercial));
                        cmd.Parameters.Add(_oracleHelper.getParam("sSerie", OracleType.VarChar, ParameterDirection.Input, request.Serie));
                        cmd.Parameters.Add(_oracleHelper.getParam("sDescripcion", OracleType.VarChar, ParameterDirection.Input, request.Descripcion));
                        cmd.Parameters.Add(_oracleHelper.getParam("sUbicacionActualDepartamento", OracleType.VarChar, ParameterDirection.Input, request.UbicacionActualDepartamento));
                        cmd.Parameters.Add(_oracleHelper.getParam("sUbicacionActualPersona", OracleType.VarChar, ParameterDirection.Input, request.UbicacionActualPersona));
                        cmd.Parameters.Add(_oracleHelper.getParam("sEstadoTelefono", OracleType.VarChar, ParameterDirection.Input, request.EstadoTelefono));
                        cmd.Parameters.Add(_oracleHelper.getParam("sObservacion", OracleType.VarChar, ParameterDirection.Input, request.Observacion));
                        cmd.Parameters.Add(_oracleHelper.getParam("sGrupo2022_2", OracleType.VarChar, ParameterDirection.Input, request.Grupo2022_2));
                        cmd.Parameters.Add(_oracleHelper.getParam("sFechaEnvio", OracleType.DateTime, ParameterDirection.Input, request.FechaEnvio));

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

        public async Task EliminarTelefono(int idTelefonoCelular)
        {
            OracleConnection context = null;

            try
            {
                Environment.SetEnvironmentVariable("NLS_LANG", ".UTF8");
                using (context = new OracleConnection(_dbConnection.ConnectionString))
                {
                    using (OracleCommand cmd = new OracleCommand("ESSIV.PKG_ESSIV.SP_ELIMINAR_TELEFONO", context))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(_oracleHelper.getParam("sIdTelefonoCelular", OracleType.Number, ParameterDirection.Input, idTelefonoCelular));

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
