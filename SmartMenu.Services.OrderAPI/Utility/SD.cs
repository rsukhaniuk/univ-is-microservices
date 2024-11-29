namespace SmartMenu.Services.OrderAPI.Utility
{
    /// <summary>
    /// Static class containing constants for order statuses and user roles.
    /// </summary>
    public static class SD
    {
        /// <summary>
        /// Status indicating the order is pending.
        /// </summary>
        public const string Status_Pending = "Pending";

        /// <summary>
        /// Status indicating the order is approved.
        /// </summary>
        public const string Status_Approved = "Approved";

        /// <summary>
        /// Status indicating the order is ready for pickup.
        /// </summary>
        public const string Status_ReadyForPickup = "ReadyForPickup";

        /// <summary>
        /// Status indicating the order is completed.
        /// </summary>
        public const string Status_Completed = "Completed";

        /// <summary>
        /// Status indicating the order is refunded.
        /// </summary>
        public const string Status_Refunded = "Refunded";

        /// <summary>
        /// Status indicating the order is cancelled.
        /// </summary>
        public const string Status_Cancelled = "Cancelled";

        /// <summary>
        /// Role indicating the user is an admin.
        /// </summary>
        public const string RoleAdmin = "ADMIN";

        /// <summary>
        /// Role indicating the user is a customer.
        /// </summary>
        public const string RoleCustomer = "CUSTOMER";
    }
}
