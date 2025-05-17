// DTO klasörü altında olması gereken tüm DTO'lar

namespace OtobusBiletiApp.Dtos
{
    public class PersonDto
    {
        public int p_id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }

        public string password { get; set; }
    }

    public class AdminDto
    {
        public int p_id { get; set; }
    }

    public class BusDto
    {
        public string b_plaka { get; set; }
        public string? model { get; set; }
        public int? seat_capacity { get; set; }
        public int? company_id { get; set; }
    }

    public class BusCompanyDto
    {
        public int company_id { get; set; }
        public string c_name { get; set; }
        public string c_telno { get; set; }
    }

    public class CompanyTelDto
    {
        public int id { get; set; }
        public int company_id { get; set; }
        public string tel_no { get; set; }
    }

    public class BusFeatureDto
    {
        public string b_plaka { get; set; }
        public string feature_name { get; set; }
    }

    public class PaymentDto
    {
        public int payment_id { get; set; }
        public string? status { get; set; } // cvv ve kart_no gösterilmez
    }

    public class SeatDto
    {
        public int seat_no { get; set; }
        public string? b_plaka { get; set; }
        public bool? is_avalable { get; set; }
        public int? PNR_NO { get; set; }  // EKLENDİ
        public int? p_id { get; set; }    // EKLENDİ
    }

    public class TicketDto
    {
        public int PNR_NO { get; set; }
        public int? trip_id { get; set; }
        public int? p_id { get; set; }
        public int? payment_id { get; set; }
    }

    public class TicketSeatDto
    {
        public int id { get; set; }
        public int PNR_NO { get; set; }
        public int seat_no { get; set; }
        public string b_plaka { get; set; }
    }

    public class TripDto
    {
        public int trip_id { get; set; }
        public string? startpoint { get; set; }
        public string? end_point { get; set; }
        public DateTime? start_time { get; set; }
        public DateTime? end_time { get; set; }
        public decimal? price { get; set; }
        public string? b_plaka { get; set; }
        public int? p_id { get; set; }
    }
}
