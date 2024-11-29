using SmartMenu.Web.Service.IService;
using SmartMenu.Web.Utility;
using Newtonsoft.Json.Linq;

namespace SmartMenu.Web.Service
{
    /// <summary>
    /// Provides methods to manage tokens in the HTTP context.
    /// </summary>
    public class TokenProvider : ITokenProvider
    {
        private readonly IHttpContextAccessor _contextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenProvider"/> class.
        /// </summary>
        /// <param name="contextAccessor">The HTTP context accessor.</param>
        public TokenProvider(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        /// <summary>
        /// Clears the token from the cookies.
        /// </summary>
        public void ClearToken()
        {
            _contextAccessor.HttpContext?.Response.Cookies.Delete(SD.TokenCookie);
        }

        /// <summary>
        /// Gets the token from the cookies.
        /// </summary>
        /// <returns>The token if it exists; otherwise, null.</returns>
        public string? GetToken()
        {
            string? token = null;
            bool? hasToken = _contextAccessor.HttpContext?.Request.Cookies.TryGetValue(SD.TokenCookie, out token);
            return hasToken is true ? token : null;
        }

        /// <summary>
        /// Sets the token in the cookies.
        /// </summary>
        /// <param name="token">The token to set.</param>
        public void SetToken(string token)
        {
            _contextAccessor.HttpContext?.Response.Cookies.Append(SD.TokenCookie, token);
        }
    }
}
