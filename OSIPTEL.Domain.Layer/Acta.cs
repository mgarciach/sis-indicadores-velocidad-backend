using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSIPTEL.Domain.Layer
{
	public class Acta
	{
		public int IdActa { get; set; }
		public DateTime FechaInicio { get; set; }
		public string? HoraInicio { get; set; }
		public decimal? IdTblTipoActa { get; set; }
		public decimal? IdTblOperadora { get; set; }
		public string? UbigeoCentroPoblado { get; set; }
		public decimal? IdTblTipoTecnologiaFijo { get; set; }
		public decimal? IdTblModalidadActa { get; set; }
		public decimal? IdTblCategoriaActa { get; set; }
		public string? Operadora { get; set; }
		public string? Operadora2 { get; set; }
		public string? TipoActa { get; set; }
		public string? CategoriaActa { get; set; }
		public string? CentroPoblado { get; set; }
		public string? Departamento { get; set; }
		public string? Provincia { get; set; }
		public string? Distrito { get; set; }
		public string? TipoTecnologiaFijo { get; set; }
		public string? ModalidadActa { get; set; }
		public string? NombreActaDescarga { get; set; }
		public int? IdTblTipoCP { get; set; }
		public string? TipoCP { get; set; }
		public string? LatitudCentro { get; set; }
		public string? LongitudCentro { get; set; }
		public int TieneAnexo2 { get; set; }
		public int TieneAnexo3 { get; set; }
		public string? DescripcionAnexo2 { get; set; }
		public string? DescripcionAnexo3 { get; set; }
		public string? Estrato { get; set; }
		public string? NumeroDocumentoSupervision { get; set; }
		public string? NombresSupervisor { get; set; }
		public string? ApellidosSupervisor { get; set; }
		public bool EsEliminado { get; set; }
		public string Usuario { get; set; }
		public DateTime? FechaCreacion { get; set; }
		public string? Guid { get; set; }
		public List<Medicion> Mediciones { get; set; }
	}

	public class ActaIds {
		public int IdActa { get; set; }
		public string? Guid { get; set; }
        public List<MedicionIds> Mediciones { get; set; }
    }

	public class MedicionIds {
		public int IdMedicion { get; set; }
		public string? Guid { get; set; }
	}
}
