using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace WSEvoluciona
{
    [ServiceContract]
    public interface IRestService
    {

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "{prefijoIsoPais}/{fechaModi}")]
        Resultado GetListUser(string prefijoIsoPais, string fechaModi);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "{documentoIdentidad}/{rol}/{prefijoIsoPais}/{cub}")]
        Resultado GetDataUser(string documentoIdentidad, string rol, string prefijoIsoPais, string cub);

    }
}
