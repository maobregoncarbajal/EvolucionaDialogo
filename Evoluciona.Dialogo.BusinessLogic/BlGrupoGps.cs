
namespace Evoluciona.Dialogo.BusinessLogic
{
    using BusinessEntity;
    using DataAccess;
    using System;
    using System.Collections.Generic;

    public class BlGrupoGps
    {
        private static readonly DaGrupoGps DaGrupoGps = new DaGrupoGps();

        public List<BeGrupoGps> ObtenerGruposGps(string prefijoIsoPais)
        {
            try
            {
                var entidades = DaGrupoGps.ObtenerGruposGps(prefijoIsoPais);

                return entidades;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int GrabarGruposGps(string prefijoIsoPais, string pXml)
        {
            try
            {

                return DaGrupoGps.GrabarGruposGps(prefijoIsoPais, pXml);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
