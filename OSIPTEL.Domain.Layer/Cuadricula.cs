﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSIPTEL.Domain.Layer
{
    public class Cuadricula
    {
        public int IdCuadricula { get; set; }
        public string UbigeoCentroPoblado { get; set; }
        public string NumeroCuadricula { get; set; }
        public decimal? Latitud1 { get; set; }
        public decimal? Longitud1 { get; set; }

        public decimal? Latitud2 { get; set; }
        public decimal? Longitud2 { get; set; }

        public decimal? Latitud3 { get; set; }
        public decimal? Longitud3 { get; set; }

        public decimal? Latitud4 { get; set; }
        public decimal? Longitud4 { get; set; }
    }
}
