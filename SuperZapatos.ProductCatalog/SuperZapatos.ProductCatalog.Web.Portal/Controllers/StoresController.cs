using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SuperZapatos.ProductCatalog.Entity;
using SuperZapatos.ProductCatalog.Web.Portal.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SuperZapatos.ProductCatalog.Web.Portal.Controllers
{
    public class StoresController : Controller
    {
        public StoresController()
        {
         
        }

        // GET: StoresInfo
        public async Task<ActionResult> Index()
        {
            using (var client = new AuthenticatedClient())
            {
                HttpResponseMessage responseMessage = await client.GetAsync("/services/stores");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    JObject googleSearch = JObject.Parse(responseData);
                    var Stores = JsonConvert.DeserializeObject<List<StoreEntity>>(googleSearch["Stores"].ToString());
                    return View(Stores);
                }
                return View("Error");
            }
        }


        //
        // GET: /Stores/Create
        public ActionResult Create()
        {
            return View();
        }


          //
        // POST: /Stores/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(StoreEntity store)
        {
            using (var client = new AuthenticatedClient())
            {
                if (ModelState.IsValid)
                {
                    var response = await client.PostAsJsonAsync("/services/stores", store);
                    return RedirectToAction("Index");
                }
                return View("Error");
            }
        }

        //
        // GET: /Stores/Edit
        public async Task<ActionResult> Edit(int storeId)
        {
            using (var client = new AuthenticatedClient())
            {
                HttpResponseMessage responseMessage = await client.GetAsync("/services/stores/" + storeId);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    JObject googleSearch = JObject.Parse(responseData);
                    var Store = JsonConvert.DeserializeObject<StoreEntity>(googleSearch["Store"].ToString());
                    return View(Store);
                }
                return View("Error");
            }
        }

        //
        // GET: /Stores/Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(StoreEntity store)
        {
            using (var client = new AuthenticatedClient())
            {
                if (ModelState.IsValid)
                {
                    var response = await client.PutAsJsonAsync("/services/stores/" + store.StoreId, store);
                    return RedirectToAction("Index");
                }
                return View("Error");
            }         
        }

        //
        // GET: /Stores/Delete
        public async Task<ActionResult> Delete(int storeId)
        {
            using (var client = new AuthenticatedClient())
            {
                HttpResponseMessage responseMessage = await client.GetAsync("/services/stores/" + storeId);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    JObject googleSearch = JObject.Parse(responseData);
                    var Store = JsonConvert.DeserializeObject<StoreEntity>(googleSearch["Store"].ToString());
                    return View(Store);
                }
                return View("Error");
            }
        }

        //
        // POST: /Stores/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(StoreEntity store)
        {
            using (var client = new AuthenticatedClient())
            {
                var response = await client.DeleteAsync("/services/stores/" + store.StoreId);
                return RedirectToAction("Index");                
            }
        }

    }
}
