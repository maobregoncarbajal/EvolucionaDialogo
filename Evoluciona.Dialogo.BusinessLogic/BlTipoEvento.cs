
namespace Evoluciona.Dialogo.BusinessLogic
{
    using BusinessEntity;
    using DataAccess;
    using System.Collections.Generic;

    public class BlTipoEvento
    {
        private static readonly DaTipoEvento DaTipoEvento = new DaTipoEvento();

        public List<BeTipoEvento> ObtenerTipoEventos(int tipoReunion, int codigoRol)
        {
            return DaTipoEvento.ObtenerTipoEventos(tipoReunion, codigoRol);
        }

        public List<BeTipoEvento> ObtenerSubEventos(int idPadre)
        {
            return DaTipoEvento.ObtenerSubEventos(idPadre);
        }

        public BeTipoEvento ObtenerTipoEvento(int idTipoEvento)
        {
            return DaTipoEvento.ObtenerTipoEvento(idTipoEvento);
        }

        public void RegistrarTipoEvento(BeTipoEvento tipoEvento)
        {
            DaTipoEvento.RegistrarTipoEvento(tipoEvento);
        }

        public void ActualizarTipoEvento(BeTipoEvento tipoEvento)
        {
            DaTipoEvento.ActualizarTipoEvento(tipoEvento);
        }

        public void EliminarTipoEvento(int idTipoEvento)
        {
            DaTipoEvento.EliminarTipoEvento(idTipoEvento);
        }
    }
}