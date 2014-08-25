using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace WSEvoluciona
{
    [DataContract]
    public class Usuario
    {

        [DataMember]
        public string Rol;

        [DataMember]
        public string PrefijoIsoPais;

        [DataMember]
        public string NombreCompleto;

        [DataMember]
        public string CUB;

        [DataMember]
        public string CodigoRegion;

        [DataMember]
        public string CodigoZona;

        [DataMember]
        public string FechaModi;

        [DataMember]
        public string DocumentoIdentidad;

        [DataMember]
        public string CodigoTransac;

    }
}