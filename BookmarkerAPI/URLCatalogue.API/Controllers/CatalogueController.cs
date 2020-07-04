using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using URLCatalogue.API.Model;
using URLCatalogue.API.Model.View;
using URLCatalogue.API.Service;

namespace URLCatalogue.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogueController : ControllerBase
    {
        private readonly ICatalogueManagement _catalogueManagement = null;

        public CatalogueController(
            IOptions<AppConfig> appConfigs, 
            ICatalogueManagement catalogueManagement
        )
        {
            _catalogueManagement = catalogueManagement;
            _catalogueManagement.ResetGlobals();
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _catalogueManagement.GetAllCatalogues();

            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpGet("{type}")]
        public IActionResult Get(string type)
        {
            var result = _catalogueManagement.GetCatalogueDetailForType(type);

            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post(CatalogueDetail catalogueDetail)
        {
            if (catalogueDetail == null) return BadRequest();

            var checkDuplicate = _catalogueManagement.CheckExists(catalogueDetail);

            if (checkDuplicate == null)
            {
                var result = _catalogueManagement.AddCatalogue(catalogueDetail);

                if (result == null) return StatusCode(StatusCodes.Status500InternalServerError);

                return Ok(result);
            }
            else
                return Conflict(checkDuplicate);
        }

        [HttpPut]
        public IActionResult Put(CatalogueUpdateDetail catalogueUpdateDetail)
        {
            if (catalogueUpdateDetail == null) return BadRequest();

            var result = _catalogueManagement.EditCatalogue(catalogueUpdateDetail);

            if (result == null) return NotFound();
            else if (string.IsNullOrEmpty(result)) return Conflict();

            return Ok(result);
        }

        [HttpGet("{type}/{id}")]
        public IActionResult Remove(string type, string id)
        {
            if (string.IsNullOrEmpty(type) || string.IsNullOrEmpty(id)) return BadRequest();

            _catalogueManagement.DeleteCatalogue(type, id);

            return Ok();
        }
    }
}