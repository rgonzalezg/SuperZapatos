using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace SuperZapatos.ProductCatalog.Web.Portal.Utils
{
    public class AuthenticatedClient : HttpClient
    {
        public AuthenticatedClient()
        {
            string password = ConfigurationManager.AppSettings["Password"];
            string userName = ConfigurationManager.AppSettings["UserName"];
            string encoded = Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(userName + ":" + password));

            BaseAddress = new Uri(ConfigurationManager.AppSettings["ApiUrl"]);
            DefaultRequestHeaders.Accept.Clear();
            DefaultRequestHeaders.Add("Authorization", "Basic " + encoded);
            DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}