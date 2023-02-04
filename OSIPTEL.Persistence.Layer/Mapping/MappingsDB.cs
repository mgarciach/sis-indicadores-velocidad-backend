using OSIPTEL.Common.Layer;
using OSIPTEL.Domain.Layer;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSIPTEL.Persistence.Layer.Mapping
{
    public class MappingsDB
    {
        public static async Task<List<Poligono>> MapToValueListPoligono(OracleHelper _oracleHelper, DbDataReader reader)
        {
            List<Poligono> list = new List<Poligono>();
            while (await reader.ReadAsync())
            {
                list.Add(new Poligono
                {
                    IdPoligono = _oracleHelper.getInt32(reader, "ID_POLIGONO"),
                    UbigeoCentroPoblado = _oracleHelper.getString(reader, "UBIGEO_CENTRO_POBLADO"),
                    Vertice = _oracleHelper.getString(reader, "VERTICE"),
                    CoordenadaVertice = _oracleHelper.getString(reader, "COORDENADA_VERTICE"),
                    Longitud = _oracleHelper.getDecimal(reader, "LONGITUD"),
                    Latitud = _oracleHelper.getDecimal(reader, "LATITUD"),
                    Ax = _oracleHelper.getDecimal(reader, "AX"),
                    By = _oracleHelper.getDecimal(reader, "BY"),
                    C = _oracleHelper.getDouble(reader, "C"),
                });
            }
            return list;
        }

        public static async Task<List<SerieMovil>> MapToValueListSerieMovil(OracleHelper _oracleHelper, DbDataReader reader)
        {

            List<SerieMovil> list = new List<SerieMovil>();

            while (await reader.ReadAsync())
            {
                list.Add(new SerieMovil
                {
                    IdSerieMovil = _oracleHelper.getInt32(reader, "ID_SERIE_MOVIL"),
                    Serie = _oracleHelper.getString(reader, "SERIE"),
                    RazonSocial = _oracleHelper.getString(reader, "RAZON_SOCIAL"),
                    Codigo = _oracleHelper.getInt32(reader, "CODIGO"),
                    Tipo = _oracleHelper.getString(reader, "TIPO"),
                    IdTblOperadora = _oracleHelper.getInt32(reader, "ID_TBL_OPERADORA")
                });
            }

            return list;
        }

        public static async Task<List<Telefono>> MapToValueListTelefono(OracleHelper _oracleHelper, DbDataReader reader)
        {

            List<Telefono> list = new List<Telefono>();
            while (await reader.ReadAsync())
            {
                list.Add(new Telefono
                {
                    IdTelefonoCelular = _oracleHelper.getInt32(reader, "ID_TELEFONO_CELULAR"),
                    ControlPatrimonial = _oracleHelper.getString(reader, "CONTROL_PATRIMONIAL"),
                    Marca = _oracleHelper.getString(reader, "MARCA"),
                    Modelo = _oracleHelper.getString(reader, "MODELO"),
                    ModeloComercial = _oracleHelper.getString(reader, "MODELO_COMERCIAL"),
                    Serie = _oracleHelper.getString(reader, "SERIE"),
                    Descripcion = _oracleHelper.getString(reader, "DESCRIPCION"),
                    EstadoTelefono = _oracleHelper.getString(reader, "ESTADO_TELEFONO"),
                    Observacion = _oracleHelper.getString(reader, "OBSERVACION"),
                    FechaEnvio = _oracleHelper.getDateTime(reader, "FECHA_ENVIO"),
                });
            }
            return list;
        }

        public static async Task<List<TablaMaestra>> MapToValueListTablaMaestra(OracleHelper _oracleHelper, DbDataReader reader)
        {

            List<TablaMaestra> list = new List<TablaMaestra>();

            while (await reader.ReadAsync())
            {
                list.Add(new TablaMaestra
                {
                    IdTablaMaestraDetalle = _oracleHelper.getInt32(reader, "ID_TABLA_MAESTRA_DETALLE"),
                    NombreTablaMaestra = _oracleHelper.getString(reader, "NOMBRE_TABLA_MAESTRA"),
                    Codigo = _oracleHelper.getInt32(reader, "CODIGO"),
                    Descripcion = _oracleHelper.getString(reader, "DESCRIPCION"),
                    Detalle = _oracleHelper.getString(reader, "DETALLE"),
                    Detalle2 = _oracleHelper.getString(reader, "DETALLE2")
                });
            }
            return list;
        }

        public static async Task<List<TablaMaestraMant>> MapToValueListTablaMaestraMant(OracleHelper _oracleHelper, DbDataReader reader)
        {

            List<TablaMaestraMant> list = new List<TablaMaestraMant>();

            while (await reader.ReadAsync())
            {
                list.Add(new TablaMaestraMant
                {
                    IdTablaMaestra = _oracleHelper.getInt32(reader, "ID_TABLA_MAESTRA"),
                    Nombre = _oracleHelper.getString(reader, "NOMBRE"),
                    Codigo = _oracleHelper.getInt32(reader, "CODIGO"),
                    Descripcion = _oracleHelper.getString(reader, "DESCRIPCION")
                });
            }
            return list;
        }

        public static async Task<List<TablaMaestraDetalleMant>> MapToValueListTablaMaestraDetalleMant(OracleHelper _oracleHelper, DbDataReader reader)
        {

            List<TablaMaestraDetalleMant> list = new List<TablaMaestraDetalleMant>();

            while (await reader.ReadAsync())
            {
                list.Add(new TablaMaestraDetalleMant
                {
                    IdTablaMaestraDetalle = _oracleHelper.getInt32(reader, "ID_TABLA_MAESTRA_DETALLE"),
                    Detalle = _oracleHelper.getString(reader, "DETALLE"),
                    Codigo = _oracleHelper.getInt32(reader, "CODIGO"),
                    Descripcion = _oracleHelper.getString(reader, "DESCRIPCION"),
                    Detalle2 = _oracleHelper.getString(reader, "DETALLE2"),
                });
            }
            return list;
        }

        public static async Task<PaginateResponse<Telefono>> MapToValuePaginateTelefono(OracleHelper _oracleHelper, DbDataReader reader)
        {

            List<Telefono> list = new List<Telefono>();
            while (await reader.ReadAsync())
            {
                list.Add(new Telefono
                {
                    IdTelefonoCelular = _oracleHelper.getInt32(reader, "ID_TELEFONO_CELULAR"),
                    ControlPatrimonial = _oracleHelper.getString(reader, "CONTROL_PATRIMONIAL"),
                    Marca = _oracleHelper.getString(reader, "MARCA"),
                    Modelo = _oracleHelper.getString(reader, "MODELO"),
                    ModeloComercial = _oracleHelper.getString(reader, "MODELO_COMERCIAL"),
                    Serie = _oracleHelper.getString(reader, "SERIE"),
                    Descripcion = _oracleHelper.getString(reader, "DESCRIPCION"),
                    EstadoTelefono = _oracleHelper.getString(reader, "ESTADO_TELEFONO"),
                    Observacion = _oracleHelper.getString(reader, "OBSERVACION"),
                    FechaEnvio = _oracleHelper.getDateTimeNull(reader, "FECHA_ENVIO"),
                    Total = _oracleHelper.getInt32(reader, "TOTAL"),
                });
            }
            return new PaginateResponse<Telefono>()
            {
                Items = list,
                Total = list.Count() == 0 ? 0 : list.First().Total
            };
        }

        public static async Task<List<Cobertura>> MapToValueListCobertura(OracleHelper _oracleHelper, DbDataReader reader)
        {
            List<Cobertura> list = new List<Cobertura>();
            while (await reader.ReadAsync())
            {
                list.Add(new Cobertura
                {
                    IdCobertura = _oracleHelper.getInt32(reader, "ID_COBERTURA"),
                    UbigeoCentroPoblado = _oracleHelper.getString(reader, "UBIGEO_CENTRO_POBLADO"),
                    EstratoAMO = _oracleHelper.getString(reader, "ESTRATO_AMO"),
                    EstratoTDP = _oracleHelper.getString(reader, "ESTRATO_TDP"),
                    TotalConexionesAMO = _oracleHelper.getInt32Null(reader, "TOTAL_CONEXIONES_AMO"),
                    TotalConexionesTDP = _oracleHelper.getInt32Null(reader, "TOTAL_CONEXIONES_TDP"),
                });
            }
            return list;
        }
    }
}
