namespace SmartMenu.Web.Models
{
    /// <summary>
    /// Data transfer object for API responses.
    /// </summary>
    public class ResponseDto
    {
        /// <summary>
        /// Gets or sets the result of the API response.
        /// </summary>
        public object? Result { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the API request was successful.
        /// </summary>
        public bool IsSuccess { get; set; } = true;

        /// <summary>
        /// Gets or sets the message associated with the API response.
        /// </summary>
        public string Message { get; set; } = "";
    }
}
