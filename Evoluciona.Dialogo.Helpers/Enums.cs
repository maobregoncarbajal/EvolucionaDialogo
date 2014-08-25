
namespace Evoluciona.Dialogo.Helpers
{
    public enum TipoHistorial
    {
        CriticasAntes = 1,
        CriticasDurante = 2
    }

    public enum TipoEncuesta
    {
        EncuestaEvaluador = 1,
        EncuestaEvaluado = 2
    }

    public enum TipoPantalla
    {
        Antes = 1,
        Durante = 2,
        Despues = 3
    }

    public enum TipoProceso
    {
        Dialogo = 0,
        Visita = 1,
        Todos = -1
    }

    public enum TipoRol
    {
        DirectoraVentas = 1,
        GerenteRegion = 2,
        GerenteZona = 3
    }

    public enum JqGridGroupOp
    {
        AND,
        OR
    }

    public enum JqGridOperations
    {
        eq, // "equal"
        ne, // "not equal"
        lt, // "less"
        le, // "less or equal"
        gt, // "greater"
        ge, // "greater or equal"
        bw, // "begins with"
        bn, // "does not begin with"
        //in, // "in"
        //ni, // "not in"
        ew, // "ends with"
        en, // "does not end with"
        cn, // "contains"
        nc  // "does not contain"
    }

}
