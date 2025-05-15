using Microsoft.AspNetCore.Mvc;
using OtobusBiletiApp.Models;
using OtobusBiletiApp.Dtos;

namespace OtobusBiletiApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicketSeatController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TicketSeatController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _context.TicketSeats
                .Select(ts => new TicketSeatDto
                {
                    id = ts.id,
                    PNR_NO = ts.PNR_NO,
                    seat_no = ts.seat_no,
                    b_plaka = ts.b_plaka
                })
                .ToList();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var item = _context.TicketSeats
                .Where(ts => ts.id == id)
                .Select(ts => new TicketSeatDto
                {
                    id = ts.id,
                    PNR_NO = ts.PNR_NO,
                    seat_no = ts.seat_no,
                    b_plaka = ts.b_plaka
                })
                .FirstOrDefault();

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public IActionResult Add([FromBody] TicketSeatDto dto)
        {
            var model = new TicketSeat
            {
                PNR_NO = dto.PNR_NO,
                seat_no = dto.seat_no,
                b_plaka = dto.b_plaka
            };

            _context.TicketSeats.Add(model);
            _context.SaveChanges();

            dto.id = model.id;
            return Ok(dto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] TicketSeatDto dto)
        {
            var model = _context.TicketSeats.FirstOrDefault(ts => ts.id == id);
            if (model == null)
                return NotFound();

            model.PNR_NO = dto.PNR_NO;
            model.seat_no = dto.seat_no;
            model.b_plaka = dto.b_plaka;

            _context.SaveChanges();
            return Ok(dto);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var model = _context.TicketSeats.FirstOrDefault(ts => ts.id == id);
            if (model == null)
                return NotFound();

            _context.TicketSeats.Remove(model);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
