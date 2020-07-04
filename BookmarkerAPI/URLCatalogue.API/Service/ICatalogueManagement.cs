using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using URLCatalogue.API.Model.View;

namespace URLCatalogue.API.Service
{
    public interface ICatalogueManagement
    {
        string AddCatalogue(CatalogueDetail item);
        string EditCatalogue(CatalogueUpdateDetail item);
        void DeleteCatalogue(string type, string id);
        IEnumerable<CatalogueDetail> GetAllCatalogues();
        CatalogueDetail GetCatalogueDetailForType(string type);
        string CheckExists(CatalogueDetail item);
        void ResetGlobals();
    }
}
