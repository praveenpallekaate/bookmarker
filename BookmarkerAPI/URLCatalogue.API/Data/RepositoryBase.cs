using Microsoft.Extensions.Options;
using URLCatalogue.API.Common;
using URLCatalogue.API.Helper;
using URLCatalogue.API.Model;

namespace URLCatalogue.API.Data
{
    public class RepositoryBase<T> where T : IMongoDbModel
    {
        public RepositoryBase(IOptions<AppConfig> appConfigs)
        {
            MongoHelper.SetDatabaseDetailsFromConfig(appConfigs);
            MongoDBStorage<T>.Initialize();
        }
    }
}
