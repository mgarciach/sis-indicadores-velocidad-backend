using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Data.OracleClient;
using OSIPTEL.Persistence.Layer;
using OSIPTEL.Service.Layer;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using OSIPTEL.Common.Layer;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using OSIPTEL.Essiv.Api.Config;

namespace OSIPTEL.Essiv.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            var identitySettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(identitySettingsSection);

            services.AddCors(options => options.AddPolicy(name: "customCors",
                      policy =>
                      {
                          policy.WithOrigins("*")
                            .WithMethods("*")
                            .AllowAnyHeader();
                      }));

            
            var encrypt = Convert.ToBoolean(Configuration["Encrypt"]);

            var cnStr = this.Configuration.GetConnectionString("SeguridadConnection");

            if (encrypt) {
                var keysCifrado = Configuration.GetSection("KeysCifrado").Get<KeysCifrado>();
                cnStr = DecryptHelper.DecryptString(cnStr, keysCifrado);
            }

            services.AddTransient<IDbConnection>((sp) => new OracleConnection(cnStr));

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie();

            services.AddHttpContextAccessor();

            services.AddMemoryCache();
            //services.AddMvc(options =>
            //{
            //    //options.SetCompatibilityVersion(CompatibilityVersion.)
            //    options.EnableEndpointRouting = false;
            //});//.SetCompatibilityVersion(CompatibilityVersion.Ver);
            services.AddControllers().AddNewtonsoftJson();
            services.AddTransient<OracleHelper>();

            //Services
            services.AddTransient<ICentroPobladoService, CentroPobladoService>();
            services.AddTransient<IAplicacionCentroPobladoAdo, AplicacionCentroPobladoAdo>();

            services.AddTransient<ICuadriculaService, CuadriculaService>();
            services.AddTransient<IAplicacionCuadriculaAdo, AplicacionCuadriculaAdo>();

            services.AddTransient<IPoligonoService, PoligonoService>();
            services.AddTransient<IAplicacionPoligonoAdo, AplicacionPoligonoAdo>();

            services.AddTransient<ISerieMovilService, SerieMovilService>();
            services.AddTransient<IAplicacionSerieMovilAdo, AplicacionSerieMovilAdo>();

            services.AddTransient<ITelefonoService, TelefonoService>();
            services.AddTransient<IAplicacionTelefonoAdo, AplicacionTelefonoAdo>();

            services.AddTransient<ITablaMaestraService, TablaMaestraService>();
            services.AddTransient<IAplicacionTablaMaestraAdo, AplicacionTablaMaestraAdo>();

            services.AddTransient<IUsuarioService, UsuarioService>();
            services.AddTransient<IAplicacionUsuarioAdo, AplicacionUsuarioAdo>();

            services.AddTransient<ICoberturaService, CoberturaService>();
            services.AddTransient<IAplicacionCoberturaAdo, AplicacionCoberturaAdo>(); 

            services.AddTransient<IActaMedicionService, ActaMedicionService>();
            services.AddTransient<IAplicacionActaMedicionAdo, AplicacionActaMedicionAdo>();

            //Services External
            //services.AddTransient<ISunatService, SunatService>();

            //JWT
            var key = Encoding.ASCII.GetBytes(Configuration.GetValue<string>("SecretKey"));
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            // Configure swagger
            services.AddSwaggerGen(options =>
            {
                // Specify two versions 
                //options.SwaggerDoc("v1"
                //    ,
                //    new Info()
                //    {
                //        Version = "v1",
                //        Title = "OSIPTEL.Authorization.Api V1",
                //        Description = "Documentación para el API de Autorización V1",
                //        Contact = new Contact
                //        {
                //            Email = "ovasuqeza@osiptel.gob.pe",
                //            Name = "OSIPTEL",
                //            Url = "https://www.osiptel.gob.pe"
                //        },
                //        License = new License
                //        {
                //            Name = "User under LICX",
                //            Url = "https://example.com/license"
                //        }
                //    }
                //    );

                // Set the comments path for the Swagger JSON and UI.
                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //options.IncludeXmlComments(xmlPath);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            AutoMapperConfig.Initialize();

            loggerFactory.AddFile(env.ContentRootPath + "/Logging/log-{Date}.txt");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "OSIPTEL.ESSIV.Api V1");
                //c.RoutePrefix = String.Empty;
            });
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseCookiePolicy();


            app.UseRouting();
            app.UseCors("customCors");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Home}/{action=Index}/{id?}");
            //});
        }
    }
}
