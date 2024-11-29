namespace SmartMenu.Services.OrderAPI.Models.Dto
{
    /// <summary>
    /// Represents a response with a result, success status, and message.
    /// </summary>
    public class ResponseDto
    {
        /// <summary>
        /// Gets or sets the result of the response.
        /// </summary>
        public object? Result { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the response is successful.
        /// </summary>
        public bool IsSuccess { get; set; } = true;

        /// <summary>
        /// Gets or sets the message of the response.
        /// </summary>
        public string Message { get; set; } = "";
    }
}
