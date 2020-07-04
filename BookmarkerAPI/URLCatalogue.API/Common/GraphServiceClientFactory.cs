using Microsoft.Graph;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace URLCatalogue.API.Common
{
    public class GraphServiceClientFactory
    {
        public static GraphServiceClient GetAuthenticatedGraphClient(Func<Task<string>> acquireAccessToken,
                                                                                 string baseUrl)
        {
            return new GraphServiceClient(baseUrl, new CustomAuthenticationProvider(acquireAccessToken));
        }

        internal static GraphServiceClient GetAuthenticatedGraphClient(Func<Task<string>> p, object graphApiUrl)
        {
            throw new NotImplementedException();
        }
    }

    class CustomAuthenticationProvider : IAuthenticationProvider
    {
        public CustomAuthenticationProvider(Func<Task<string>> acquireTokenCallback)
        {
            acquireAccessToken = acquireTokenCallback;
        }

        private readonly Func<Task<string>> acquireAccessToken;

        public async Task AuthenticateRequestAsync(HttpRequestMessage request)
        {
            string accessToken = await acquireAccessToken.Invoke();

            // Append the access token to the request.
            request.Headers.Authorization = new AuthenticationHeaderValue(
                Constants.BearerAuthorizationScheme, accessToken);
        }
    }
}
