using Microsoft.AspNetCore.Authentication;
using System.Net.Http.Headers;

namespace SmartMenu.Services.OrderAPI.Utility 
{
    /// <summary>
    /// A delegating handler that adds authentication headers to HTTP requests.
    /// </summary>
    public class BackendApiAuthenticationHttpClientHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _accessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="BackendApiAuthenticationHttpClientHandler"/> class.
        /// </summary>
        /// <param name="accessor">The HTTP context accessor.</param>
        public BackendApiAuthenticationHttpClientHandler(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        /// <summary>
        /// Sends an HTTP request with an authentication header.
        /// </summary>
        /// <param name="request">The HTTP request message.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The HTTP response message.</returns>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _accessor.HttpContext.GetTokenAsync("access_token");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
