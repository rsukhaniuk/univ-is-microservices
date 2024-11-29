using SmartMenu.Services.OrderAPI.Models.Dto;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMenu.Services.OrderAPI.Models
{
    /// <summary>
    /// Represents the details of an order.
    /// </summary>
    public class OrderDetails
    {
        /// <summary>
        /// Gets or sets the order details ID.
        /// </summary>
        [Key]
        public int OrderDetailsId { get; set; }

        /// <summary>
        /// Gets or sets the order header ID.
        /// </summary>
        public int OrderHeaderId { get; set; }

        /// <summary>
        /// Gets or sets the order header.
        /// </summary>
        [ForeignKey("OrderHeaderId")]
        public OrderHeader? OrderHeader { get; set; }

        /// <summary>
        /// Gets or sets the product ID.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the product.
        /// </summary>
        [NotMapped]
        public ProductDto? Product { get; set; }

        /// <summary>
        /// Gets or sets the count of products.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Gets or sets the price of the product.
        /// </summary>
        public double Price { get; set; }
    }
}
