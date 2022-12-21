using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSIPTEL.DomainDto.Layer
{
    public class CoberturaDto
    {
        public int IdCobertura { get; set; }
        public string UbigeoCentroPoblado { get; set; }
        public int? TotalConexionesTDP { get; set; }
        public int? TotalConexionesAMO { get; set; }
        public string EstratoTDP { get; set; }
        public string EstratoAMO { get; set; }
    }
}
