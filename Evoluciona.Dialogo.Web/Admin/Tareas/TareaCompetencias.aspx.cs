using Evoluciona.Dialogo.BusinessEntity;
using Evoluciona.Dialogo.BusinessLogic;
using Evoluciona.Dialogo.Web.WsPlanDesarrollo;
using System;
using System.Data;

namespace Evoluciona.Dialogo.Web.Admin.Tareas
{
    public partial class TareaCompetencias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CargaCompetencia();
        }

        private static void CargaCompetencia()
        {
            var wsCompetencia = new WsInterfaceFFVVSoapClient();
            var objBlCompetencia = new BlCompetencia();
            //listo a los GR y GZ con sus pais y año
            var listaCompetencia = objBlCompetencia.SeleccionarGerenteNumeroDocumento();
            foreach (var t in listaCompetencia)
            {
                //esta validacion solo se aplica para el caso de colombia donde el doc. identidad se aplica en enteros
                var documentoIdentidadAux = t.DocIdentidad.Trim();
                //string documentoIdentidad = t.CodigoPaisAdam == constantes.codigoAdamPaisColombia
                //    ? Int64.Parse(t.DocIdentidad.Trim()).ToString(CultureInfo.InvariantCulture)
                //    : t.DocIdentidad.Trim();

                var cub = t.Cub.Trim();


                var dtConsultaPlanDesarrollo = new DataSet();
                var dtConsultaPorcentajeAvanceCompetencia = new DataSet();

                try
                {
                    dtConsultaPlanDesarrollo =
                        wsCompetencia.ConsultaPlanDesarrollo(int.Parse(t.Anio.Trim()), cub.Trim());
                    dtConsultaPorcentajeAvanceCompetencia =
                        wsCompetencia.ConsultaPorcentajeAvanceCompetencia(int.Parse(t.Anio.Trim()),
                            cub.Trim());

                }
                catch (Exception exception)
                {
                    objBlCompetencia.InsertarLogCargaCompetencia(t.Anio.Trim() + "|" + cub.Trim(), exception.Message);
                }


                if (dtConsultaPlanDesarrollo == null) continue;
                if (dtConsultaPlanDesarrollo.Tables[0].Rows.Count <= 0) continue;
                foreach (DataRow fls in dtConsultaPlanDesarrollo.Tables[0].Rows)
                {
                    if (dtConsultaPorcentajeAvanceCompetencia == null) continue;
                    if (dtConsultaPorcentajeAvanceCompetencia.Tables[0].Rows.Count <= 0) continue;
                    foreach (DataRow filas in dtConsultaPorcentajeAvanceCompetencia.Tables[0].Rows)
                    {
                        if (int.Parse(fls["CodigoCompetencia"].ToString()) !=
                            int.Parse(filas["CodigoCompetencia"].ToString())) continue;
                        var a = new BeCompetencia
                        {
                            CodigoCompetencia = int.Parse(filas["CodigoCompetencia"].ToString()),
                            PrefijoIsoPais = t.PrefijoIsoPais,
                            Competencia = filas["DescripcionCompetencia"].ToString(),
                            CodigoColaborador = documentoIdentidadAux,
                            Anio = filas["AnioCurso"].ToString(),
                            EstadoActivo = 1,
                            Descripcion = filas["DescripcionCompetencia"].ToString(),
                            Sugerencia =
                                dtConsultaPlanDesarrollo.Tables[0].Rows[0]["DescripcionSugerencia"]
                                    .ToString(),
                            PorcentajeAvance =
                                decimal.Round(
                                    decimal.Parse(filas["PorcentajeAvance"].ToString())/100, 2),
                            IdRol = t.IdRol
                        };
                        objBlCompetencia.AgregarCompetencia(a);
                    }
                }
            }
        }

    }
}