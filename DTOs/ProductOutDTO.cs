using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EcommerceTask.DTOs
{
    public class ProductOutDTO
    {
        public int PID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        [ForeignKey("Category")]
        public int CatId { get; set; }

        public decimal Price { get; set; }

        public decimal Rating { get; set; }

    }
}
