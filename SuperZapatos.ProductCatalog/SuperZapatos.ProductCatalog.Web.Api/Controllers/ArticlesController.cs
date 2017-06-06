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
    public class ArticlesController : ApiController
    {
        #region Private variable.

        private readonly IArticleService _articleService;
        private readonly IStoreService _storeService;

        #endregion

        #region Public Constructor

        /// <summary>
        /// Public constructor to initialize article service instance
        /// </summary>
        public ArticlesController(IArticleService articleServices, IStoreService storeService)
        {
            _articleService = articleServices;
            _storeService = storeService;
        }

        #endregion

        // GET services/articles
        public HttpResponseMessage Get()
        {
            var articles = _articleService.GetAll();
            if (articles != null)
                return Request.CreateResponse(HttpStatusCode.OK, articles);
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Articles not found");
        }

        // GET services/articles/:id
        public HttpResponseMessage Get(int id)
        {
            var article = _articleService.Get(id);
            if (article != null)
                return Request.CreateResponse(HttpStatusCode.OK, article);
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No article found for this id");
        }

        // POST services/articles
        public HttpResponseMessage Post([FromBody] ArticleEntity articleEntity)
        {
            var store = _storeService.Get(articleEntity.StoreId);
            if (store != null)
            {
                _articleService.Create(articleEntity);
                return Request.CreateResponse(HttpStatusCode.OK, articleEntity);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No store found for this id");
        }

        // PUT services/articles/:id
        public HttpResponseMessage Put(int id, [FromBody] ArticleEntity articleEntity)
        {
            if (id > 0)
            {
                var store = _storeService.Get(articleEntity.StoreId);
                if (store != null)
                {
                    _articleService.Update(id, articleEntity);
                    return Request.CreateResponse(HttpStatusCode.OK, articleEntity);
                }
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No article found for this id");
        }

        // DELETE services/articles
        public HttpResponseMessage Delete(int id)
        {
            if (id > 0)
            {
                _articleService.Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No article found for this id");
        }
    }
}
