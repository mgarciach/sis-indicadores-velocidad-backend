using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSIPTEL.DomainDto.Layer
{
    public class TelefonoDto
    {
        public int IdTelefonoCelular { get; set; }
        public string ControlPatrimonial { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string ModeloComercial { get; set; }
        public string Serie { get; set; }
        public string Descripcion { get; set; }
        public string UbicacionActualDepartamento { get; set; }
        public string UbicacionActualPersona { get; set; }
        public string EstadoTelefono { get; set; }
        public string Observacion { get; set; }
        public string Grupo2022_2 { get; set; }
        public DateTime? FechaEnvio { get; set; }
        public int Total { get; set; }
    }

    public class PageTelefonoRequestDto
    {
        public string? Marca { get; set; }
        public string? Serie { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }

    }

    public class TelefonoRequestDto {
        public int IdTelefonoCelular { get; set; }
        public string? ControlPatrimonial { get; set; }
        public string? Marca { get; set; }
        public string? Modelo { get; set; }
        public string? ModeloComercial { get; set; }
        public string? Serie { get; set; }
        public string? Descripcion { get; set; }
        public string? UbicacionActualDepartamento { get; set; }
        public string? UbicacionActualPersona { get; set; }
        public string? EstadoTelefono { get; set; }
        public string? Observacion { get; set; }
        public string? Grupo2022_2 { get; set; }
        public DateTime? FechaEnvio { get; set; }
    }
}
