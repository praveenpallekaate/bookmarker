using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using URLCatalogue.API.Common;
using URLCatalogue.API.Model;
using URLCatalogue.API.Model.Data;

namespace URLCatalogue.API.Data
{
    public class CatalogueRepository : RepositoryBase<Catalogues>, IRepository<Catalogues>
    {
        private readonly string _collection = string.Empty;

        public CatalogueRepository(IOptions<AppConfig> appConfigs) : base(appConfigs)
        {
            _collection = AppCollections.Catalogues.ToString();
        }

        public void CreateItem(Catalogues item)
        {
            MongoDBStorage<Catalogues>.CreateItem(_collection, item);
        }

        public async Task<IEnumerable<Catalogues>> GetAllItemsAsync()
        {
            return await MongoDBStorage<Catalogues>.GetAllItemsAsync(_collection);
        }

        public IEnumerable<Catalogues> GetAllItems()
        {
            return GetAllItemsAsync().Result;
        }

        public async Task<IEnumerable<Catalogues>> GetItemsAsync(Expression<Func<Catalogues, bool>> predicate)
        {
            return await MongoDBStorage<Catalogues>.GetItemsAsync(_collection, predicate);
        }

        public IEnumerable<Catalogues> GetItems(Expression<Func<Catalogues, bool>> predicate)
        {
            return GetItemsAsync(predicate).Result;
        }

        public async Task<Catalogues> GetItemAsync(Expression<Func<Catalogues, bool>> predicate)
        {
            return await MongoDBStorage<Catalogues>.GetItemAsync(_collection, predicate);
        }

        public Catalogues GetItem(Expression<Func<Catalogues, bool>> predicate)
        {
            return GetItemAsync(predicate).Result;
        }

        public void UpdateItem(Expression<Func<Catalogues, bool>> predicate, Catalogues item)
        {
            MongoDBStorage<Catalogues>.UpdateItem(_collection, predicate, item);
        }
    }
}
