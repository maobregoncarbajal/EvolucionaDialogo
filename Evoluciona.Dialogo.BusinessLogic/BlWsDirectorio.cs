
namespace Evoluciona.Dialogo.BusinessLogic
{
    using System.Data;
    using DataAccess;


    public class BlWsDirectorio
    {
        private static readonly DaWsDirectorio DaWsDirectorio = new DaWsDirectorio();

        public bool DeleteClientesDirectorio(string codPais)
        {
            return DaWsDirectorio.DeleteClientesDirectorio(codPais);
        }

        public string InsertClientesDirectorio(DataTable dtClienteDirWebService)
        {
            return DaWsDirectorio.InsertClientesDirectorio(dtClienteDirWebService);
        }

        public void InsertarLogCargaDirectorio(string codigoPaisComercial, string descripcion)
        {
            DaWsDirectorio.InsertarLogCargaDirectorio(codigoPaisComercial, descripcion);
        }

        public string DirectorioCargaInClientes()
        {
            return DaWsDirectorio.DirectorioCargaInClientes();
        }



    }
}