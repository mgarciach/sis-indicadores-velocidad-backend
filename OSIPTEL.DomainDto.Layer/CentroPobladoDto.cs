using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSIPTEL.DomainDto.Layer
{
    public class CentroPobladoDto
    {
            public int IdCentroPoblado { get; set; }
            public string NombreCentroPoblado { get; set; }
            public string Ubigeo { get; set; }
            public string Departamento { get; set; }
            public string Provincia { get; set; }
            public string Distrito { get; set; }
            public decimal Latitud { get; set; }
            public decimal Longitud { get; set; }
            public string ClasificacionArea { get; set; }

    }
}
