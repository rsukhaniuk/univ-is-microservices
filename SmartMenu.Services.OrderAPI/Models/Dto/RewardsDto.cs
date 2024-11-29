namespace SmartMenu.Services.OrderAPI.Models.Dto
{
    /// <summary>
    /// Represents rewards information for a user.
    /// </summary>
    public class RewardsDto
    {
        /// <summary>
        /// Gets or sets the user ID.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the rewards activity.
        /// </summary>
        public int RewardsActivity { get; set; }

        /// <summary>
        /// Gets or sets the order ID.
        /// </summary>
        public int OrderId { get; set; }
    }
}
