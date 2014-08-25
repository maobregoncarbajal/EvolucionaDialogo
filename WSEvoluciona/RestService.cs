using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Configuration;

namespace WSEvoluciona
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class RestService : IRestService
    {
       
        public Resultado GetListUser(string prefijoIsoPais, string fechaModi)
        {

            Resultado res = new Resultado();

            var date = IsDate(fechaModi);
            var pais = IsPais(prefijoIsoPais,"GetListUser");

            if (date && pais)
            {
                DaRestService da = new DaRestService();
                res = da.GetListUser(prefijoIsoPais, fechaModi);
                return res;
            }
            else { 

                res.Codigo = "0";

                if (!date)
                {
                    res.Mensaje = ConfigurationSettings.AppSettings["msjFecha"].ToString();
                }
                else if (!pais) {
                    res.Mensaje = ConfigurationSettings.AppSettings["msjPais"].ToString();
                }

                return res;
            
            }
        }


        public Resultado GetDataUser(string documentoIdentidad, string rol, string prefijoIsoPais, string cub)
        {
            Resultado res = new Resultado();

            var rl = IsRol(rol);
            var pais = IsPais(prefijoIsoPais,"GetDataUser");

            if (rl && pais)
            {
                DaRestService da = new DaRestService();
                res = da.GetDataUser(documentoIdentidad, rol, prefijoIsoPais, cub);
                return res;
            }
            else {


                res.Codigo = "0";

                if (!rl)
                {
                    res.Mensaje = ConfigurationSettings.AppSettings["msjRol"].ToString();
                }
                else if (!pais)
                {
                    res.Mensaje = ConfigurationSettings.AppSettings["msjPais"].ToString();
                }

                return res;
            
            }

        }



        public static bool IsDate(string inputDate)
        {
            bool isDate = true;
            try
            {
                DateTime dateValue;
                dateValue = DateTime.ParseExact(inputDate, "yyyy-MM-dd", null);
            }
            catch
            {
                isDate = false;
            }
            return isDate;
        }



        public static bool IsPais(string inputPais, string nombreMetodo)
        {
            var isPais = true;

            switch (inputPais)
            {
                case "AR":{return isPais;}
                case "BO": { return isPais; }
                case "CL": { return isPais; }
                case "CO": { return isPais; }
                case "CR": { return isPais; }
                case "DO": { return isPais; }
                case "EC": { return isPais; }
                case "SV": { return isPais; }
                case "GT": { return isPais; }
                case "MX": { return isPais; }
                case "PA": { return isPais; }
                case "PE": { return isPais; }
                case "PR": { return isPais; }
                case "VE": { return isPais; }
                case "0": {
                    switch (nombreMetodo)
                    {
                        case "GetListUser":{return isPais;}
                        case "GetDataUser":{return false;}
                        default: { return false; }
                    }
                }
                default: { return false; }

            }

        }


        public static bool IsRol(string inputRol)
        {
            var isRol = true; ;
            switch (inputRol)

            {
                case "DV": { return isRol; }
                case "GR": { return isRol; }
                case "GZ": { return isRol; }
                case "0": { return isRol; }
                default: { return false; }
            }

        }


    }
}