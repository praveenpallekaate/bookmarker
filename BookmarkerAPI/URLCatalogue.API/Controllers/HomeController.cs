using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using System.Collections.Generic;
using URLCatalogue.API.Common;

namespace URLCatalogue.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [AuthorizeForScopes(Scopes = new[] { Constants.ScopeUserReadAll })]
        public ActionResult<IEnumerable<string>> Get()
        {
            return RedirectPermanent("/");
        }

        [HttpPost]
        [AuthorizeForScopes(Scopes = new[] { Constants.ScopeUserReadAll })]
        public ActionResult<IEnumerable<string>> Post()
        {
            return RedirectPermanent("/");
        }
    }
}