using Microsoft.Extensions.Options;
using Microsoft.Graph;
using Microsoft.Identity.Web;
using System.Threading.Tasks;
using URLCatalogue.API.Common;
using URLCatalogue.API.Model;
using URLCatalogue.API.Model.View;

namespace URLCatalogue.API.Helper
{
    public class UserHelper
    {
        readonly ITokenAcquisition tokenAcquisition;
        readonly AppConfig webOptions;

        public UserHelper(ITokenAcquisition tokenAcquisitionValue, IOptions<AppConfig> webOptionValue)
        {
            tokenAcquisition = tokenAcquisitionValue;
            webOptions = webOptionValue.Value;
        }

        public async Task<UserDetail> CurrentUser()
        {
            var graphClient = GetGraphServiceClient(new[] { URLCatalogue.API.Common.Constants.ScopeUserReadAll });

            var me = await graphClient.Me.Request().GetAsync();

            return new UserDetail
            {
                Email = me.Mail
            };
        }

        private GraphServiceClient GetGraphServiceClient(string[] scopes)
        {
            return GraphServiceClientFactory.GetAuthenticatedGraphClient(async () =>
            {
                string result = await tokenAcquisition.GetAccessTokenOnBehalfOfUserAsync(scopes);
                return result;
            }, webOptions.GraphApiUrl);
        }
    }
}
