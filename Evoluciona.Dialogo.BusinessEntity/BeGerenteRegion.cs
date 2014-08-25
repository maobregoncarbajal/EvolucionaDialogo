using System;

namespace Evoluciona.Dialogo.BusinessEntity
{
    public class BeGerenteRegion
    {
        public string ChrCodigoDataMart { get; set; }
        public string ChrCodDirectorVenta { get; set; }
        public string ChrNombreCodDirectorVenta { get; set; }
        public int IntUsuarioCrea { get; set; }
        public bool BitEstado { get; set; }
        public string VchCorreoElectronico { get; set; }
        public string VchNombrecompleto { get; set; }
        public string ChrPrefijoIsoPais { get; set; }
        public string ChrCodigoGerenteRegion { get; set; }
        public int IntIDGerenteRegion { get; set; }
        public string VchCUBGR { get; set; }
        public string ChrCodigoPlanilla { get; set; }
        public string VchCodigoRegion { get; set; }
        public string VchObservacion { get; set; }

        /// <summary>
        /// Descripción:    Se agregan nuevos parametros para el mentenimiento de matrices
        ///                 DataMart - Belcorp
        /// Modificado por: Daniel Huamán
        /// Fecha:          17/11/2012
        /// </summary>
        ///
        public BeDirectoraVentas obeDirectoraVentas { get; set; }
        public string chrCampaniaRegistro { get; set; }
        public string chrIndicadorMigrado { get; set; }
        public string chrCampaniaBaja { get; set; }
        public string IdAndCodigoGerenteRegion { get; set; }
        public string CodigoGerenteRegionForDatamart { get; set; }
        public BePais obePais { get; set; }
        public string EstadoGerente { get; set; }
        public string FechaActualizacion { get; set; }
        public string codZona { get; set; }
        public string FechaBaja { get; set; }
        public string DescripcionRegion { get; set; }

        #region "DATAMART"
        public string AnioCampana { get; set; }
        public string CodPais { get; set; }
        public string CodRegion { get; set; }
        public string CodGerenteRegional { get; set; }
        public string DesGerenteRegional { get; set; }
        public string DocIdentidad { get; set; }
        public string CorreoElectronico { get; set; }
        public string EstadoCamp { get; set; }
        public int PtoRankingProdCamp { get; set; }//Convertido a entero el tipo de dato original es Tinyint
        public string Periodo { get; set; }
        public string EstadoPeriodo { get; set; }
        //public float PtoRankingProdPeriodo { get; set; }
        public decimal PtoRankingProdPeriodo { get; set; }
        public DateTime FechaUltAct { get; set; }
        public int FlagProceso { get; set; }
        public int FlagControl { get; set; }
        public int FlagControl_CSFyGH { get; set; }
        #endregion "DATAMART"
    }
}