using LinqToDB.Mapping;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EcommerceTask.Models
{
    public enum Role { NormalUser, Admin } 
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Identity]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d@$!%*?&]{8,}$")]
        public string Password { get; set; }

        [Required]
       // [RegularExpression(@"^\\+?[1-9][0-9]{7,14}$")]
        public string PhoneNumber { get; set; }

        public Role Role { get; set; }
        public bool AccountActive {  get; set; }

        public DateTime Created { get; set; }

        public DateTime LastUpdatedAt { get; set;}

        public int ModifiedBy { get; set;}

        [JsonIgnore]
        public virtual ICollection<Order> Orders { get; set; }

        [JsonIgnore]
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
