using static SmartMenu.Web.Utility.SD;

namespace SmartMenu.Web.Models
{
    /// <summary>
    /// Data transfer object for API requests.
    /// </summary>
    public class RequestDto
    {
        /// <summary>
        /// Gets or sets the type of the API request.
        /// </summary>
        public ApiType ApiType { get; set; } = ApiType.GET;

        /// <summary>
        /// Gets or sets the URL for the API request.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the data to be sent with the API request.
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// Gets or sets the access token for authentication.
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// Gets or sets the content type of the API request.
        /// </summary>
        public ContentType ContentType { get; set; } = ContentType.Json;
    }
}
