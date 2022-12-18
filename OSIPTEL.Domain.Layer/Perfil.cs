using System;
using System.Collections.Generic;
using System.Text;

namespace OSIPTEL.Domain.Layer
{
    public class Perfil
    {
        public int IdPerfil { get; set; }
        public string Descripcion { get; set; }
        public string Aplicacion { get; set; }
        public string Usuario { get; set; }
        public string Estado { get; set; }
    }
}
