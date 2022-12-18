using Microsoft.Extensions.Logging;
using OSIPTEL.Common.Layer;
using OSIPTEL.Domain.Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSIPTEL.Persistence.Layer
{
    public interface IAplicacionCentroPobladoAdo
    {
        Task<List<CentroPoblado>> GetAllUsuarioServicio();
    }
    public class AplicacionCentroPobladoAdo : IAplicacionCentroPobladoAdo
    {
        private readonly IDbConnection _dbConnection;
        private readonly ILogger _logger;
        private readonly OracleHelper _oracleHelper;

        public AplicacionCentroPobladoAdo(
           IDbConnection dbConnection,
           ILogger<AplicacionCentroPobladoAdo> logger,
           OracleHelper oracleHelper
           )
        {
            _dbConnection = dbConnection;
            _logger = logger;
            _oracleHelper = oracleHelper;
        }

        public async Task<List<CentroPoblado>> GetAllUsuarioServicio() {
            OracleConnection context = null;
            List<CentroPoblado> response = null;
            try
            {
                Environment.SetEnvironmentVariable("NLS_LANG", ".UTF8");
                using (context = new OracleConnection(_dbConnection.ConnectionString))
                {
                    
                    using (OracleCommand cmd = new OracleCommand("ESSIV.PKG_ESSIV.SP_LISTAR_CENTROS_POBLADOS", context))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.Add(_oracleHelper.getParam("sUsuario", OracleType.VarChar, ParameterDirection.Input, model.UserName));
                        cmd.Parameters.Add(_oracleHelper.getParam("CURSOR_", OracleType.Cursor));

                        await context.OpenAsync();
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            var ListCentrosPoblados = new List<CentroPoblado>();
                            ////object[] valuesLista = new object[reader.FieldCount];
                            while (await reader.ReadAsync())
                            {
                                response = MapToValueListCentroPoblado(reader, ListCentrosPoblados/*, valuesLista*/);
                            }
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

        private List<CentroPoblado> MapToValueListCentroPoblado(DbDataReader reader, List<CentroPoblado> ListServ/*, object[] valuesLista*/)
        {
            //reader.GetValues(valuesLista);
            ListServ.Add(new CentroPoblado
            {
                IdCentroPoblado = _oracleHelper.getInt32(reader, "ID_CENTRO_POBLADO"),
                NombreCentroPoblado = _oracleHelper.getString(reader, "CENTRO_POBLADO"),
                Ubigeo = _oracleHelper.getString(reader, "UBIGEO"),
                Departamento = _oracleHelper.getString(reader, "DEPARTAMENTO"),
                Provincia = _oracleHelper.getString(reader, "PROVINCIA"),
                Distrito = _oracleHelper.getString(reader, "DISTRITO"),
                Latitud = _oracleHelper.getDecimal(reader, "LATITUD"),
                Longitud = _oracleHelper.getDecimal(reader, "LONGITUD"),
                ClasificacionArea = _oracleHelper.getString(reader, "CLASIFICACION_AREA"),
            });
            return ListServ;
        }
    }
}
