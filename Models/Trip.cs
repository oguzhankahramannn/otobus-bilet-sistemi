using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OtobusBiletiApp.Models
{
    [Table("trip")]
    public class Trip
    {
        [Key]
        [Column("trip_id")]
        public int trip_id { get; set; }

        [Column("startpoint", TypeName = "varchar(100)")]
        public string? startpoint { get; set; }

        [Column("end_point", TypeName = "varchar(100)")]
        public string? end_point { get; set; }

        [Column("start_time")]
        public DateTime? start_time { get; set; }

        [Column("end_time")]
        public DateTime? end_time { get; set; }

        [Column("price", TypeName = "decimal(8,2)")]
        public decimal? price { get; set; }

        [Column("b_plaka", TypeName = "varchar(20)")]
        public string? b_plaka { get; set; }

        [ForeignKey("b_plaka")]
        public Bus? Bus { get; set; }

        [Column("p_id")]
        public int? p_id { get; set; }

        [ForeignKey("p_id")]
        public Person? Person { get; set; }
    }
}
