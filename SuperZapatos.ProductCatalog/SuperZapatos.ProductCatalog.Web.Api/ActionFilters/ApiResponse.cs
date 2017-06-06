using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Web;

namespace SuperZapatos.ProductCatalog.Web.Api.ActionFilters
{
    [DataContract]
    public class ApiResponse
    {      
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public object Articles { get; set; }

        [DataMember(Order = 1, EmitDefaultValue = false, IsRequired = false)]
        public object Stores { get; set; }

        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public object Article { get; set; }

        [DataMember(Order = 1, EmitDefaultValue = false, IsRequired = false)]
        public object Store { get; set; }

        [DataMember]
        public bool Status { get; set; }

        [DataMember]
        public int error_code { get; set; }
     
        [DataMember(EmitDefaultValue = false)]
        public string error_msg { get; set; }      
        [DataMember(Order = 2, EmitDefaultValue = false, IsRequired = false)]
        public int Total_Elements { get; set; }


        public ApiResponse(bool status, HttpStatusCode statusCode, object result = null, string errorMessage = null)
        {
            Status = status;
            error_code = (int)statusCode;
            if (result != null)
            {
                if (result.GetType() == typeof(List<SuperZapatos.ProductCatalog.Entity.StoreEntity>))
                {
                    Stores = result;
                    Total_Elements = ((List<SuperZapatos.ProductCatalog.Entity.StoreEntity>)result).Count;
                }
                else if (result.GetType() == typeof(List<SuperZapatos.ProductCatalog.Entity.ArticleEntity>))
                {
                    Articles = result;
                    Total_Elements = ((List<SuperZapatos.ProductCatalog.Entity.ArticleEntity>)result).Count;
                }
                else if (result.GetType() == typeof(SuperZapatos.ProductCatalog.Entity.StoreEntity))
                {
                    Store = result;                 
                }
                else if (result.GetType() == typeof(SuperZapatos.ProductCatalog.Entity.ArticleEntity))
                {
                    Article = result;                   
                }
            }
            error_msg = errorMessage;
        } 
    }
}