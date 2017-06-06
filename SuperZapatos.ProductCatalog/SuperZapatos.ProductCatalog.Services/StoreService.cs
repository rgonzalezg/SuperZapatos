using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using SuperZapatos.ProductCatalog.Model;
using AutoMapper;
using SuperZapatos.ProductCatalog.Entity;

namespace SuperZapatos.ProductCatalog.Service
{
    /// <summary>
    /// Offers services for stores specific CRUD operations
    /// </summary>
    public class StoreService : IStoreService
    {       
        private ProductCatalogEntities dbContext = new ProductCatalogEntities();
        /// <summary>
        /// Public constructor.
        /// </summary>
        public StoreService()
        {          
        }

        /// <summary>
        /// Fetches store details by id
        /// </summary>
        /// <param name="storeId"></param>
        /// <returns></returns>
        public StoreEntity Get(int storeId)
        {
            var store = dbContext.Stores.Where(x => x.StoreId == storeId).FirstOrDefault();
            if (store != null)
            {
                Mapper.CreateMap<Store, StoreEntity>();
                var storeModel = Mapper.Map<Store, StoreEntity>(store);
                return storeModel;
            }
            return null; 
        }

        /// <summary>
        /// Fetches all the stores.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<StoreEntity> GetAll()
        {
            var stores = dbContext.Stores.ToList();
            if (stores.Any())
            {
                Mapper.CreateMap<Store, StoreEntity>();
                var storesModel = Mapper.Map<List<Store>, List<StoreEntity>>(stores);
                return storesModel;
            }
            return null;
        }

        /// <summary>
        /// Creates a store
        /// </summary>
        /// <param name="storeEntity"></param>
        /// <returns></returns>
        public int Create(StoreEntity storeEntity)
        {
            var store = new Store
                {
                    Name = storeEntity.Name,
                    Address = storeEntity.Address                    
                };
            dbContext.Stores.Add(store);
            dbContext.SaveChanges();
            return store.StoreId;
          
        }

        /// <summary>
        /// Updates a store
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="storeEntity"></param>
        /// <returns></returns>
        public bool Update(int storeId, StoreEntity storeEntity)
        {
            var success = false;
            if (storeEntity != null)
            {
                var store = dbContext.Stores.Where(x => x.StoreId == storeId).FirstOrDefault();
                if (store != null)
                {
                    store.Name = storeEntity.Name;
                    store.Address = storeEntity.Address;
                    dbContext.SaveChanges();              
                    success = true;
                }
            }
            return success;
        }

        /// <summary>
        /// Deletes a particular store
        /// </summary>
        /// <param name="storeId"></param>
        /// <returns></returns>
        public bool Delete(int storeId)
        {
            var success = false;
            if (storeId > 0)
            {
                var store = dbContext.Stores.Where(x => x.StoreId == storeId).FirstOrDefault();
                if (store != null)
                {
                    dbContext.Stores.Remove(store);
                    dbContext.SaveChanges();
                    success = true;
                }
            }
            return success;
        }
    }
}
