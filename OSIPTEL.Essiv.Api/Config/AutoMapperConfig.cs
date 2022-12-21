using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using OSIPTEL.Domain.Layer;
using OSIPTEL.DomainDto.Layer;

namespace OSIPTEL.Essiv.Api.Config
{
    public class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                //cfg.CreateMap<Usuario, UsuarioDto>().ReverseMap();
                //cfg.CreateMap<UsuarioValid, UsuarioValidDto>().ReverseMap();

                cfg.CreateMap<CentroPoblado, CentroPobladoDto>().ReverseMap();
                cfg.CreateMap<Cuadricula, CuadriculaDto>().ReverseMap();
                cfg.CreateMap<Poligono, PoligonoDto>().ReverseMap();
                cfg.CreateMap<SerieMovil, SerieMovilDto>().ReverseMap();
                cfg.CreateMap<Telefono, TelefonoDto>().ReverseMap();
                cfg.CreateMap<TablaMaestra, TablaMaestraDto>().ReverseMap();
                cfg.CreateMap<TablaMaestraMant, TablaMaestraMantDto>().ReverseMap();
                cfg.CreateMap<TablaMaestraDetalleMant, TablaMaestraDetalleMantDto>().ReverseMap();
                cfg.CreateMap<TablaMaestraDetalleMantRequest, TablaMaestraDetalleMantRequestDto>().ReverseMap();
                cfg.CreateMap<PageTelefonoRequest, PageTelefonoRequestDto>().ReverseMap();
                cfg.CreateMap<TelefonoRequest, TelefonoRequestDto>().ReverseMap();
                cfg.CreateMap<Usuario, UsuarioDto>().ReverseMap();
                cfg.CreateMap<UsuarioValid, UsuarioValidDto>().ReverseMap();
                cfg.CreateMap<Perfil, PerfilDto>().ReverseMap();
                cfg.CreateMap<Cobertura, CoberturaDto>().ReverseMap();
            });
        }
    }
}
