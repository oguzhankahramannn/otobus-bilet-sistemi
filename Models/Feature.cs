using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OtobusBiletiApp.Models
{
    [Table("bus_feature")]
    public class BusFeature
    {
        [Column("b_plaka", TypeName = "varchar(20)")]
        public string b_plaka { get; set; }  // Primary Key + Foreign Key

        [Column("feature_name", TypeName = "varchar(50)")]
        public string feature_name { get; set; }  // Primary Key

        [ForeignKey("b_plaka")]
        public Bus Bus { get; set; }

        
    }
}
