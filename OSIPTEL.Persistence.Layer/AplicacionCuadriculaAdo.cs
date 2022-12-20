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
    public interface IAplicacionCuadriculaAdo
    {
        Task<List<Cuadricula>> GetAllCuadricula();
    }
    public class AplicacionCuadriculaAdo : IAplicacionCuadriculaAdo
    {
        private readonly IDbConnection _dbConnection;
        private readonly ILogger _logger;
        private readonly OracleHelper _oracleHelper;

        public AplicacionCuadriculaAdo(
           IDbConnection dbConnection,
           ILogger<AplicacionCentroPobladoAdo> logger,
           OracleHelper oracleHelper
           )
        {
            _dbConnection = dbConnection;
            _logger = logger;
            _oracleHelper = oracleHelper;
        }
        public async Task<List<Cuadricula>> GetAllCuadricula()
        {
            OracleConnection context = null;
            List<Cuadricula> response = null;
            try
            {
                Environment.SetEnvironmentVariable("NLS_LANG", ".UTF8");
                using (context = new OracleConnection(_dbConnection.ConnectionString))
                {
                    using (OracleCommand cmd = new OracleCommand("ESSIV.PKG_ESSIV.SP_LISTAR_CUADRICULAS", context))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.Add(_oracleHelper.getParam("sUsuario", OracleType.VarChar, ParameterDirection.Input, model.UserName));
                        cmd.Parameters.Add(_oracleHelper.getParam("CURSOR_", OracleType.Cursor));

                        await context.OpenAsync();
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            var ListCuadriculas = new List<Cuadricula>();
                            ////object[] valuesLista = new object[reader.FieldCount];
                            while (await reader.ReadAsync())
                            {
                                response = MapToValueListCuadricula(reader, ListCuadriculas/*, valuesLista*/);
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

        private List<Cuadricula> MapToValueListCuadricula(DbDataReader reader, List<Cuadricula> ListServ)
        {
            ListServ.Add(new Cuadricula
            {
                IdCuadricula = _oracleHelper.getInt32(reader, "ID_CUADRICULA"),
                UbigeoCentroPoblado = _oracleHelper.getString(reader, "UBIGEO_CENTRO_POBLADO"),
                Latitud1 = _oracleHelper.getDecimal(reader, "LATITUD1"),
                Longitud1 = _oracleHelper.getDecimal(reader, "LONGITUD1"),
                Latitud2 = _oracleHelper.getDecimal(reader, "LATITUD2"),
                Longitud2 = _oracleHelper.getDecimal(reader, "LONGITUD2"),
                Latitud3 = _oracleHelper.getDecimal(reader, "LATITUD3"),
                Longitud3 = _oracleHelper.getDecimal(reader, "LONGITUD3"),
                Latitud4 = _oracleHelper.getDecimal(reader, "LATITUD4"),
                Longitud4 = _oracleHelper.getDecimal(reader, "LONGITUD4"),
                NumeroCuadricula = _oracleHelper.getString(reader, "CUADRICULA")
            });
            return ListServ;
        }
    }
}
