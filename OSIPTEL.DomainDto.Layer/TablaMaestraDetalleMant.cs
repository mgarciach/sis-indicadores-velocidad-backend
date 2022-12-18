using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSIPTEL.DomainDto.Layer
{
    public class TablaMaestraDetalleMantDto
    {
        public int IdTablaMaestraDetalle { get; set; }
        public string Descripcion { get; set; }
        public int Codigo { get; set; }
        public string Detalle { get; set; }
        public string Detalle2 { get; set; }
    }

    public class TablaMaestraDetalleMantRequestDto
    {
        public int? IdTablaMaestra { get; set; }
        public int? IdTablaMaestraDetalle { get; set; }
        public string Descripcion { get; set; }
        public string? Detalle { get; set; }
        public string? Detalle2 { get; set; }
        public string Usuario { get; set; }
    }
}
