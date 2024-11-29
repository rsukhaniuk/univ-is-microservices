namespace SmartMenu.Services.AuthAPI.Models.Dto
{
    /// <summary>
    /// Represents a standard response data transfer object.
    /// </summary>
    public class ResponseDto
    {
        /// <summary>
        /// Gets or sets the result of the response.
        /// </summary>
        public object? Result { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the response indicates success.
        /// </summary>
        public bool IsSuccess { get; set; } = true;

        /// <summary>
        /// Gets or sets the message associated with the response.
        /// </summary>
        public string Message { get; set; } = "";
    }
}
