
namespace Evoluciona.Dialogo.Web
{
    using BusinessEntity;
    using BusinessLogic;
    using Dialogo.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class Principal : MasterPage
    {
     
        protected void Page_Init(object sender, EventArgs e)
        {
            ValidarSesionActiva();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
          
           
            ValidarSesionActiva();

            if (IsPostBack) return;

            BeResumenProceso objResumenBE = (BeResumenProceso)Session[Constantes.ObjUsuarioProcesado];
            BlPais paisBL = new BlPais();

            BeUsuario objUsuario = (BeUsuario)Session[Constantes.ObjUsuarioLogeado];
            
            lblUserLogeado.Text = objUsuario.nombreUsuario;
            lblRolLogueado.Text = objUsuario.rolDescripcion;
            lblRolLogueado.Visible = false;

            BePais paisActual = paisBL.ObtenerPais(objUsuario.prefijoIsoPais);
            imgImagenPais.ImageUrl = "~/Images/" + paisActual.Imagen;
            lblNombrePais.Text = paisActual.NombrePais.Replace(" LBEL", "").Replace("ESIKA-", "").Replace("ESK-", "").Replace("ESK", "");
            if (objResumenBE != null)
            {
                lblFechaEvaluacion.Text = "Fecha de evaluación : " + objResumenBE.fechaCreacion.ToShortDateString();
            }

            CargarMenuPrincipal();
        }

        private void CargarMenuPrincipal()
        {
            DataTable dt = (DataTable)Session["MenuPrincipal"];

            //ds.Tables[0].Rows[0]["vchNombreCompleto"].ToString();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string idMenu = dt.Rows[i]["intIDMenu"].ToString();
                    

                    MenuItem mnItem = new MenuItem();
                    String url = (string) dt.Rows[i]["vchURL"];
                    if (dt.Rows[i]["vchDescripcionMenu"].ToString() == "Competencias")
                    {
                        BeUsuario objUsuario = (BeUsuario)Session[Constantes.ObjUsuarioLogeado];

                        Encriptacion en = new Encriptacion();

                        String cubEncriptado = en.EncriptarCub(objUsuario.cub);

                        //url = url + "?IdUsuario=" + objUsuario.IdUsuarioTK;
                        url = url + "?CUB=" + cubEncriptado;
                    }


                    if (dt.Rows[i]["vchDescripcionMenu"].ToString() == "Matriz")
                    {
                        BeUsuario objUsuario = (BeUsuario)Session[Constantes.ObjUsuarioLogeado];

                        if(objUsuario.idRol==3)
                        {
                            url = "restringido.aspx";
                        }
                    }

                    if (dt.Rows[i]["vchDescripcionMenu"].ToString() == "Escuela Ventas")
                    {
                        BeUsuario objUsuario = (BeUsuario)Session[Constantes.ObjUsuarioLogeado];

                        url = url + "?CUB=" + objUsuario.cub + "&pais=" + objUsuario.prefijoIsoPais + "&rol=" + GetRolByCodigoRol(objUsuario.codigoRol);
                    }



                    //mnItem.Value = string.Format("{0},{1},{2}", idMenu, url, dt.Rows[i]["vchImagen"]);
                    mnItem.Text = dt.Rows[i]["vchDescripcionMenu"].ToString();
                    mnItem.NavigateUrl = url;//string.Format("{0},{1},{2}", idMenu, url, dt.Rows[i]["vchImagen"]);
                    //if(String.Equals(idMenu, "5"))
                    //    mnItem.Enabled = false;

                    //if (mnItem.Text != "Visitas")

                    mnuOpciones.Items.Add(mnItem);

                    if (Session["mnMenuSeleccionado"] != null && idMenu == Session["mnMenuSeleccionado"].ToString())
                    {
                        mnItem.Selected = true;
                    }
                }
            }
        }

        protected void mnuOpciones_MenuItemClick(object sender, MenuEventArgs e)
        {
            string valorMenu = e.Item.Value;
            string[] opciones = valorMenu.Split(',');

            if (opciones.Length > 0)
            {
                e.Item.Selected = true;
                Session["mnMenuSeleccionado"] = opciones[0];
                Session["mnMenuImagen"] = opciones[2];
                Response.Redirect("~/" + opciones[1]);
            }
        }

        protected void lnkCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Response.Redirect("~/error.aspx?mensaje=Su sesion ha finalizado correctamente.");
        }

        private void ValidarSesionActiva()
        {
            BeUsuario objUsuario = (BeUsuario)Session[Constantes.ObjUsuarioLogeado];
            if (objUsuario == null)
            {
                Response.Redirect("~/error.aspx?mensaje=sesion");
            }
            else
            {
                if (Session["periodosValidos"] == null)
                {
                    BlPeriodos periodoBL = new BlPeriodos();
                    List<string> periodos = periodoBL.ObtenerPeriodos(objUsuario.prefijoIsoPais);
                    Session["periodosValidos"] = periodos;
                }
            }
        }


        private string GetRolByCodigoRol(int codigoRol)
        {
            string rol = String.Empty;

            switch (codigoRol)
            {
                case Constantes.RolDirectorVentas:
                    rol = Constantes.CodDirectorVentas;
                    break;
                case Constantes.RolGerenteRegion:
                    rol = Constantes.CodGerenteRegion;
                    break;
                case Constantes.RolGerenteZona:
                    rol = Constantes.CodGerenteZona;
                    break;
            }
            return rol;
        }


    }
}