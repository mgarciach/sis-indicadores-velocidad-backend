using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSIPTEL.DomainDto.Layer
{
    public class PoligonoDto
    {
        public int IdPoligono { get; set; }
        public string UbigeoCentroPoblado { get; set; }
        public string Vertice { get; set; }
        public string CoordenadaVertice { get; set; }
        public decimal Longitud { get; set; }
        public decimal Latitud { get; set; }
        public decimal Ax { get; set; }
        public decimal By { get; set; }
        public double C { get; set; }
    }
}
