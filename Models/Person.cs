using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OtobusBiletiApp.Models
{
    [Table("Person")]
    public class Person
    {
        [Key]
        [Column("p_id")]
        public int p_id { get; set; }

        [Column("name", TypeName = "varchar(50)")]
        [Required]
        public string name { get; set; }

        [Column("surname", TypeName = "varchar(50)")]
        [Required]
        public string surname { get; set; }

        [Column("password", TypeName = "varchar(100)")]
        [Required]
        public string password { get; set; }

        [Column("email", TypeName = "varchar(100)")]
        [Required]
        public string email { get; set; }
    }
}
