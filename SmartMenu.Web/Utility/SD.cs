namespace SmartMenu.Web.Utility
{
    /// <summary>
    /// Provides static details and constants used across the application.
    /// </summary>
    public class SD
    {
        /// <summary>
        /// Gets or sets the base URL for the Coupon API.
        /// </summary>
        public static string CouponAPIBase { get; set; }

        /// <summary>
        /// Gets or sets the base URL for the Product API.
        /// </summary>
        public static string ProductAPIBase { get; set; }

        /// <summary>
        /// Gets or sets the base URL for the Auth API.
        /// </summary>
        public static string AuthAPIBase { get; set; }

        /// <summary>
        /// Gets or sets the base URL for the Shopping Cart API.
        /// </summary>
        public static string ShoppingCartAPIBase { get; set; }

        /// <summary>
        /// Gets or sets the base URL for the Order API.
        /// </summary>
        public static string OrderAPIBase { get; set; }

        /// <summary>
        /// Represents the admin role.
        /// </summary>
        public const string RoleAdmin = "ADMIN";

        /// <summary>
        /// Represents the customer role.
        /// </summary>
        public const string RoleCustomer = "CUSTOMER";

        /// <summary>
        /// Represents the token cookie name.
        /// </summary>
        public const string TokenCookie = "JWTToken";

        /// <summary>
        /// Defines the types of API requests.
        /// </summary>
        public enum ApiType
        {
            /// <summary>
            /// Represents a GET request.
            /// </summary>
            GET,

            /// <summary>
            /// Represents a POST request.
            /// </summary>
            POST,

            /// <summary>
            /// Represents a PUT request.
            /// </summary>
            PUT,

            /// <summary>
            /// Represents a DELETE request.
            /// </summary>
            DELETE
        }

        /// <summary>
        /// Represents the pending status.
        /// </summary>
        public const string Status_Pending = "Pending";

        /// <summary>
        /// Represents the approved status.
        /// </summary>
        public const string Status_Approved = "Approved";

        /// <summary>
        /// Represents the ready for pickup status.
        /// </summary>
        public const string Status_ReadyForPickup = "ReadyForPickup";

        /// <summary>
        /// Represents the completed status.
        /// </summary>
        public const string Status_Completed = "Completed";

        /// <summary>
        /// Represents the refunded status.
        /// </summary>
        public const string Status_Refunded = "Refunded";

        /// <summary>
        /// Represents the cancelled status.
        /// </summary>
        public const string Status_Cancelled = "Cancelled";

        /// <summary>
        /// Defines the types of content.
        /// </summary>
        public enum ContentType
        {
            /// <summary>
            /// Represents JSON content type.
            /// </summary>
            Json,

            /// <summary>
            /// Represents multipart/form-data content type.
            /// </summary>
            MultipartFormData,
        }
    }
}
