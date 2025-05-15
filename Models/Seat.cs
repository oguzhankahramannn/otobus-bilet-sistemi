using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OtobusBiletiApp.Models
{
    [Table("seat")]
    public class Seat
    {
        [Key]
        [Column("seat_no")]
        public int seat_no { get; set; }

        [Column("b_plaka", TypeName = "varchar(20)")]
        public string? b_plaka { get; set; }

        [ForeignKey("b_plaka")]
        public Bus? Bus { get; set; }

        [Column("is_available")]
        public bool? is_avalable { get; set; }

        [Column("pnr_no")]
        public int? PNR_NO { get; set; }

        [ForeignKey("PNR_NO")]
        public Ticket? Ticket { get; set; }

        [Column("p_id")]
        public int? p_id { get; set; }

        [ForeignKey("p_id")]
        public Person? Person { get; set; }
    }
}
