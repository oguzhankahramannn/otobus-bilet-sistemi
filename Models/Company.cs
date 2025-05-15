using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OtobusBiletiApp.Models
{
    [Table("bus_company")]
    public class BusCompany
    {
        [Key]
        [Column("company_id")]
        public int company_id { get; set; }

        [Column("c_name", TypeName = "varchar(100)")]
        [Required]
        public string c_name { get; set; }

        [Column("c_telno", TypeName = "varchar(20)")]
        [Required]
        public string c_telno { get; set; }
    }
}
