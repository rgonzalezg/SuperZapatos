using System.Collections.Generic;
using SuperZapatos.ProductCatalog.Model;
using SuperZapatos.ProductCatalog.Entity;

namespace SuperZapatos.ProductCatalog.Service
{
    /// <summary>
    /// Store Service Contract
    /// </summary>
    public interface IStoreService
    {
        StoreEntity Get(int articleId);
        IEnumerable<StoreEntity> GetAll();
        int Create(StoreEntity articleEntity);
        bool Update(int articleId, StoreEntity articleEntity);
        bool Delete(int articleId);
    }
}
