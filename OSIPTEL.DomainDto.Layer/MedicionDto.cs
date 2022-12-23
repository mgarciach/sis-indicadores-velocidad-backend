using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSIPTEL.DomainDto.Layer
{
    public class MedicionDto
    {
		public int IdMedicion { get; set; }
		public int IdActa { get; set; }
		public string? Latitud { get; set; }
		public string? Longitud { get; set; }
		public DateTime FechaMedicion { get; set; }
		public string HoraMedicion { get; set; }
		public string? NumeroMovil { get; set; }
		public int? IdTelefono { get; set; }
		public int? IdTblTipoServidor { get; set; }
		public decimal? VelocidadDescarga { get; set; }
		public decimal? VelocidadSubida { get; set; }
		public decimal? Latencia { get; set; }
		public decimal? NivelIntensidad { get; set; }
		public string? Observaciones { get; set; }
		public string? MarcaTelefono { get; set; }
		public string? ModeloTelefono { get; set; }
		public string? SerieTelefono { get; set; }
		public string? ControlPatrimonialTelefono { get; set; }
		public string? TipoServidor { get; set; }
		public bool? TieneOtroValorDescarga { get; set; }
		public bool? TieneOtroValorSubida { get; set; }
		public string? VelocidadDescargaOtroValor { get; set; }
		public string? VelocidadSubidaOtroValor { get; set; }
		public bool? TieneOtroValorLatencia { get; set; }
		public string? LatenciaOtroValor { get; set; }
		public string? Cuadricula { get; set; }
		public decimal? TasaPerdidaPaquetes { get; set; }
		public string? NodoOptico { get; set; }
		public string? NombrePlanTarifario { get; set; }
		public DateTime? FechaAltaPlan { get; set; }
		public decimal? VelocidadBajadaPlan { get; set; }
		public decimal? VelocidadSubidaPlan { get; set; }
		public decimal? PorcentajeGarantizadoPlanBajada { get; set; }
		public decimal? PorcentajeGarantizadoPlanSubida { get; set; }
		public string? NombrePromocion { get; set; }
		public decimal? VelocidadBajadaPromocion { get; set; }
		public decimal? VelocidadSubidaPromocion { get; set; }
		public decimal? PorcentajeGarantizadoPromocion { get; set; }
		public DateTime? InicioPromocion { get; set; }
		public DateTime? FinPromocion { get; set; }
		public string? NumeroTelefonoServicio { get; set; }
		public string? NombreTitular { get; set; }
		public string? DireccionInstalacion { get; set; }
		public string GuidActa { get; set; }
		public string Guid { get; set; }
		public bool EsEliminado { get; set; }
		public string? Usuario { get; set; }
		public DateTime? FechaCreacion { get; set; }
	}
}
