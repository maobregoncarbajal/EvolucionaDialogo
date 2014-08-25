
namespace Evoluciona.Dialogo.BusinessEntity
{
    public class BeCondiciones
    {
        public BeCondiciones()
        {
            prefijoIsoPais = "";
            tipoCondicion = "";
            descripcionCondicion = "";
            numeroCondicionLineamiento = "";
            estadoActivo = 0;
        }

        public string prefijoIsoPais { get; set; }
        public string tipoCondicion { get; set; }
        public string descripcionCondicion { get; set; }
        public string numeroCondicionLineamiento { get; set; }
        public int estadoActivo { get; set; }
    }

    public class BeCondicionesDetalle
    {
        public BeCondicionesDetalle()
        {
            tipoCondicion = "";
            numeroCondicionLineamiento = "";
            descripcionVariables = "";
            valorVariable = 0;
            tipoVariable = "";
        }

        public string prefijoIsoPais { get; set; }
        public string tipoCondicion { get; set; }
        public string numeroCondicionLineamiento { get; set; }
        public string descripcionVariables { get; set; }
        public decimal valorVariable { get; set; }
        public string tipoVariable { get; set; }
    }
}
