using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OtobusBiletiApp.Models
{
    [Table("company_tel")]
    public class CompanyTel
    {
        [Key]
        [Column("id")]
        public int id { get; set; }

        [Required]
        [Column("company_id")]
        public int company_id { get; set; }

        [Required]
        [Column("tel_no", TypeName = "varchar(20)")]
        public string tel_no { get; set; }

        // Navigation property (opsiyonel ama önerilir)
        [ForeignKey("company_id")]
        public BusCompany? BusCompany { get; set; }
    }
}
