using Microsoft.Extensions.Configuration;
using System;
using System.Security.Cryptography;
using System.Text;

namespace OSIPTEL.Common.Layer
{
    public class Encriptador
    {
        private readonly IConfiguration _configuration;
        public Encriptador(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Cifrar(string pTexto, ref string pMensaje)
        {
            string vLlave = "";
            string lRespuesta = "";
            pMensaje = "";
            SymmetricAlgorithm vAlgoritmo = default(SymmetricAlgorithm);
            vAlgoritmo = new RijndaelManaged();
            try
            {
                vLlave = _configuration["SecretKey"];
            }
            catch (Exception)
            {
                pMensaje = "Falta inicializar las variables de configuracion";
            }
            if (pMensaje.Trim().Length == 0)
            {
                try
                {
                    vAlgoritmo.Key = Convert.FromBase64String(vLlave);
                    vAlgoritmo.Mode = CipherMode.ECB;
                    ICryptoTransform vEncryptor = vAlgoritmo.CreateEncryptor();
                    byte[] vDato = Encoding.Unicode.GetBytes(pTexto);
                    byte[] vDatoEncriptado = vEncryptor.TransformFinalBlock(vDato, 0, vDato.Length);
                    lRespuesta = Convert.ToBase64String(vDatoEncriptado);
                }
                catch (Exception ex)
                {
                    pMensaje = "Error : " + ex.Message;
                }
            }
            return lRespuesta;
        }

        public string Decifrar(string pTexto, ref string pMensaje)
        {
            string vLlave = "";
            string lRespuesta = "";
            pMensaje = "";
            SymmetricAlgorithm vAlgoritmo = default(SymmetricAlgorithm);
            vAlgoritmo = new RijndaelManaged();
            try
            {
                vLlave = _configuration["SecretKey"];
            }
            catch (Exception)
            {
                pMensaje = "Falta inicializar las variables de configuracion";
            }
            if (pMensaje.Trim().Length == 0)
            {
                try
                {
                    vAlgoritmo.Key = Convert.FromBase64String(vLlave);
                    vAlgoritmo.Mode = CipherMode.ECB;
                    ICryptoTransform vDecryptor = vAlgoritmo.CreateDecryptor();
                    byte[] vDato = Convert.FromBase64String(pTexto);
                    byte[] vDatoDecifrado = vDecryptor.TransformFinalBlock(vDato, 0, vDato.Length);
                    lRespuesta = Encoding.Unicode.GetString(vDatoDecifrado);
                }
                catch (Exception ex)
                {
                    pMensaje = "Error : " + ex.Message;
                }
            }
            return lRespuesta;
        }
    }
}
