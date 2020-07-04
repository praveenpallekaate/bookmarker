using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Web;
using System.Threading.Tasks;
using URLCatalogue.API.Common;
using URLCatalogue.API.Helper;
using URLCatalogue.API.Model;

namespace URLCatalogue.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ADUserController : ControllerBase
    {
        private readonly UserHelper _userHelper = null;

        public ADUserController(
            ITokenAcquisition tokenAcquisitionValue,
            IOptions<AppConfig> appConfigs
        )
        {
            _userHelper = new UserHelper(tokenAcquisitionValue, appConfigs);
        }

        [HttpGet]
        [AuthorizeForScopes(Scopes = new[] { Constants.ScopeUserReadAll })]
        public async Task<IActionResult> Get()
        {
            var result = await _userHelper.CurrentUser();

            return Ok(result);
        }
    }
}