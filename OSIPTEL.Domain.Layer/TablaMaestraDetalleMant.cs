using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSIPTEL.Domain.Layer
{
    public class TablaMaestraDetalleMant
    {
        public int IdTablaMaestraDetalle { get; set; }
        public string Descripcion { get; set; }
        public int Codigo { get; set; }
        public string Detalle { get; set; }
        public string Detalle2 { get; set; }
    }

    public class TablaMaestraDetalleMantRequest
    {
        public int? IdTablaMaestra { get; set; }
        public int? IdTablaMaestraDetalle { get; set; }
        public string Descripcion { get; set; }
        public string Detalle { get; set; }
        public string Detalle2 { get; set; }
        public string Usuario { get; set; }
    }
}
