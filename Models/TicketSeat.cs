using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OtobusBiletiApp.Models
{
    [Table("ticket_seat")]
    public class TicketSeat
    {
        [Key]
        [Column("id")]
        public int id { get; set; }

        [Required]
        [Column("pnr_no")]
        public int PNR_NO { get; set; }  //  TİP düzeltildi: string → int

        [Required]
        [Column("seat_no")]
        public int seat_no { get; set; }

        [Required]
        [Column("b_plaka", TypeName = "varchar(255)")]
        public string b_plaka { get; set; }

        [ForeignKey("PNR_NO")]
        public Ticket? Ticket { get; set; }

        [ForeignKey("seat_no")]
        public Seat? Seat { get; set; }

        [ForeignKey("b_plaka")]
        public Bus? Bus { get; set; }
    }
}
