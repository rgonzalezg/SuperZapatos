using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SuperZapatos.ProductCatalog.Service;
using SuperZapatos.ProductCatalog.Model;
using SuperZapatos.ProductCatalog.Entity;


namespace WebApi.Controllers
{
    [Authorize]
    public class StoresController : ApiController
    {
        #region Private variable.

        private readonly IStoreService _storeService;

        #endregion

        #region Public Constructor

        /// <summary>
        /// Public constructor to initialize store service instance
        /// </summary>
        public StoresController(IStoreService storeServices)
        {
            _storeService = storeServices;
        }

        #endregion

        // GET services/stores
        public HttpResponseMessage Get()
        {
            var stores = _storeService.GetAll();
            if (stores != null)
                return Request.CreateResponse(HttpStatusCode.OK, stores);
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Stores not found");
        }

        // GET services/stores/:id
        public HttpResponseMessage Get(int id)
        {
            var store = _storeService.Get(id);
            if (store != null)
                return Request.CreateResponse(HttpStatusCode.OK, store);
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No store found for this id");
        }

        // POST services/stores
        public int Post([FromBody] StoreEntity storeEntity)
        {
            return _storeService.Create(storeEntity);
        }

        // PUT services/stores/:id
        public bool Put(int id, [FromBody] StoreEntity storeEntity)
        {
            if (id > 0)
            {
                return _storeService.Update(id, storeEntity);
            }
            return false;
        }

        // DELETE services/stores
        public bool Delete(int id)
        {
            if (id > 0)
                return _storeService.Delete(id);
            return false;
        }
    }
}
