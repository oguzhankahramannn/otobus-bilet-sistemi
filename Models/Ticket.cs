using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OtobusBiletiApp.Models
{
    [Table("ticket")]
    public class Ticket
    {
        [Key]
        [Column("pnr_no")]
        public int PNR_NO { get; set; }

        [Column("trip_id")]
        public int? trip_id { get; set; }

        [ForeignKey("trip_id")]
        public Trip? Trip { get; set; }

        [Column("p_id")]
        public int? p_id { get; set; }

        [ForeignKey("p_id")]
        public Person? Person { get; set; }

        [Column("payment_id")]
        public int? payment_id { get; set; }

        [ForeignKey("payment_id")]
        public PaymentProcessing? PaymentProcessing { get; set; }
    }
}

