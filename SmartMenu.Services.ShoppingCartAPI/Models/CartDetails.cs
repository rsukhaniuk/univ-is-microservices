using SmartMenu.Services.ShoppingCartAPI.Models.Dto;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMenu.Services.ShoppingCartAPI.Models
{
    /// <summary>
    /// Represents the details of a cart item.
    /// </summary>
    public class CartDetails
    {
        /// <summary>
        /// Gets or sets the unique identifier for the cart details.
        /// </summary>
        [Key]
        public int CartDetailsId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the cart header.
        /// </summary>
        public int CartHeaderId { get; set; }

        /// <summary>
        /// Gets or sets the cart header associated with these details.
        /// </summary>
        [ForeignKey("CartHeaderId")]
        public CartHeader CartHeader { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the product.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the product details associated with these cart details.
        /// </summary>
        [NotMapped]
        public ProductDto Product { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the product.
        /// </summary>
        public int Count { get; set; }
    }
}
