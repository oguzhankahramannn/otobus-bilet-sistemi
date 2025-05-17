using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OtobusBiletiApp.Models
{
    [Table("passenger")]
    public class Passenger
    {
        [Key]
        [Column("p_id")]
        public int p_id { get; set; }

        [Required]
        [Column("gender", TypeName = "varchar(10)")]
        public string gender { get; set; }

        [Required]
        [Column("tel_no", TypeName = "varchar(20)")]
        public string tel_no { get; set; }

        // Navigation property (önerilir)
        [ForeignKey("p_id")]
        public Person? Person { get; set; }
    }
}
