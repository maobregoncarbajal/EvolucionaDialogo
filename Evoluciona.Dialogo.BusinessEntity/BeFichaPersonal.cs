
namespace Evoluciona.Dialogo.BusinessEntity
{
    public class BeFichaPersonal
    {
        public string Rol { get; set; }
        public string CodigoPlanilla { get; set; }
        public string CodigoPaisADAM { get; set; }
        public string DescripcionPais { get; set; }
        public string Cub { get; set; }
        public string CedulaIdentidad { get; set; }
        public string EstadoCarga { get; set; }
        public string NombresApellidos { get; set; }
        public string FechaNacimiento { get; set; }
        public string EstadoCivil { get; set; }
        public int CantidadHijos { get; set; }
        public string Domicilio { get; set; }
        public string TelefonoFijo { get; set; }
        public string Formacion { get; set; }
        public string ExperienciaProfesional { get; set; }
        public string FechaIngreso { get; set; }
        public string PuestoActual { get; set; }
        public string CodigoRegionoZona { get; set; }
        public string DescripcionRegionoZona { get; set; }
        public string CodigoGerenteRegionoZona { get; set; }
        public string FechaAsignacionActual { get; set; }
        public string CorreoElectronico { get; set; }
        public string NombreJefe { get; set; }
        public int CantidadPersonas { get; set; }
        public int EstadoActivo { get; set; }
        public BeCentroEstudios CentroEstudios { get; set; }
        public BePuestosOcupados PuestosOcupados { get; set; }
        public BePersonaCargo PersonaCargo { get; set; }
    }

    public class BeCentroEstudios
    {
        public string CentroDeEstudios { get; set; }
        public string CodigoPlanilla { get; set; }
        public int EstadoActivo { get; set; }
    }

    public class BePuestosOcupados
    {
        public string PosicionAnterior { get; set; }
        public string CodigoPlanilla { get; set; }
        public int EstadoActivo { get; set; }
    }

    public class BePersonaCargo
    {
        public string PersonasACargo { get; set; }
        public string CodigoPlanilla { get; set; }
        public int EstadoActivo { get; set; }
    }
}
