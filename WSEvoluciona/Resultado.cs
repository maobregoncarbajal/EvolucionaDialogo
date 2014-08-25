using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace WSEvoluciona
{
    [DataContract]
    public class Resultado
    {
        [DataMember]
        public string Codigo;

        [DataMember]
        public string Mensaje;

        [DataMember]
        public List<Usuario> ListaUsuarios;

    }
}