
namespace Evoluciona.Dialogo.BusinessEntity
{
    using System;

    [Serializable]
    public class BeEvento
    {
        private string _descripcion;
        public int IDEvento { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int Reunion { get; set; }

        public string DescripcionReunion
        {
            get
            {
                switch (Reunion)
                {
                    case 1:
                        return "Individual";
                    case 2:
                        return "Grupal";
                    case 3:
                        return "Otros";
                    default:
                        return string.Empty;
                }
            }
        }

        public int Evento { get; set; }
        public int SubEvento { get; set; }
        public string Evaluado { get; set; }
        public string Asunto { get; set; }
        public string CodigoUsuario { get; set; }
        public int RolUsuario { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Fecha { get; set; }

        public string Descripcion
        {
            get
            {
                if (!string.IsNullOrEmpty(_descripcion))
                    return string.Format("{0}, {1}", _descripcion, Asunto);
                if (string.IsNullOrEmpty(Filtro) || string.IsNullOrEmpty(Filtro.Trim()))
                    return string.Format("{0} - {1}", DescripcionReunion, Asunto);
                return string.Format("{0} ({1}) - {2}", DescripcionReunion, Filtro.Trim(), Asunto);
            }
            set { _descripcion = value; }
        }

        public string Hora
        {
            get { return string.Format("{0}:{1}", FechaInicio.Hour, FechaInicio.Minute); }
        }

        public string Color { get; set; }
        public string Campanha { get; set; }
        public string Filtro { get; set; }
        public string BorderColor { get; set; }
        public string TextColor { get; set; }
    }
}