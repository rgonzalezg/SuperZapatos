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
    public class ArticlesController : Controller
    {       


        public ArticlesController()
        {
          
        }
        //
        // GET: /Articles/
        public async Task<ActionResult> Index()
        {
            using (var client = new AuthenticatedClient())
            {
                HttpResponseMessage responseMessage = await client.GetAsync("/services/articles");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    JObject googleSearch = JObject.Parse(responseData);
                    var Articles = JsonConvert.DeserializeObject<List<ArticleEntity>>(googleSearch["Articles"].ToString());
                    Articles = Articles ?? new List<ArticleEntity>();
                    return View(Articles);
                }
                return View(new List<ArticleEntity>());
            }
        }

        //
        // GET: /Articles/Create
        public async Task<ActionResult> Create()
        {
            using (var client = new AuthenticatedClient())
            {
                HttpResponseMessage responseStoresMessage = await client.GetAsync("/services/stores");

                if (responseStoresMessage.IsSuccessStatusCode)
                {
                    var responseData = responseStoresMessage.Content.ReadAsStringAsync().Result;
                    var googleSearch = JObject.Parse(responseData);
                    var Stores = JsonConvert.DeserializeObject<List<StoreEntity>>(googleSearch["Stores"].ToString());
                    ViewBag.Stores = Stores;
                }
                return View();
            }
        }


        //
        // POST: /Articles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ArticleEntity article)
        {
            using (var client = new AuthenticatedClient())
            {
                if (ModelState.IsValid)
                {
                    var response = await client.PostAsJsonAsync("/services/articles", article).ConfigureAwait(false);
                    return RedirectToAction("Index");
                }
                return View("Error");
            }
        }

        //
        // GET: /Articles/Edit
        public async Task<ActionResult> Edit(int articleId)
        {
            using (var client = new AuthenticatedClient())
            {
                HttpResponseMessage responseMessage = await client.GetAsync("/services/articles/" + articleId);

                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    JObject googleSearch = JObject.Parse(responseData);
                    var Articles = JsonConvert.DeserializeObject<ArticleEntity>(googleSearch["Article"].ToString());

                    HttpResponseMessage responseStoresMessage = await client.GetAsync("/services/stores");

                    if (responseStoresMessage.IsSuccessStatusCode)
                    {
                        responseData = responseStoresMessage.Content.ReadAsStringAsync().Result;
                        googleSearch = JObject.Parse(responseData);
                        var Stores = JsonConvert.DeserializeObject<List<StoreEntity>>(googleSearch["Stores"].ToString());
                        ViewBag.Stores = Stores;
                    }
                    return View(Articles);
                }
                return View("Error");
            }
        }

        //
        // POST: /Articles/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ArticleEntity article)
        {
            using (var client = new AuthenticatedClient())
            {
                if (ModelState.IsValid)
                {
                    var response = await client.PutAsJsonAsync("/services/articles/" + article.ArticleId, article).ConfigureAwait(false);
                    return RedirectToAction("Index");
                }
                return View("Error");
            }
        }

        //
        // GET: /Articles/Delete
        public async Task<ActionResult> Delete(int articleId)
        {
            using (var client = new AuthenticatedClient())
            {
                HttpResponseMessage responseMessage = await client.GetAsync("/services/articles/" + articleId);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    JObject googleSearch = JObject.Parse(responseData);
                    var Article = JsonConvert.DeserializeObject<ArticleEntity>(googleSearch["Article"].ToString());
                    return View(Article);
                }
                return View("Error");
            }
        }

        //
        // POST: /Articles/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(ArticleEntity article)
        {
            using (var client = new AuthenticatedClient())
            {
                var response = await client.DeleteAsync("/services/articles/" + article.ArticleId);
                return RedirectToAction("Index");            
            }
        }

    }
}
