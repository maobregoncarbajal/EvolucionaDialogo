
namespace Evoluciona.Dialogo.Web.Admin
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Web.UI;
    using I = Helper;

    public partial class Impresion : Page
    {
        private string codigoUsuario;
        private int idRolEvaluado;
        private string prefijoIsoPais;
        private string periodoEvaluacion;
        private int codigoRolEvaluado;

        protected void Page_Load(object sender, EventArgs e)
        {
            codigoUsuario = Request["codigoUsuario"];
            idRolEvaluado = Convert.ToInt32(Request["idRolEvaluado"]);
            prefijoIsoPais = Request["prefijoIsoPais"];
            periodoEvaluacion = Request["periodoEvaluacion"];
            codigoRolEvaluado = Convert.ToInt32(Request["codigoRolEvaluado"]);
            if (!Page.IsPostBack)
                cargarDatos();
        }

        private void cargarDatos()
        {
            List<BeComun> lista = new List<BeComun>();
            lista.Add(new BeComun() { Codigo = "", Descripcion = "TODOS" });
            lista.AddRange(CargarProcesosByEstado(Constantes.EstadoProcesoActivo));
            lista.AddRange(CargarProcesosByEstado(Constantes.EstadoProcesoRevision));
            lista.AddRange(CargarProcesosByEstado(Constantes.EstadoProcesoCulminado));

            ddlFiltroImpresion.DataTextField = "Descripcion";
            ddlFiltroImpresion.DataValueField = "Codigo";
            ddlFiltroImpresion.DataSource = lista;
            ddlFiltroImpresion.DataBind();
        }

        private List<BeComun> CargarProcesosByEstado(string estadoProceso)
        {
            BlResumenProceso objBLResumen = new BlResumenProceso();
            BeUsuario objUsuario = (BeUsuario)Session[Constantes.ObjUsuarioLogeado];

            List<BeComun> comunes = null;
            switch (codigoRolEvaluado)
            {
                case Constantes.RolGerenteRegion:
                    comunes = objBLResumen.SeleccionarResumenProcesoGrList(objUsuario.codigoUsuario, idRolEvaluado, objUsuario.prefijoIsoPais, periodoEvaluacion, estadoProceso, Constantes.EstadoActivo);
                    break;
                case Constantes.RolGerenteZona:
                    comunes = objBLResumen.SeleccionarResumenProcesoGzList(objUsuario.codigoUsuario, idRolEvaluado, objUsuario.prefijoIsoPais, periodoEvaluacion, estadoProceso, Constantes.EstadoActivo);
                    break;
            }
            return comunes;
        }

        public static byte[] ReadFile(string filePath)
        {
            byte[] buffer;
            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            try
            {
                int length = (int)fileStream.Length;  // get file length
                buffer = new byte[length];            // create buffer
                int count;                            // actual number of bytes read
                int sum = 0;                          // total number of bytes read

                // read until Read method returns 0 (end of the stream has been reached)
                while ((count = fileStream.Read(buffer, sum, length - sum)) > 0)
                    sum += count;  // sum is a buffer offset for next reading
            }
            finally
            {
                fileStream.Close();
            }
            return buffer;
        }

        protected void lnkBuscar_Click(object sender, EventArgs e)
        {
            string evaluados = hdEvaluados.Value;
            string evaluadosNombre = hdEvaluadosNombres.Value;
            string id = Guid.NewGuid().ToString();

            string strFilePath;
            string strFolder;
            strFolder = Server.MapPath("./");
            strFilePath = strFolder + "Prueba" + id + ".xml";

            I.Impresion impresion = new Helper.Impresion();
            impresion.GenerarExcel(strFilePath, evaluados, evaluadosNombre, codigoRolEvaluado, prefijoIsoPais);

            byte[] libro = ReadFile(strFilePath);

            Response.ClearHeaders();
            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment;filename=ResumenDialogo.xls");
            Response.ContentType = "application/xls"; ;
            Response.BinaryWrite(libro);
            Response.Flush();
            if (File.Exists(strFilePath))
            {
                try
                {
                    File.Delete(strFilePath);
                }
                catch (IOException)
                {
                }
            }
            Response.End();
        }
    }
}