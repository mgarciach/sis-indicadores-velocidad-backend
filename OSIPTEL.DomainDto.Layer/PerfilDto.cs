using System;
using System.Collections.Generic;
using System.Text;

namespace OSIPTEL.DomainDto.Layer
{
    public class PerfilDto
    {
        public int IdPerfil { get; set; }
        public string Descripcion { get; set; }
        public string Aplicacion { get; set; }
        public string Usuario { get; set; }
        public string Estado { get; set; }
    }
}
