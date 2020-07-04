using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using URLCatalogue.API.Data;
using URLCatalogue.API.Model;
using URLCatalogue.API.Model.Data;
using URLCatalogue.API.Model.View;

namespace URLCatalogue.API.Service
{
    public class CatalogueManagement : ICatalogueManagement
    {
        private readonly IRepository<Catalogues> _catalogueRepository = null;
        private IEnumerable<CatalogueDetail> catalogues = null;

        public CatalogueManagement(IOptions<AppConfig> appConfigs)
        {
            _catalogueRepository = new CatalogueRepository(appConfigs);
        }

        public string AddCatalogue(CatalogueDetail item)
        {
            Guid catalogueId = Guid.NewGuid();

            var catalogue = GetCatalogues().FirstOrDefault(i => i.Type.ToLower() == item.Type.Trim().ToLower());

            var catalogues = item.CatalogueList
                .Select(i => new Catalogue
                {
                    Id = Guid.NewGuid(),
                    Details = i.Details.Trim().ToLower(),
                    Name = i.Name.Trim().ToLower(),
                    Path = i.Path.Trim().ToLower(),
                    Category = i.Category.Trim().ToLower(),
                    Tags = i.Tags.Trim().ToLower(),
                    TileImageUrl = i.TileImageUrl.Trim().ToLower(),
                    IsActive = true
                });

            if (catalogue == null)
            {
                _catalogueRepository.CreateItem(new Catalogues
                {
                    CatalogueList = catalogues,
                    Id = catalogueId,
                    IsActive = true,
                    Type = item.Type.Trim().ToLower()
                });
            }
            else
            {
                var catalogueToUpdate = _catalogueRepository.GetItem(k => k.Type.ToLower() == item.Type.Trim().ToLower());

                List<Catalogue> newCatalogues = new List<Catalogue>();
                newCatalogues = catalogueToUpdate.CatalogueList.ToList();
                newCatalogues.Add(catalogues.FirstOrDefault());

                catalogueToUpdate.CatalogueList = newCatalogues;

                _catalogueRepository.UpdateItem(j => j.Id == catalogue.Id, catalogueToUpdate);
            }

            ResetGlobals();

            return catalogueId.ToString();
        }

        public void DeleteCatalogue(string type, string id)
        {
            if (!string.IsNullOrEmpty(type) && !string.IsNullOrEmpty(id))
            {
                var catalogue = _catalogueRepository.GetItem(i => i.Type.ToLower() == type.ToLower() && i.IsActive);

                foreach(Catalogue item in catalogue.CatalogueList)
                {
                    if (item.Id == new Guid(id))
                        item.IsActive = false;
                }

                _catalogueRepository.UpdateItem(j => j.Type.ToLower() == type.ToLower() && j.IsActive, catalogue);

                ResetGlobals();
            }
        }

        public string EditCatalogue(CatalogueUpdateDetail item)
        {
            var catalogue = _catalogueRepository.GetItem(i => i.Type.ToLower() == item.Type.ToLower() && i.IsActive);
            var toCheck = catalogue.CatalogueList.FirstOrDefault(k => k.Id == item.Id);
            var duplicate = catalogue.CatalogueList
                .FirstOrDefault(k => k.Name != toCheck.Name && k.Name == item.Name && k.IsActive);

            if (duplicate != null) return string.Empty;

            foreach (Catalogue record in catalogue.CatalogueList)
            {
                if (record.Id == item.Id)
                {
                    record.Details = item.Details.Trim().ToLower();
                    record.Name = item.Name.Trim().ToLower();
                    record.Path = item.Path.Trim().ToLower();
                    record.Category = item.Category.Trim().ToLower();
                    record.Tags = item.Tags.Trim().ToLower();
                    record.TileImageUrl = item.TileImageUrl.Trim().ToLower();
                }
            }

            _catalogueRepository.UpdateItem(j => j.Type.ToLower() == item.Type.ToLower() && j.IsActive, catalogue);

            ResetGlobals();

            return item.Id.ToString();
        }

        public IEnumerable<CatalogueDetail> GetAllCatalogues()
        {
            return _catalogueRepository.GetAllItems()
                .Select(i =>
                {
                    return new CatalogueDetail
                    {
                        CatalogueList = i.CatalogueList,
                        Id = i.Id,
                        IsActive = i.IsActive,
                        Type = i.Type
                    };
                });
        }

        public IEnumerable<CatalogueDetail> GetActiveCatalogues()
        {
            return GetAllCatalogues().Where(i => i.IsActive)
                .Select(j => new CatalogueDetail
                {
                    CatalogueList = j.CatalogueList.Where(k => k.IsActive),
                    Id = j.Id,
                    IsActive = j.IsActive,
                    Type = j.Type
                });
        }

        public CatalogueDetail GetCatalogueDetailForType(string type)
        {
            return GetCatalogues().FirstOrDefault(i => i.Type.ToLower() == type.ToLower());
        }

        public string CheckExists(CatalogueDetail item)
        {
            var compare = item.CatalogueList.FirstOrDefault();

            var catalogue = GetCatalogues().FirstOrDefault(i => i.Type.ToLower() == item.Type.ToLower());

            if (catalogue == null) return null;

            var exists = catalogue?.CatalogueList.FirstOrDefault(j => j.Name.ToLower() == compare.Name.ToLower());

            return exists?.Id.ToString();
        }

        public void ResetGlobals()
        {
            catalogues = null;
        }

        private IEnumerable<CatalogueDetail> GetCatalogues()
        {
            if (catalogues == null) catalogues = GetActiveCatalogues();

            return catalogues;
        }
    }
}
