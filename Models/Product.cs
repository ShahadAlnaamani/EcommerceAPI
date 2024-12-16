using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EcommerceTask.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PID { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        [ForeignKey("Category")]
        public int CatId { get; set; }

        [Required]
        [Range(minimum: 0.1, maximum: int.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [Range(minimum: 0, maximum: int.MaxValue)]
        public int Stock {  get; set; }

        [Range (1, 5)]
        [DefaultValue(5)]
        public decimal Rating { get; set; }

        public bool ProductActive { get; set; }

        public DateTime Modifiedt { get; set; }
        public int ModifiedBy { get; set; }

        [JsonIgnore]
        public virtual ICollection<Review> Reviews { get; set; }

        [JsonIgnore]
        public virtual ICollection<Order_Product> Order_Products { get; set; }

    }
}
