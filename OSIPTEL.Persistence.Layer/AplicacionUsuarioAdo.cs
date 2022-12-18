using Microsoft.Extensions.Logging;
using OSIPTEL.Common.Layer;
using OSIPTEL.Domain.Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;
using System.Threading.Tasks;

namespace OSIPTEL.Persistence.Layer
{
    public interface IAplicacionUsuarioAdo
    {
        Task<string> GetValidUsuario(UsuarioValid model);
        Task<string> GetCadenaLDAP(DominioValid model);
        Task<Usuario> GetDatUsuario(UsuarioValid model);
        Task<List<Perfil>> GetDatPerfil(UsuarioValid model);
    }
    public class AplicacionUsuarioAdo : IAplicacionUsuarioAdo
    {
        private readonly IDbConnection _dbConnection;
        private readonly ILogger _logger;
        private readonly OracleHelper _oracleHelper;

        public AplicacionUsuarioAdo(
            IDbConnection dbConnection,
            ILogger<AplicacionUsuarioAdo> logger,
            OracleHelper oracleHelper
            )
        {
            _dbConnection = dbConnection;
            _logger = logger;
            _oracleHelper = oracleHelper;
        }

        public async Task<string> GetValidUsuario(UsuarioValid model)
        {
            OracleConnection context = null;
            //Usuario response = null;
            string sResult = string.Empty;
            try
            {
                //response = MapToValue();
                using (context = new OracleConnection(_dbConnection.ConnectionString))
                {
                    using (OracleCommand cmd = new OracleCommand("ACCESO.PKG_ACCESO.SP_VALIDAR_USUARIO", context))
                    {
                        try
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(_oracleHelper.getParam("sUsuario", OracleType.VarChar, ParameterDirection.Input, model.UserName));

                            OracleParameter prmout = new OracleParameter("nRetorno", OracleType.Number);
                            prmout.Direction = ParameterDirection.Output;
                            cmd.Parameters.Add(prmout);

                            await context.OpenAsync();
                            await cmd.ExecuteReaderAsync();
                            sResult = Convert.ToString(cmd.Parameters["nRetorno"].Value);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex.Message);
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
            return sResult;
        }

        public async Task<Usuario> GetDatUsuario(UsuarioValid model)
        {
            OracleConnection context = null;
            Usuario response = null;
            try
            {
                //response = MapToValue();
                using (context = new OracleConnection(_dbConnection.ConnectionString))
                {
                    using (OracleCommand cmd = new OracleCommand("ACCESO.PKG_ACCESO.SP_OBTENER_USUARIO", context))
                    {
                        try
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(_oracleHelper.getParam("sUsuario", OracleType.VarChar, ParameterDirection.Input, model.UserName));
                            cmd.Parameters.Add(_oracleHelper.getParam("oListado", OracleType.Cursor));

                            await context.OpenAsync();
                            using (var reader = await cmd.ExecuteReaderAsync())
                            {
                                ////object[] valuesBase = new object[reader.FieldCount];
                                while (await reader.ReadAsync())
                                {
                                    response = MapToValueOracle(reader/*, valuesBase*/);
                                }
                                reader.Close();
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex.Message);
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

        public async Task<List<Perfil>> GetDatPerfil(UsuarioValid model)
        {
            OracleConnection context = null;
            List<Perfil> response = null;
            try
            {
                //response = MapToValue();
                using (context = new OracleConnection(_dbConnection.ConnectionString))
                {
                    using (OracleCommand cmd = new OracleCommand("ACCESO.PKG_ACCESO.SP_OBTENER_PERFIL_X_USUARIO", context))
                    {
                        try
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(_oracleHelper.getParam("sAplicacion", OracleType.VarChar, ParameterDirection.Input, model.Aplicacion));
                            cmd.Parameters.Add(_oracleHelper.getParam("sUsuario", OracleType.VarChar, ParameterDirection.Input, model.UserName));
                            cmd.Parameters.Add(_oracleHelper.getParam("oListado", OracleType.Cursor));

                            await context.OpenAsync();
                            using (var reader = await cmd.ExecuteReaderAsync())
                            {
                                var ListPerfil = new List<Perfil>();
                                ////object[] valuesLista = new object[reader.FieldCount];
                                while (await reader.ReadAsync())
                                {
                                    response = MapToValueListPerfil(reader, ListPerfil/*, valuesLista*/);
                                }
                                reader.Close();
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex.Message);
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

        public async Task<string> GetCadenaLDAP(DominioValid model)
        {
            OracleConnection context = null;
            string sResult = string.Empty;
            try
            {
                using (context = new OracleConnection(_dbConnection.ConnectionString))
                {
                    using (OracleCommand cmd = new OracleCommand("ACCESO.PKG_GLOBAL.SP_OBTENER_GLOBAL", context))
                    {
                        try
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(_oracleHelper.getParam("sAplicacion", OracleType.VarChar, ParameterDirection.Input, model.Aplicacion));
                            cmd.Parameters.Add(_oracleHelper.getParam("sGlobal", OracleType.VarChar, ParameterDirection.Input, model.Global));

                            OracleParameter prmout = new OracleParameter("sValor", OracleType.VarChar, 500);
                            prmout.Direction = ParameterDirection.Output;
                            cmd.Parameters.Add(prmout);

                            await context.OpenAsync();
                            await cmd.ExecuteReaderAsync();
                            sResult = Convert.ToString(cmd.Parameters["sValor"].Value);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex.Message);
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
            return sResult;
        }

        //private Usuario MapToValue()
        //{
        //    return new Usuario()
        //    {
        //        IdUser = 1,
        //        UserName = "ovasqueza",
        //        Password = "Orv@.2019",
        //        Nombre = "Oscar Romeo Vásquez Alfaro"
        //    };
        //}

        private Usuario MapToValueOracle(DbDataReader reader/*, object[] valuesBase*/)
        {
            //reader.GetValues(valuesBase);
            return new Usuario()
            {
                Nombre = _oracleHelper.getString(reader, "NOMBRE"),
                UserName = _oracleHelper.getString(reader, "USUARIO"),
                Email = _oracleHelper.getString(reader, "CORREO"),
                Area = _oracleHelper.getString(reader, "AREA"),
                Cargo = _oracleHelper.getString(reader, "CARGO"),
                Estado = _oracleHelper.getString(reader, "ESTADO")
            };
        }

        private List<Perfil> MapToValueListPerfil(DbDataReader reader, List<Perfil> ListPerfil/*, object[] valuesLista*/)
        {
            //reader.GetValues(valuesLista);
            ListPerfil.Add(new Perfil
            {
                IdPerfil = _oracleHelper.getInt32(reader, "IDPERFIL"),
                Descripcion = _oracleHelper.getString(reader, "DESCRIPCION"),
                Aplicacion = _oracleHelper.getString(reader, "APLICACION"),
                Usuario = _oracleHelper.getString(reader, "USUARIO"),
                Estado = _oracleHelper.getString(reader, "ESTADO")
            });
            return ListPerfil;
        }
    }
}
