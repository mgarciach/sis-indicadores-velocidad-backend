using Microsoft.Extensions.Logging;
using OSIPTEL.Common.Layer;
using OSIPTEL.Domain.Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSIPTEL.Persistence.Layer
{
    public interface IAplicacionActaMedicionAdo
    {
        /// <summary>
        /// Inserta un registro de la tabla ACTA
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<int?> InsertarActa(Acta request);

        /// <summary>
        /// Inserta una lista de mediciones segun el id de la tabla ACTA
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task InsertarMediciones(int idActa, List<Medicion> requests, string usuario);

        /// <summary>
        /// Lista todos los ids de actas y sus mediciones por usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        Task<List<ActaIds>> GetAllActasIdsPorUsuario(string usuario);
    }

    public class AplicacionActaMedicionAdo : IAplicacionActaMedicionAdo
    {
        private readonly IDbConnection _dbConnection;
        private readonly ILogger _logger;
        private readonly OracleHelper _oracleHelper;

        public AplicacionActaMedicionAdo(
           IDbConnection dbConnection,
           ILogger<AplicacionActaMedicionAdo> logger,
           OracleHelper oracleHelper
           )
        {
            _dbConnection = dbConnection;
            _logger = logger;
            _oracleHelper = oracleHelper;
        }

        /// <summary>
        /// Inserta un registro de la tabla ACTA
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<int?> InsertarActa(Acta request)
        {
            OracleConnection context = null;
            int? idActa = null;
            try
            {
                Environment.SetEnvironmentVariable("NLS_LANG", ".UTF8");
                using (context = new OracleConnection(_dbConnection.ConnectionString))
                {
                    using (OracleCommand cmd = new OracleCommand("ESIGAII.PKG_ESIGAII.SP_INSERTAR_ACTA", context))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(_oracleHelper.getParam("sFechaInicio", OracleType.DateTime, ParameterDirection.Input, request.FechaInicio));
                        cmd.Parameters.Add(_oracleHelper.getParam("sHoraInicio", OracleType.VarChar, ParameterDirection.Input, request.HoraInicio));
                        cmd.Parameters.Add(_oracleHelper.getParam("sIdTblTipoActa", OracleType.Number, ParameterDirection.Input, request.IdTblTipoActa));
                        cmd.Parameters.Add(_oracleHelper.getParam("sIdTblOperadora", OracleType.Number, ParameterDirection.Input, request.IdTblOperadora));
                        cmd.Parameters.Add(_oracleHelper.getParam("sUbigeoCentroPoblado", OracleType.VarChar, ParameterDirection.Input, request.UbigeoCentroPoblado));
                        cmd.Parameters.Add(_oracleHelper.getParam("sIdTblTipoTecnologiaFijo", OracleType.Number, ParameterDirection.Input, request.IdTblTipoTecnologiaFijo));
                        cmd.Parameters.Add(_oracleHelper.getParam("sIdTblModalidadActa", OracleType.Number, ParameterDirection.Input, request.IdTblModalidadActa));
                        cmd.Parameters.Add(_oracleHelper.getParam("sIdTblCategoriaActa", OracleType.Number, ParameterDirection.Input, request.IdTblCategoriaActa));
                        cmd.Parameters.Add(_oracleHelper.getParam("sOperadora", OracleType.VarChar, ParameterDirection.Input, request.Operadora));
                        cmd.Parameters.Add(_oracleHelper.getParam("sOperadora2", OracleType.VarChar, ParameterDirection.Input, request.Operadora2));
                        cmd.Parameters.Add(_oracleHelper.getParam("sTipoActa", OracleType.VarChar, ParameterDirection.Input, request.TipoActa));
                        cmd.Parameters.Add(_oracleHelper.getParam("sCategoriaActa", OracleType.VarChar, ParameterDirection.Input, request.CategoriaActa));
                        cmd.Parameters.Add(_oracleHelper.getParam("sCentroPoblado", OracleType.VarChar, ParameterDirection.Input, request.CentroPoblado));
                        cmd.Parameters.Add(_oracleHelper.getParam("sDepartamento", OracleType.VarChar, ParameterDirection.Input, request.Departamento));
                        cmd.Parameters.Add(_oracleHelper.getParam("sProvincia", OracleType.VarChar, ParameterDirection.Input, request.Provincia));
                        cmd.Parameters.Add(_oracleHelper.getParam("sDistrito", OracleType.VarChar, ParameterDirection.Input, request.Distrito));
                        cmd.Parameters.Add(_oracleHelper.getParam("sTipoTecnologiaFijo", OracleType.VarChar, ParameterDirection.Input, request.TipoTecnologiaFijo));
                        cmd.Parameters.Add(_oracleHelper.getParam("sModalidadActa", OracleType.VarChar, ParameterDirection.Input, request.ModalidadActa));
                        cmd.Parameters.Add(_oracleHelper.getParam("sNombreActaDescarga", OracleType.VarChar, ParameterDirection.Input, request.NombreActaDescarga));
                        cmd.Parameters.Add(_oracleHelper.getParam("sIdTblTipoCP", OracleType.Number, ParameterDirection.Input, request.IdTblTipoCP));
                        cmd.Parameters.Add(_oracleHelper.getParam("sTipoCP", OracleType.VarChar, ParameterDirection.Input, request.TipoCP));
                        cmd.Parameters.Add(_oracleHelper.getParam("sLatitudCentro", OracleType.VarChar, ParameterDirection.Input, request.LatitudCentro));
                        cmd.Parameters.Add(_oracleHelper.getParam("sLongitudCentro", OracleType.VarChar, ParameterDirection.Input, request.LongitudCentro));
                        cmd.Parameters.Add(_oracleHelper.getParam("sTieneAnexo2", OracleType.Number, ParameterDirection.Input, request.TieneAnexo2));
                        cmd.Parameters.Add(_oracleHelper.getParam("sTieneAnexo3", OracleType.Number, ParameterDirection.Input, request.TieneAnexo3));
                        cmd.Parameters.Add(_oracleHelper.getParam("sDescripcionAnexo2", OracleType.VarChar, ParameterDirection.Input, request.DescripcionAnexo2));
                        cmd.Parameters.Add(_oracleHelper.getParam("sDescripcionAnexo3", OracleType.VarChar, ParameterDirection.Input, request.DescripcionAnexo3));
                        cmd.Parameters.Add(_oracleHelper.getParam("sEstrato", OracleType.VarChar, ParameterDirection.Input, request.Estrato));
                        cmd.Parameters.Add(_oracleHelper.getParam("sNumeroDocumentoSupervision", OracleType.VarChar, ParameterDirection.Input, request.NumeroDocumentoSupervision));
                        cmd.Parameters.Add(_oracleHelper.getParam("sNombresSupervisor", OracleType.VarChar, ParameterDirection.Input, request.NombresSupervisor));
                        cmd.Parameters.Add(_oracleHelper.getParam("sApellidosSupervisor", OracleType.VarChar, ParameterDirection.Input, request.ApellidosSupervisor));
                        cmd.Parameters.Add(_oracleHelper.getParam("sGuid", OracleType.VarChar, ParameterDirection.Input, request.Guid));
                        cmd.Parameters.Add(_oracleHelper.getParam("sUsuario", OracleType.VarChar, ParameterDirection.Input, request.Usuario));
                        cmd.Parameters.Add(_oracleHelper.getParam("sFechaCreacion", OracleType.DateTime, ParameterDirection.Input, DateTime.Now));
                        cmd.Parameters.Add(_oracleHelper.getParam("oIdActa", OracleType.Number, ParameterDirection.Output, idActa));


                        await context.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();
                        idActa = Convert.ToInt32(cmd.Parameters["oIdActa"].Value);
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

            return idActa;
        }

        /// <summary>
        /// Inserta una lista de mediciones segun el id de la tabla ACTA
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task InsertarMediciones(int idActa, List<Medicion> requests, string usuario)
        {
            OracleConnection context = null;

            try
            {
                Environment.SetEnvironmentVariable("NLS_LANG", ".UTF8");
                using (context = new OracleConnection(_dbConnection.ConnectionString))
                {
                    foreach (var request in requests)
                    {
                        using (OracleCommand cmd = new OracleCommand("ESIGAII.PKG_ESIGAII.SP_INSERTAR_MEDICION", context))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(_oracleHelper.getParam("sIdActa", OracleType.Number, ParameterDirection.Input, idActa));
                            cmd.Parameters.Add(_oracleHelper.getParam("sLatitud", OracleType.VarChar, ParameterDirection.Input, request.Latitud));
                            cmd.Parameters.Add(_oracleHelper.getParam("sLongitud", OracleType.VarChar, ParameterDirection.Input, request.Longitud));
                            cmd.Parameters.Add(_oracleHelper.getParam("sFechaMedicion", OracleType.DateTime, ParameterDirection.Input, request.FechaMedicion));
                            cmd.Parameters.Add(_oracleHelper.getParam("sHoraMedicion", OracleType.VarChar, ParameterDirection.Input, request.HoraMedicion));
                            cmd.Parameters.Add(_oracleHelper.getParam("sNumeroMovil", OracleType.VarChar, ParameterDirection.Input, request.NumeroMovil));
                            cmd.Parameters.Add(_oracleHelper.getParam("sIdTelefono", OracleType.Number, ParameterDirection.Input, request.IdTelefono));
                            cmd.Parameters.Add(_oracleHelper.getParam("sIdTblTipoServidor", OracleType.Number, ParameterDirection.Input, request.IdTblTipoServidor));
                            cmd.Parameters.Add(_oracleHelper.getParam("sVelocidadDescarga", OracleType.Number, ParameterDirection.Input, request.VelocidadDescarga));
                            cmd.Parameters.Add(_oracleHelper.getParam("sVelocidadSubida", OracleType.Number, ParameterDirection.Input, request.VelocidadSubida));
                            cmd.Parameters.Add(_oracleHelper.getParam("sLatencia", OracleType.Number, ParameterDirection.Input, request.Latencia));
                            cmd.Parameters.Add(_oracleHelper.getParam("sNivelIntensidad", OracleType.Number, ParameterDirection.Input, request.NivelIntensidad));
                            cmd.Parameters.Add(_oracleHelper.getParam("sObservaciones", OracleType.VarChar, ParameterDirection.Input, request.Observaciones));
                            cmd.Parameters.Add(_oracleHelper.getParam("sMarcaTelefono", OracleType.VarChar, ParameterDirection.Input, request.MarcaTelefono));
                            cmd.Parameters.Add(_oracleHelper.getParam("sModeloTelefono", OracleType.VarChar, ParameterDirection.Input, request.ModeloTelefono));
                            cmd.Parameters.Add(_oracleHelper.getParam("sSerieTelefono", OracleType.VarChar, ParameterDirection.Input, request.SerieTelefono));
                            cmd.Parameters.Add(_oracleHelper.getParam("sControlPatrimonialTelefono", OracleType.VarChar, ParameterDirection.Input, request.ControlPatrimonialTelefono));
                            cmd.Parameters.Add(_oracleHelper.getParam("sTipoServidor", OracleType.VarChar, ParameterDirection.Input, request.TipoServidor));
                            cmd.Parameters.Add(_oracleHelper.getParam("sTieneOtroValorDescarga", OracleType.Number, ParameterDirection.Input, request.TieneOtroValorDescarga == true ? 1 : 0));
                            cmd.Parameters.Add(_oracleHelper.getParam("sTieneOtroValorSubida", OracleType.Number, ParameterDirection.Input, request.TieneOtroValorSubida == true ? 1 : 0));
                            cmd.Parameters.Add(_oracleHelper.getParam("sVelocidadDescargaOtroValor", OracleType.VarChar, ParameterDirection.Input, request.VelocidadDescargaOtroValor));
                            cmd.Parameters.Add(_oracleHelper.getParam("sVelocidadSubidaOtroValor", OracleType.VarChar, ParameterDirection.Input, request.VelocidadSubidaOtroValor));
                            cmd.Parameters.Add(_oracleHelper.getParam("sTieneOtroValorLatencia", OracleType.Number, ParameterDirection.Input, request.TieneOtroValorLatencia == true ? 1 : 0));
                            cmd.Parameters.Add(_oracleHelper.getParam("sLatenciaOtroValor", OracleType.VarChar, ParameterDirection.Input, request.LatenciaOtroValor));
                            cmd.Parameters.Add(_oracleHelper.getParam("sCuadricula", OracleType.VarChar, ParameterDirection.Input, request.Cuadricula));
                            cmd.Parameters.Add(_oracleHelper.getParam("sTasaPerdidaPaquetes", OracleType.Number, ParameterDirection.Input, request.TasaPerdidaPaquetes));
                            cmd.Parameters.Add(_oracleHelper.getParam("sNodoOptico", OracleType.VarChar, ParameterDirection.Input, request.NodoOptico));
                            cmd.Parameters.Add(_oracleHelper.getParam("sNombrePlanTarifario", OracleType.VarChar, ParameterDirection.Input, request.NombrePlanTarifario));
                            cmd.Parameters.Add(_oracleHelper.getParam("sFechaAltaPlan", OracleType.DateTime, ParameterDirection.Input, request.FechaAltaPlan));
                            cmd.Parameters.Add(_oracleHelper.getParam("sVelocidadBajadaPlan", OracleType.Number, ParameterDirection.Input, request.VelocidadBajadaPlan));
                            cmd.Parameters.Add(_oracleHelper.getParam("sVelocidadSubidaPlan", OracleType.Number, ParameterDirection.Input, request.VelocidadSubidaPlan));
                            cmd.Parameters.Add(_oracleHelper.getParam("sPorcentajeGarantPlanBajada", OracleType.Number, ParameterDirection.Input, request.PorcentajeGarantizadoPlanBajada));
                            cmd.Parameters.Add(_oracleHelper.getParam("sPorcentajeGarantPlanSubida", OracleType.Number, ParameterDirection.Input, request.PorcentajeGarantizadoPlanSubida));
                            cmd.Parameters.Add(_oracleHelper.getParam("sNombrePromocion", OracleType.VarChar, ParameterDirection.Input, request.NombrePromocion));
                            cmd.Parameters.Add(_oracleHelper.getParam("sVelocidadBajadaPromocion", OracleType.Number, ParameterDirection.Input, request.VelocidadBajadaPromocion));
                            cmd.Parameters.Add(_oracleHelper.getParam("sVelocidadSubidaPromocion", OracleType.Number, ParameterDirection.Input, request.VelocidadSubidaPromocion));
                            cmd.Parameters.Add(_oracleHelper.getParam("sPorcentajeGaranPromocion", OracleType.Number, ParameterDirection.Input, request.PorcentajeGarantizadoPromocion));
                            cmd.Parameters.Add(_oracleHelper.getParam("sInicioPromocion", OracleType.DateTime, ParameterDirection.Input, request.InicioPromocion));
                            cmd.Parameters.Add(_oracleHelper.getParam("sFinPromocion", OracleType.DateTime, ParameterDirection.Input, request.FinPromocion));
                            cmd.Parameters.Add(_oracleHelper.getParam("sNumeroTelefonoServicio", OracleType.VarChar, ParameterDirection.Input, request.NumeroTelefonoServicio));
                            cmd.Parameters.Add(_oracleHelper.getParam("sNombreTitular", OracleType.VarChar, ParameterDirection.Input, request.NombreTitular));
                            cmd.Parameters.Add(_oracleHelper.getParam("sDireccionInstalacion", OracleType.VarChar, ParameterDirection.Input, request.DireccionInstalacion));
                            cmd.Parameters.Add(_oracleHelper.getParam("sGuid", OracleType.VarChar, ParameterDirection.Input, request.Guid));
                            cmd.Parameters.Add(_oracleHelper.getParam("sGuidActa", OracleType.VarChar, ParameterDirection.Input, request.GuidActa));
                            cmd.Parameters.Add(_oracleHelper.getParam("sUsuario", OracleType.VarChar, ParameterDirection.Input, usuario));
                            cmd.Parameters.Add(_oracleHelper.getParam("sFechaCreacion", OracleType.DateTime, ParameterDirection.Input, DateTime.Now));

                            await context.OpenAsync();
                            await cmd.ExecuteNonQueryAsync();
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
        }


        /// <summary>
        /// Lista todos los ids de actas y sus mediciones por usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public async Task<List<ActaIds>> GetAllActasIdsPorUsuario(string usuario)
        {
            OracleConnection context = null;
            List<ActaIds> response = new List<ActaIds>();
            try
            {
                Environment.SetEnvironmentVariable("NLS_LANG", ".UTF8");
                using (context = new OracleConnection(_dbConnection.ConnectionString))
                {

                    using (OracleCommand cmd = new OracleCommand("ESIGAII.PKG_ESIGAII.SP_LISTAR_IDS_GUID_ACTAS", context))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(_oracleHelper.getParam("sUsuario", OracleType.VarChar, ParameterDirection.Input, usuario));
                        cmd.Parameters.Add(_oracleHelper.getParam("oCursor", OracleType.Cursor));

                        await context.OpenAsync();
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                response.Add(new ActaIds
                                {
                                    IdActa = _oracleHelper.getInt32(reader, "ID_ACTA"),
                                    Guid = _oracleHelper.getString(reader, "GUID"),
                                });

                            }
                            reader.Close();
                        }
                    }

                    foreach (var acta in response)
                    {
                        var mediciones = new List<MedicionIds>();

                        using (OracleCommand cmd = new OracleCommand("ESIGAII.PKG_ESIGAII.SP_LISTAR_IDS_GUID_MEDICIONES", context))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(_oracleHelper.getParam("sIdActa", OracleType.VarChar, ParameterDirection.Input, acta.IdActa));
                            cmd.Parameters.Add(_oracleHelper.getParam("oCursor", OracleType.Cursor));

                            await context.OpenAsync();
                            using (var reader = await cmd.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    mediciones.Add(new MedicionIds
                                    {
                                        IdMedicion = _oracleHelper.getInt32(reader, "ID_MEDICION"),
                                        Guid = _oracleHelper.getString(reader, "GUID"),
                                    });

                                }
                                reader.Close();
                            }
                        }

                        acta.Mediciones = mediciones;
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
