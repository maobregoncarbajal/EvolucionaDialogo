using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Web.Configuration;
using Evoluciona.Dialogo.BusinessEntity;
using Evoluciona.Dialogo.BusinessLogic;
using Evoluciona.Dialogo.Web.WsDirectorio;

namespace Evoluciona.Dialogo.Web.Admin.Tareas
{

    public partial class TareaCargarDirectorio : System.Web.UI.Page
    {
        private static readonly BlAlbama BlAlbama = new BlAlbama();
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadDirectorio();
        }

        private void LoadDirectorio()
        {
            var paisRegionZona = "00||".Split('|');
            var pais = paisRegionZona[0].Trim();
            var region = paisRegionZona[1].Trim();
            var zona = paisRegionZona[2].Trim();
            var cargo = String.Empty;
            var periodo = String.Empty;
            var estadoCargo = String.Empty;

            var oListPaises = new List<BeComun>();

            if (String.Equals(pais, "00"))
            {
                oListPaises = BlAlbama.ListarPaises("00");
            }
            else
            {
                var oPais = new BeComun { Codigo = pais, Descripcion = String.Empty, Referencia = String.Empty };
                oListPaises.Add(oPais);
            }

            ConsumirWsObtenerClientesDirectorio(oListPaises, region, zona, cargo, periodo, estadoCargo);

            var varCarga = DirectorioCargaInClientes();
            var blWsDirectorio = new BlWsDirectorio();

            if (varCarga == "1")
            {
                blWsDirectorio.InsertarLogCargaDirectorio("DIC", "Correcto: Carga ESE_DIRECTORIO_IN_CLIENTES");
            }
            else
            {
                blWsDirectorio.InsertarLogCargaDirectorio("DIC", varCarga);
            }

        }


        private void ConsumirWsObtenerClientesDirectorio(List<BeComun> oListPaises, string region, string zona, string cargo, string periodo, string estadoCargo)
        {
            var tablaLimpiada = false;

            var blWsDirectorio = new BlWsDirectorio();

            foreach (var oPais in oListPaises)
            {

                var wsDirectorio = new ProcesoDIRWebServiceImplService { Url = WebConfigurationManager.AppSettings.Get("WsDirectorio" + oPais.Codigo), Timeout = 180000 };

                try
                {
                    ProcesoDIRWebServiceResultado resultado = wsDirectorio.obtenerClientesDirectorio(oPais.Codigo, region, zona, cargo, periodo, estadoCargo);

                    switch (resultado.codigo)
                    {
                        case "0":
                            {
                                if (!tablaLimpiada)
                                {
                                    tablaLimpiada = DeleteClientesDirectorio(oListPaises);
                                }

                                var strInsert = InsertarClientesDirectorio(resultado);

                                if (strInsert == "1")
                                {
                                    blWsDirectorio.InsertarLogCargaDirectorio(oPais.Codigo, "Correcto " + resultado.mensaje);
                                }
                                else
                                {
                                    blWsDirectorio.InsertarLogCargaDirectorio(oPais.Codigo, strInsert);
                                }
                            }
                            break;
                        default:
                            blWsDirectorio.InsertarLogCargaDirectorio(oPais.Codigo, resultado.mensaje);
                            break;
                    }
                }
                catch (Exception exception)
                {
                    blWsDirectorio.InsertarLogCargaDirectorio(oPais.Codigo, exception.Message);
                }
            }
        }


        private bool DeleteClientesDirectorio(List<BeComun> oListPaises)
        {
            var blWsDirectorio = new BlWsDirectorio();
            var cantPaises = oListPaises.Count;
            var resultado = false;

            if (cantPaises > 0)
            {
                resultado = blWsDirectorio.DeleteClientesDirectorio("00");

            }
            else
            {
                foreach (var oPais in oListPaises)
                {
                    resultado = blWsDirectorio.DeleteClientesDirectorio(oPais.Codigo);
                }
            }

            return resultado;
        }

        private string InsertarClientesDirectorio(ProcesoDIRWebServiceResultado resultado)
        {
            var dtClienteDirWebService = ConvertToDataTable(resultado.clienteDIRWebService);
            var blWsDirectorio = new BlWsDirectorio();

            return blWsDirectorio.InsertClientesDirectorio(dtClienteDirWebService);

        }

        public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            var table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }


        private string DirectorioCargaInClientes()
        {
            var blWsDirectorio = new BlWsDirectorio();
            return blWsDirectorio.DirectorioCargaInClientes();

        }



    }
}