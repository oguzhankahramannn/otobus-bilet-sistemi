using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OtobusBiletiApp.Models
{
    [Table("bus")]
    public class Bus
    {
        [Key]
        [Column("b_plaka", TypeName = "varchar(20)")]
        public string b_plaka { get; set; }

        [Column("model", TypeName = "varchar(50)")]
        public string? model { get; set; }

        [Column("seat_capacity")]
        public int? seat_capacity { get; set; }

        [Column("company_id")]
        public int? company_id { get; set; }

        [ForeignKey("company_id")]
        public BusCompany? BusCompany { get; set; }
    }
}
