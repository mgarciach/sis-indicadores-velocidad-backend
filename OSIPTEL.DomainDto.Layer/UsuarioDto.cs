using System;
using System.Collections.Generic;

namespace OSIPTEL.DomainDto.Layer
{
    public class UsuarioDto
    {
        //public int IdUser { get; set; }
        public string UserName { get; set; }
        //public string Password { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Cargo { get; set; }
        public string Area { get; set; }
        public string Estado { get; set; }
        public string CodError { get; set; }
        public string MsjError { get; set; }

        //public List<Rol> Rol { get; set; }
        //public List<Personal> Personal { get; set; }
        //public List<Sistema> Sistema { get; set; }
    }

    public class UsuarioValidDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Aplicacion { get; set; }
    }
}
