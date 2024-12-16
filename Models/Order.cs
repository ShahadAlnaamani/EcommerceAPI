using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EcommerceTask.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OID { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserID { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        [JsonIgnore]
        public virtual ICollection<Order_Product> Order_Products { get; set; }
    }
}
