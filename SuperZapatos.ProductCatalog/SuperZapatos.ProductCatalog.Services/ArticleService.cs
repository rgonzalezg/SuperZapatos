using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using SuperZapatos.ProductCatalog.Model;
using AutoMapper;
using SuperZapatos.ProductCatalog.Entity;

namespace SuperZapatos.ProductCatalog.Service
{
    /// <summary>
    /// Offers services for aritcles specific CRUD operations
    /// </summary>
    public class ArticleService : IArticleService
    {       
        private ProductCatalogEntities dbContext = new ProductCatalogEntities();
        /// <summary>
        /// Public constructor.
        /// </summary>
        public ArticleService()
        {          
        }

        /// <summary>
        /// Fetches article details by id
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        public ArticleEntity Get(int articleId)
        {
            var article = dbContext.Articles.Where(x => x.ArticleId == articleId).FirstOrDefault();
            if (article != null)
            {
                Mapper.CreateMap<Article, ArticleEntity>();
                var productModel = Mapper.Map<Article, ArticleEntity>(article);
                return productModel;
            }
            return null; 
        }

        /// <summary>
        /// Fetches all the articles.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ArticleEntity> GetAll()
        {
            var articles = dbContext.Articles.ToList();
            if (articles.Any())
            {
                Mapper.CreateMap<Article, ArticleEntity>();
                var productsModel = Mapper.Map<List<Article>, List<ArticleEntity>>(articles);
                return productsModel;
            }
            return null;
        }

        /// <summary>
        /// Creates a article
        /// </summary>
        /// <param name="articleEntity"></param>
        /// <returns></returns>
        public int Create(ArticleEntity articleEntity)
        {
            var article = new Article
                {
                    Name = articleEntity.Name,
                    Description = articleEntity.Description,
                    Price = articleEntity.Price,
                    TotalInShelf = articleEntity.TotalInShelf,
                    TotalInVault = articleEntity.TotalInVault,
                    StoreId = articleEntity.StoreId
                };
            dbContext.Articles.Add(article);
            dbContext.SaveChanges();
            return article.ArticleId;
          
        }

        /// <summary>
        /// Updates a article
        /// </summary>
        /// <param name="articleId"></param>
        /// <param name="articleEntity"></param>
        /// <returns></returns>
        public bool Update(int articleId, ArticleEntity articleEntity)
        {
            var success = false;
            if (articleEntity != null)
            {
                var article = dbContext.Articles.Where(x => x.ArticleId == articleId).FirstOrDefault();
                if (article != null)
                {
                    article.Name = articleEntity.Name;
                    article.Description = articleEntity.Description;
                    article.Price = articleEntity.Price;
                    article.TotalInShelf = articleEntity.TotalInShelf;
                    article.TotalInVault = articleEntity.TotalInVault;
                    article.StoreId = articleEntity.StoreId;
                    dbContext.SaveChanges();              
                    success = true;
                }
            }
            return success;
        }

        /// <summary>
        /// Deletes a particular article
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        public bool Delete(int articleId)
        {
            var success = false;
            if (articleId > 0)
            {
                var article = dbContext.Articles.Where(x => x.ArticleId == articleId).FirstOrDefault();
                if (article != null)
                {
                    dbContext.Articles.Remove(article);
                    dbContext.SaveChanges();
                    success = true;
                }
            }
            return success;
        }
    }
}
