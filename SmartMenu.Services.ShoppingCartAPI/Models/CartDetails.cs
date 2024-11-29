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
        /// Gets or sets the cart details identifier.
        /// </summary>
        [Key]
        public int CartDetailsId { get; set; }

        /// <summary>
        /// Gets or sets the cart header identifier.
        /// </summary>
        public int CartHeaderId { get; set; }

        /// <summary>
        /// Gets or sets the cart header.
        /// </summary>
        [ForeignKey("CartHeaderId")]
        public CartHeader CartHeader { get; set; }

        /// <summary>
        /// Gets or sets the product identifier.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the product details.
        /// </summary>
        [NotMapped]
        public ProductDto Product { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the product.
        /// </summary>
        public int Count { get; set; }
    }
}
