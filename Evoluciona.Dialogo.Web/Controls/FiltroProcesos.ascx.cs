
namespace Evoluciona.Dialogo.Web.Controls
{
    using BusinessEntity;
    using Dialogo.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class FiltroProcesos : UserControl
    {
        public event EventHandler MostrarDetalleProceso;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            OcultarControles();
        }

        private void OcultarControles()
        {
            divProcesoDialogo.Visible = false;
            divProcesoVisitas.Visible = false;
            divProcesoAmbos.Visible = false;
        }

        public void MostrarValoresFiltro(List<BeProceso> procesosActuales, TipoProceso tipoProceso)
        {
            LimpiarControles();

            if (tipoProceso == TipoProceso.Todos)
            {
                divProcesoDialogo.Visible = false;
                divProcesoVisitas.Visible = false;
                divProcesoAmbos.Visible = true;

                CargarProcesosTodos(procesosActuales);
                return;
            }

            List<Dictionary<int, List<BeProceso>>> procesosAgrupados = new List<Dictionary<int, List<BeProceso>>>();

            CargarProcesos_Mostrar(procesosActuales, procesosAgrupados);
        }

        private void CargarProcesosTodos(List<BeProceso> procesosActuales)
        {
            gvProcesoAmbos.DataSource = procesosActuales;
            gvProcesoAmbos.DataBind();
        }

        private void LimpiarControles()
        {
            #region Creando un Proceso Vacio

            BeProceso procesoVacio = new BeProceso();
            procesoVacio.NombrePersona = string.Empty;

            List<BeProceso> procesos = new List<BeProceso>();
            procesos.Add(procesoVacio);

            #endregion

            gvInactivos.DataSource = procesos;
            gvInactivos.DataBind();

            gvEnProceso.DataSource = procesos;
            gvEnProceso.DataBind();

            gvEnAprobacion.DataSource = procesos;
            gvEnAprobacion.DataBind();

            gvAprobados.DataSource = procesos;
            gvAprobados.DataBind();
        }

        private void CargarProcesos_Mostrar(List<BeProceso> listaProcesos, List<Dictionary<int, List<BeProceso>>> procesosAgrupados)
        {
            #region Asignando Procesos a la Lista Correcta

            Dictionary<int, List<BeProceso>> procesoDialogo;
            Dictionary<int, List<BeProceso>> procesoVisitas;

            #region Separar lista Por Procesos

            if (procesosAgrupados.Count > 1)
            {
                procesoDialogo = procesosAgrupados[0];
                procesoVisitas = procesosAgrupados[1];
            }
            else
            {
                procesoDialogo = new Dictionary<int, List<BeProceso>>();
                procesoVisitas = new Dictionary<int, List<BeProceso>>();
            }

            #endregion

            foreach (BeProceso item in listaProcesos)
            {
                #region Poblar los Diccionarios de Procesos

                if (item.Tipo == TipoProceso.Dialogo)
                {
                    CargarProcesoEnDiccionario(procesoDialogo, item);
                }
                else
                {
                    CargarProcesoEnDiccionario(procesoVisitas, item);
                }

                #endregion
            }

            #endregion

            if (procesoDialogo.Count > 0)
            {
                divProcesoDialogo.Visible = true;
                divProcesoVisitas.Visible = false;
                divProcesoAmbos.Visible = false;

                MostrarProcesoDialogo(procesoDialogo);
            }
            else if (procesoVisitas.Count > 0)
            {
                divProcesoVisitas.Visible = true;
                divProcesoDialogo.Visible = false;
                divProcesoAmbos.Visible = false;

                MostrarProcesoVisitas(procesoVisitas);
            }
        }

        private void MostrarProcesoVisitas(Dictionary<int, List<BeProceso>> procesoVisitas)
        {
            #region Separando Valores por Estado

            List<BeProceso> procesosActivos = new List<BeProceso>();
            List<BeProceso> procesosPosVisitas = new List<BeProceso>();
            List<BeProceso> procesosCerrados = new List<BeProceso>();
            List<BeProceso> procesosNoIniciados = new List<BeProceso>();

            if (procesoVisitas.ContainsKey(int.Parse(Constantes.EstadoVisitaActivo)))
            {
                procesosActivos.AddRange(procesoVisitas[int.Parse(Constantes.EstadoVisitaActivo)]);
            }

            if (procesoVisitas.ContainsKey(int.Parse(Constantes.EstadoVisitaCerrado)))
            {
                procesosCerrados.AddRange(procesoVisitas[int.Parse(Constantes.EstadoVisitaCerrado)]);
            }

            if (procesoVisitas.ContainsKey(0))
            {
                procesosNoIniciados.AddRange(procesoVisitas[0]);
            }

            if (procesoVisitas.ContainsKey(int.Parse(Constantes.EstadoVisitaPostDialogo)))
            {
                procesosPosVisitas = procesoVisitas[int.Parse(Constantes.EstadoVisitaPostDialogo)];
            }

            #endregion

            #region Visitas Activas y Cerradas

            List<BeProceso> procesoHabiles = new List<BeProceso>();
            procesoHabiles.AddRange(procesosActivos);
            procesoHabiles.AddRange(procesosCerrados);
            procesoHabiles.AddRange(procesosNoIniciados);

            gviewCrearVisita.DataSource = procesoHabiles;
            gviewCrearVisita.DataBind();

            #endregion

            #region Visitas Post

            gviewPostVisita.DataSource = procesosPosVisitas;
            gviewPostVisita.DataBind();

            #endregion

            #region Visitas Consultas

            List<BeProceso> procesosConsultas = new List<BeProceso>();

            procesosConsultas.AddRange(procesosActivos);
            procesosConsultas.AddRange(procesosCerrados);
            procesosConsultas.AddRange(procesosPosVisitas);

            gviewConsultaVista.DataSource = procesosConsultas;
            gviewConsultaVista.DataBind();

            #endregion
        }

        private void CargarProcesoEnDiccionario(Dictionary<int, List<BeProceso>> procesoDialogo, BeProceso item)
        {
            if (procesoDialogo.ContainsKey(item.Estado))
            {
                procesoDialogo[item.Estado].Add(item);
            }
            else
            {
                List<BeProceso> procesos = new List<BeProceso>();
                procesos.Add(item);

                procesoDialogo.Add(item.Estado, procesos);
            }
        }

        private void AsignarValorDiccionarioContenedor(List<Dictionary<int, List<BeProceso>>> listaProcesos)
        {
            Dictionary<int, List<BeProceso>> procesoDialogo = listaProcesos[0];

            MostrarProcesoDialogo(procesoDialogo);
        }

        private void MostrarProcesoDialogo(Dictionary<int, List<BeProceso>> procesoDialogo)
        {
            if (procesoDialogo.ContainsKey(int.Parse(Constantes.EstadoProcesoEnviado)))
            {
                gvInactivos.DataSource = procesoDialogo[int.Parse(Constantes.EstadoProcesoEnviado)];
                gvInactivos.DataBind();
            }

            if (procesoDialogo.ContainsKey(int.Parse(Constantes.EstadoProcesoActivo)))
            {
                gvEnProceso.DataSource = procesoDialogo[int.Parse(Constantes.EstadoProcesoActivo)];
                gvEnProceso.DataBind();
            }

            if (procesoDialogo.ContainsKey(int.Parse(Constantes.EstadoProcesoRevision)))
            {
                gvEnAprobacion.DataSource = procesoDialogo[int.Parse(Constantes.EstadoProcesoRevision)];
                gvEnAprobacion.DataBind();
            }

            if (procesoDialogo.ContainsKey(int.Parse(Constantes.EstadoProcesoCulminado)))
            {
                gvAprobados.DataSource = procesoDialogo[int.Parse(Constantes.EstadoProcesoCulminado)];
                gvAprobados.DataBind();
            }
        }

        protected void gvPreRender(object sender, EventArgs e)
        {
            GridView grillaActual = (GridView)sender;
            grillaActual.UseAccessibleHeader = false;
            if (grillaActual.HeaderRow != null)
                grillaActual.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        protected void btnMostrarDetalle_Click(object sender, EventArgs e)
        {
            if (MostrarDetalleProceso != null)
                MostrarDetalleProceso(sender, EventArgs.Empty);
        }
    }
}