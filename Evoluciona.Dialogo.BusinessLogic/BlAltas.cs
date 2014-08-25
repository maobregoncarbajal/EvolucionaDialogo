using System;
using System.Collections.Generic;
using Evoluciona.Dialogo.BusinessEntity;
using Evoluciona.Dialogo.DataAccess;

namespace Evoluciona.Dialogo.BusinessLogic
{
    public class BlAltas
    {
        private static readonly DaAltas OdaAltas = new DaAltas();
        private static readonly DaDataMart ObjDataMart = new DaDataMart();

        public List<BeAltas> ListaAltas(string prefijoIsoPais)
        {
            try
            {
                var entidades = OdaAltas.ListaAltas(prefijoIsoPais);

                return entidades;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool InsertarAltas(BeAltas obeAltas)
        {
            return OdaAltas.InsertarAltas(obeAltas);
        }

        public string ActualizarEstandarizacionCodigo()
        {
            return ObjDataMart.ActualizarEstandarizacionCodigo();
        }
    }
}
