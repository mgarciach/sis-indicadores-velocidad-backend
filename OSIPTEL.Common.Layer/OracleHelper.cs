using System;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;
using System.Text;

namespace OSIPTEL.Common.Layer
{
    public class OracleHelper
    {
        public OracleParameter getParam(string pClave, OracleType pTipo)
        {
            return getParam(pClave, pTipo, 0);
        }

        public OracleParameter getParam(string pClave, OracleType pTipo, int pLongitud)
        {
            OracleParameter prm = new OracleParameter();
            prm.ParameterName = pClave;
            prm.OracleType = pTipo;
            prm.Direction = ParameterDirection.Output;
            if (pLongitud > 0)
                prm.Size = pLongitud;

            return prm;
        }
        public OracleParameter getParam(string pClave, OracleType pTipo, ParameterDirection pdireccion, object pValor)
        {
            OracleParameter prm = new OracleParameter();
            prm.ParameterName = pClave;
            prm.OracleType = pTipo;

            if (pValor != null)
            {
                prm.Value = pValor;
            }
            else
            {
                if (pTipo == OracleType.Number) prm.Value = 0;
                if (pTipo == OracleType.VarChar) prm.Value = "";
                if (pTipo == OracleType.DateTime) prm.Value = null;

            }
            prm.Direction = pdireccion;
            return prm;
        }

        #region Lectura de DataReader
        public Int32 getInt32(DbDataReader _dr, string pClave)
        {
            return (_dr[pClave] != DBNull.Value) ? Convert.ToInt32(_dr[pClave]) : default(Int32);
        }
        public Int64 getInt64(DbDataReader _dr, string pClave)
        {
            return (_dr[pClave] != DBNull.Value) ? Convert.ToInt64(_dr[pClave]) : default(Int64);
        }
        public DateTime getDateTime(DbDataReader _dr, string pClave)
        {
            return (_dr[pClave] != DBNull.Value) ? Convert.ToDateTime(_dr[pClave]) : default(DateTime);
        }
        public Decimal getDecimal(DbDataReader _dr, string pClave)
        {
            return (_dr[pClave] != DBNull.Value) ? Convert.ToDecimal(_dr[pClave]) : default(Decimal);
        }
        public Double getDouble(DbDataReader _dr, string pClave)
        {
            return (_dr[pClave] != DBNull.Value) ? Convert.ToDouble(_dr[pClave]) : default(Double);
        }
        public String getString(DbDataReader _dr, string pClave)
        {
            var  a = _dr.GetValue(_dr.GetOrdinal(name: pClave)).ToString();

            return (_dr[pClave] != DBNull.Value) ?  Convert.ToString(_dr[pClave]) : default(String);
        }
        public Decimal? getDecimalNull(DbDataReader _dr, string pClave)
        {
            if (_dr[pClave] != DBNull.Value)
            {
                return Convert.ToDecimal(_dr[pClave]);
            }

            return null;
        }

        public DateTime? getDateTimeNull(DbDataReader _dr, string pClave)
        {
            if (_dr[pClave] != DBNull.Value) {
                return Convert.ToDateTime(_dr[pClave]);
            }

            return null;
        }

        public int? getInt32Null(DbDataReader _dr, string pClave)
        {
       
            if (_dr[pClave] != DBNull.Value)
            {
                return Convert.ToInt32(_dr[pClave]);
            }

            return null;
        }
        #endregion
    }
}
