using System;

namespace Evoluciona.Dialogo.BusinessEntity
{
    public class BeGerenteZona
    {
        #region "BELCORP"

        public int intIDGerenteZona { get; set; }
        public int intIDGerenteRegion { get; set; }
        public BeGerenteRegion obeGerenteRegion { get; set; }

        public string CodigoGerenteRegion
        {
            get { return obeGerenteRegion.ChrCodigoGerenteRegion; }
        }
        public string NombreGerenteRegion
        {
            get { return obeGerenteRegion.VchNombrecompleto; }
        }

        public string chrPrefijoIsoPais { get; set; }
        public string chrCodigoGerenteZona { get; set; }
        public string vchNombreCompleto { get; set; }
        public string vchCorreoElectronico { get; set; }
        public bool bitEstado { get; set; }
        public int intUsuarioCrea { get; set; }
        public DateTime datFechaCrea { get; set; }
        public int intUsuarioModi { get; set; }
        public DateTime datFechaModi { get; set; }
        public string chrCodigoDataMart { get; set; }
        public string chrCampaniaRegistro { get; set; }
        public string chrIndicadorMigrado { get; set; }
        public string chrCampaniaBaja { get; set; }
        public BePais obePais { get; set; }
        public string EstadoGerente { get; set; }
        public string FechaActualizacion { get; set; } //11/12/2012
        public string FechaBaja { get; set; }
        public string DescripcionRegion { get; set; }
        public string DescripcionZona { get; set; }
        public string CUBGZ { get; set; }
        public string vchCUBGR { get; set; }
        public string vchCUBGZ { get; set; }
        public string chrCodigoPlanilla { get; set; }
        public string vchCodigoRegion { get; set; }
        public string vchCodigoZona { get; set; }
        public string vchObservacion { get; set; }

        #endregion

        #region "DATAMART"

        public string AnioCampana { get; set; }
        public string CodPais { get; set; }
        public string CodRegion { get; set; }
        public string CodGerenteRegional { get; set; }
        public string codZona { get; set; }
        public string CodGerenteZona { get; set; }
        public string DesGerenteZona { get; set; }
        public string DocIdentidad { get; set; }
        public string CorreoElectronico { get; set; }
        public string EstadoCamp { get; set; }
        public int PtoRankingProdCamp { get; set; }
        public string Periodo { get; set; }
        public string EstadoPeriodo { get; set; }
        public decimal PtoRankingProdPeriodo { get; set; }
        public DateTime FechaUltAct { get; set; }
        public int FlagProceso { get; set; }
        public int FlagControl { get; set; }
        public int FlagControl_CSFyGH { get; set; }

        #endregion
    }
}
