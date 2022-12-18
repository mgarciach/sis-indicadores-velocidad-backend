using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OSIPTEL.Essiv.Api.Config
{
    public class AppSettings
    {
        public Aplicacion Aplicacion { get; set; }
    }

    public class Aplicacion
    {
        public string strAplicacion { get; set; }
    }
}
