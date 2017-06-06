using System.Collections.Generic;
using SuperZapatos.ProductCatalog.Model;
using SuperZapatos.ProductCatalog.Entity;

namespace SuperZapatos.ProductCatalog.Service
{
    /// <summary>
    /// Article Service Contract
    /// </summary>
    public interface IArticleService
    {
        ArticleEntity Get(int articleId);
        IEnumerable<ArticleEntity> GetAll();
        int Create(ArticleEntity articleEntity);
        bool Update(int articleId, ArticleEntity articleEntity);
        bool Delete(int articleId);
    }
}
