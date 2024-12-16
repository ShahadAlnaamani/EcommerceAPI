using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace EcommerceTask.Models
{
    [PrimaryKey(nameof(OrderID), nameof(ProductID))]
    public class Order_Product
    {
        [Required]
        [ForeignKey("Order")]
        public int OrderID { get; set; }

        [Required]
        [ForeignKey("Product")]
        public int ProductID { get; set; }

        public int Quantity { get; set; }
    }
}
