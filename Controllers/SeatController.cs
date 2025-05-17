using Microsoft.AspNetCore.Mvc;
using OtobusBiletiApp.Models;
using OtobusBiletiApp.Dtos;

namespace OtobusBiletiApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SeatController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SeatController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("getSeat")]
        public IActionResult GetAll()
        {
            var seats = _context.Seats
                .Select(s => new SeatDto
                {
                    seat_no = s.seat_no,
                    b_plaka = s.b_plaka,
                    is_avalable = s.is_avalable,
                    PNR_NO = s.PNR_NO,
                    p_id = s.p_id
                })
                .ToList();

            return Ok(seats);
        }

      
        [HttpGet("getSeatbySeatNo/{seat_no}")]
        public IActionResult Get(int seat_no)
        {
            var seat = _context.Seats
                .Where(s => s.seat_no == seat_no)
                .Select(s => new SeatDto
                {
                    seat_no = s.seat_no,
                    b_plaka = s.b_plaka,
                    is_avalable = s.is_avalable,
                    PNR_NO = s.PNR_NO,
                    p_id = s.p_id
                })
                .FirstOrDefault();

            if (seat == null)
                return NotFound();

            return Ok(seat);
        }

      
        [HttpPost("postSeat")]
        public IActionResult Add([FromBody] SeatDto dto)
        {
            var seat = new Seat
            {
                seat_no = dto.seat_no,
                b_plaka = dto.b_plaka,
                is_avalable = dto.is_avalable,
                PNR_NO = dto.PNR_NO,
                p_id = dto.p_id
            };

            _context.Seats.Add(seat);
            _context.SaveChanges();

            return Ok(dto);
        }

      
        [HttpPut("putSeatbySeatNo/{seat_no}")]
        public IActionResult Update(int seat_no, [FromBody] SeatDto updated)
        {
            var seat = _context.Seats.FirstOrDefault(s => s.seat_no == seat_no);
            if (seat == null)
                return NotFound();

            seat.b_plaka = updated.b_plaka;
            seat.is_avalable = updated.is_avalable;
            seat.PNR_NO = updated.PNR_NO;
            seat.p_id = updated.p_id;

            _context.SaveChanges();
            return Ok(updated);
        }

        
        [HttpDelete("deleteSeatbySeatno/{seat_no}")]
        public IActionResult Delete(int seat_no)
        {
            var seat = _context.Seats.FirstOrDefault(s => s.seat_no == seat_no);
            if (seat == null)
                return NotFound();

            _context.Seats.Remove(seat);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
