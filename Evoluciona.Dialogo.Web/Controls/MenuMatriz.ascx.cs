
namespace Evoluciona.Dialogo.Web.Controls
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using System;
    using System.Web.UI;

    public partial class MenuMatriz : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CargarVariables();
            if (!Page.IsPostBack)
            {
                if (Request.Path.Contains("TomaAccion") || Request.Path.Contains("homematriz.aspx"))
                    divSubMenus.Visible = false;
                else
                    divSubMenus.Visible = true;                    
            }
            MostrarFechaRegistrarAcuerdos();         
        }

        private void MostrarFechaRegistrarAcuerdos()
        {
            var objBlCronoMatriz = new BlCronogramaPdM();
            var obj = (BeUsuario)Session[Constantes.ObjUsuarioLogeado];
            DateTime? fechaRegistrarAcuerdos;

            var objBeCronoMatriz = objBlCronoMatriz.BuscarCronogramaPdM(obj);

                if (!string.IsNullOrEmpty(objBeCronoMatriz.FechaProrroga.ToString()))
                {
                    fechaRegistrarAcuerdos = objBeCronoMatriz.FechaProrroga;
                    lblFechaRegistrarAcuerdos.Text = "Tienes hasta " + (DateTime.Parse(fechaRegistrarAcuerdos.ToString())).ToString("dd/MM/yyyy") + " para Registrar Acuerdos";
                }
                else
                {
                    if (!string.IsNullOrEmpty(objBeCronoMatriz.FechaLimite.ToString()))
                    {
                        fechaRegistrarAcuerdos = objBeCronoMatriz.FechaLimite;
                        lblFechaRegistrarAcuerdos.Text = "Tienes hasta " + (DateTime.Parse(fechaRegistrarAcuerdos.ToString())).ToString("dd/MM/yyyy") + " para Registrar Acuerdos";
                    }
                    else
                    {
                        lblFechaRegistrarAcuerdos.Text = "La fecha para registrar acuerdos no esta definida.";
                    }
                }
        }

        private void CargarVariables()
        {
            liCalibracion.Visible = false;

            var objUsuario = (BeUsuario)Session[Constantes.ObjUsuarioLogeado];

            if (objUsuario == null) return;
            var rol = objUsuario.idRol;

            if (rol == (int)TipoRol.DirectoraVentas)
            {
                liCalibracion.Visible = true;//solo pueden verlo los DV
            }
        }
    }
}