using AutoMapper;
using Microsoft.Extensions.Logging;
using OSIPTEL.Domain.Layer;
using OSIPTEL.DomainDto.Layer;
using OSIPTEL.Persistence.Layer;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Threading.Tasks;

namespace OSIPTEL.Service.Layer
{
    public interface IUsuarioService
    {
        Task<string> GetValidUsuario(UsuarioValidDto model);
        Task<string> GetValidLDAP(UsuarioValidDto model);
        Task<UsuarioDto> GetDatUsuario(UsuarioValidDto model);
        Task<List<PerfilDto>> GetDatPerfil(UsuarioValidDto model);
    }
    public class UsuarioService : IUsuarioService
    {
        private readonly IAplicacionUsuarioAdo _aplicacionUsuarioAdo;
        private readonly ILogger _logger;

        public UsuarioService(
            IAplicacionUsuarioAdo aplicacionUsuarioAdo,
            ILogger<UsuarioService> logger
            )
        {
            _aplicacionUsuarioAdo = aplicacionUsuarioAdo;
            _logger = logger;
        }

        public async Task<string> GetValidUsuario(UsuarioValidDto model)
        {
            var result = string.Empty;

            try
            {
                var entry = Mapper.Map<UsuarioValid>(model);
                result = await _aplicacionUsuarioAdo.GetValidUsuario(entry);
                //_logger.LogInformation("Enviado información del usuario");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return result;
        }

        public async Task<UsuarioDto> GetDatUsuario(UsuarioValidDto model)
        {
            var result = new UsuarioDto();
            try
            {
                var entry = Mapper.Map<UsuarioValid>(model);
                result = Mapper.Map<UsuarioDto>(
                    await _aplicacionUsuarioAdo.GetDatUsuario(entry)
                );
                //_logger.LogInformation("Enviado información del usuario");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return result;
        }

        public async Task<List<PerfilDto>> GetDatPerfil(UsuarioValidDto model)
        {
            var result = new List<PerfilDto>();
            try
            {
                var entry = Mapper.Map<UsuarioValid>(model);
                result = Mapper.Map<List<PerfilDto>>(
                    await _aplicacionUsuarioAdo.GetDatPerfil(entry)
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return result;
        }

        public async Task<string> GetValidLDAP(UsuarioValidDto model)
        {
            string result = string.Empty;
            try
            {
                DominioValid entry = new DominioValid();
                entry.Aplicacion = "COMUN";
                entry.Global = "ADDRESS_LDAP";
                result = await _aplicacionUsuarioAdo.GetCadenaLDAP(entry);

                string strError = GetvalidaLDAP(model, result);
                if (strError.Length != 0)
                {
                    return "Usuario o contraseña incorrecta.";
                }
                result = strError;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return result;
        }

        private String GetvalidaLDAP(UsuarioValidDto model, string result)
        {
            try
            {
                //string strPath = "LDAP://srvdc/DC=osiptel,DC=gob,DC=pe";
                string strPath = result;
                string strDomain = "osiptel";
                string domainAndUsername = strDomain + @"\" + model.UserName;

                DirectoryEntry entry = new DirectoryEntry(strPath, domainAndUsername, model.Password);
                DirectorySearcher search = new DirectorySearcher(entry);

                search.Filter = "name=" + model.UserName;

                SearchResultCollection results = search.FindAll();
                foreach (SearchResult resultados in results)
                {

                    ResultPropertyCollection colProperties = resultados.Properties;

                    var ls = String.Empty;
                    foreach (string key in colProperties.PropertyNames)
                    {
                        foreach (object value in colProperties[key])
                        {
                            ls = ls + "" + key.ToString() + ": " + value + "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
            return "";
        }
    }
}
