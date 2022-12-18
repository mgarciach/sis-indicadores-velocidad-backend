using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSIPTEL.DomainDto.Layer
{
    public class SerieMovilDto
    {
        public int IdSerieMovil { get; set; }
        public string Serie { get; set; }
        public string RazonSocial { get; set; }
        public int Codigo { get; set; }
        public string Tipo { get; set; }
        public int IdTblOperadora { get; set; }

    }
}
