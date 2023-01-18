﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OSIPTEL.Common.Layer;
using OSIPTEL.Domain.Layer;
using OSIPTEL.DomainDto.Layer;
using OSIPTEL.Essiv.Api.Config;
using OSIPTEL.Service.Layer;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OSIPTEL.Essiv.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private readonly AppSettings _appSettings;

        public AuthController(
           IUsuarioService usuarioService,
           ILogger<TablaMaestraController> logger,
           IConfiguration configuration,
           IOptions<AppSettings> appSettings
       )
        {
            _usuarioService = usuarioService;
            _logger = logger;
            _configuration = configuration;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Endpoint usado para crear token de acceso por JWT
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="200">Cuando el recurso es validado correctamente. Adicionalmente, retorna el recurso validado (objeto).</response>
        /// <response code="400">Cuando los parámetros de entrada no han podido ser validados.</response>/// 
        [HttpPost("login", Name = "GetValidUsuario")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetValidUsuario([FromBody] UsuarioValidDto model)
        {
            model.Aplicacion = _appSettings.Aplicacion.strAplicacion;
            var datUser = new UsuarioDto();
            var datPerfil = new List<PerfilDto>();
            //var datServicio = new List<ServicioDto>();
            var usuario = new Usuario();

            var token = string.Empty;
            var user = string.Empty;
            var name = string.Empty;
            var email = string.Empty;
            var area = string.Empty;
            var cargo = string.Empty;
            var estado = string.Empty;
            var perfil = new List<PerfilDto>();
            //var servicio = new List<ServicioDto>();

            if (Convert.ToBoolean(_configuration["Dev"]))
            {
                return Ok(TestLogin(model));
            }

            try
            {
                var strLDAP = await _usuarioService.GetValidLDAP(model);
                string iderror;
                string error;
                if (strLDAP.Length != 0)
                {
                    //return BadRequest("Usuario o contraseña incorrecta.");
                    iderror = "001";
                    error = "Usuario o contraseña incorrecta.";
                }
                else
                {
                    var valUser = await _usuarioService.GetValidUsuario(model);
                    if (valUser == "0")
                    {
                        //return BadRequest("Usuario no Registrado en Base de Datos.");
                        iderror = "002";
                        error = "Usuario no Registrado en Base de Datos.";
                    }
                    else
                    {
                        datUser = await _usuarioService.GetDatUsuario(model);
                        if (datUser == null)
                        {
                            //return BadRequest("Usuario no Registrado en Base de Datos.");
                            iderror = "003";
                            error = "Usuario no Registrado en Base de Datos.";
                        }
                        else
                        {
                            usuario = Mapper.Map<Usuario>(datUser);
                            datPerfil = await _usuarioService.GetDatPerfil(model);
                            //ServicioValidDto serv = new ServicioValidDto();
                            //serv.UserName = model.UserName;
                            //datServicio = await _servicioService.GetAllUsuarioServicio(serv);

                            if (datPerfil == null)
                            {
                                //return BadRequest("Usuario no tiene acceso a la Aplicación");
                                iderror = "004";
                                error = "Usuario no tiene acceso a la Aplicación.";
                            }
                            else
                            {
                                iderror = "000";
                                error = "Acceso OK.";
                                token = Token(usuario);
                                user = datUser.UserName;
                                name = datUser.Nombre;
                                email = datUser.Email;
                                area = datUser.Area;
                                cargo = datUser.Cargo;
                                estado = datUser.Estado;
                                perfil = datPerfil;
                                //servicio = datServicio;
                            }
                        }
                    }
                }
                return Ok(
                    new
                    {
                        access_token = token,
                        user_user = user,
                        user_name = name,
                        user_email = email,
                        user_area = area,
                        user_cargo = cargo,
                        user_estado = estado,
                        user_perfil = perfil,
                        user_iderror = iderror,
                        user_error = error,
                        //user_servicio = servicio
                    }
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " - " + ex.InnerException);
                throw ex.InnerException;
            }
        }

        private string Token(Usuario user)
        {
            var secretKey = _configuration.GetValue<string>("SecretKey");
            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserName),
                    new Claim(ClaimTypes.Name, user.Nombre)
                }),
                Expires = DateTime.UtcNow.AddDays(365),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var createdToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(createdToken);
        }
        #region HERLPERS
        private object TestLogin(UsuarioValidDto model)
        {
            UsuarioDto datUser = null;
            List<PerfilDto> datPerfil = null;

            if (model.UserName == "aperez" && model.Password == "123456")
            {
                datUser = new UsuarioDto()
                {
                    UserName = "aperez",
                    Nombre = "Alex Santiago Perez Fernandez",
                    Email = "aperez@osiptel.gob.pe",
                    Area = "Supervisión",
                    Cargo = "Supervisor",
                    Estado = "Activo",
                };

                datPerfil = new List<PerfilDto>()
                {
                    new PerfilDto() {
                        Aplicacion = "ESSIV",
                        Descripcion = "Supervisor",
                        Estado = "Activo",
                        IdPerfil = 1,
                        Usuario = "aperez"
                    }
                };
            }

            if (model.UserName == "myauricasa" && model.Password == "123456")
            {
                datUser = new UsuarioDto()
                {
                    UserName = "myauricasa",
                    Nombre = "Melissa Yauricasa Bautista",
                    Email = "myauricasa@osiptel.gob.pe",
                    Area = "Supervisión",
                    Cargo = "Administrador",
                    Estado = "Activo",
                };

                datPerfil = new List<PerfilDto>()
                {
                    new PerfilDto() {
                        Aplicacion = "SIGAII",
                        Descripcion = "Administrador",
                        Estado = "Activo",
                        IdPerfil = 1,
                        Usuario = "myauricasa"
                    }
                };
            }

            var usuario = Mapper.Map<Usuario>(datUser);

            return new
            {
                access_token = datUser == null ? null : Token(usuario),
                user_user = datUser?.UserName,
                user_name = datUser?.Nombre,
                user_email = datUser?.Email,
                user_area = datUser?.Area,
                user_cargo = datUser?.Cargo,
                user_estado = datUser?.Estado,
                user_perfil = datPerfil,
                user_iderror = datUser == null ? "001" : "000",
                user_error = datUser == null ? "Acceso Denegado" : "Acceso OK."
            };
        }

        #endregion

    }

    public class TextRequest
    {
        public string Text { get; set; }
    }
}
