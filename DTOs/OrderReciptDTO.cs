using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcommerceTask.DTOs
{
    public class OrderReciptDTO
    {
        public int OID { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }
    }
}
