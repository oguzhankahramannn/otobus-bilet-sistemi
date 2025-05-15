using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OtobusBiletiApp.Models
{
    [Table("payment_processing")]
    public class PaymentProcessing
    {
        [Key]
        [Column("payment_id")]
        public int payment_id { get; set; }

        [Column("cvv_no")]
        public int? cvv_no { get; set; }

        [Column("status", TypeName = "varchar(20)")]
        public string? status { get; set; }

        [Column("card_no", TypeName = "varchar(20)")]
        public string? card_no { get; set; }
    }
}

