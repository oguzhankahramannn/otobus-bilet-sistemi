using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OtobusBiletiApp.Models
{
    [Table("admin")]
    public class Admin
    {
        [Key]
        [Column("p_id")]
        public int p_id { get; set; }

        [Required]
        public Person Person { get; set; }
    }
}
