using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcommerceTask.DTOs
{
    public class ReviewInDTO
    {
        [Required]
        public string ProductName { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        public string Comment { get; set; }
    }
}
