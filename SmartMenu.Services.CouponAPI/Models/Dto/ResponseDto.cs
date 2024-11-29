namespace SmartMenu.Services.CouponAPI.Models.Dto
{
    /// <summary>
    /// Data transfer object for API responses.
    /// </summary>
    public class ResponseDto
    {
        /// <summary>
        /// Gets or sets the result of the API call.
        /// </summary>
        public object? Result { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the API call was successful.
        /// </summary>
        public bool IsSuccess { get; set; } = true;

        /// <summary>
        /// Gets or sets the message associated with the API call.
        /// </summary>
        public string Message { get; set; } = "";
    }
}
