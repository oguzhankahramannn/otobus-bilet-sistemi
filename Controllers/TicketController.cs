using Microsoft.AspNetCore.Mvc;
using OtobusBiletiApp.Models;
using OtobusBiletiApp.Dtos;

namespace OtobusBiletiApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TicketController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet("getTicket")]
        public IActionResult GetAll()
        {
            var tickets = _context.Tickets
                .Select(t => new TicketDto
                {
                    PNR_NO = t.PNR_NO,
                    trip_id = t.trip_id,
                    p_id = t.p_id,
                    payment_id = t.payment_id
                })
                .ToList();

            return Ok(tickets);
        }


        [HttpGet("getTicketByPNR/{pnr}")]
        public IActionResult Get(int pnr)
        {
            var ticket = _context.Tickets
                .Where(t => t.PNR_NO == pnr)
                .Select(t => new TicketDto
                {
                    PNR_NO = t.PNR_NO,
                    trip_id = t.trip_id,
                    p_id = t.p_id,
                    payment_id = t.payment_id
                })
                .FirstOrDefault();

            if (ticket == null)
                return NotFound();

            return Ok(ticket);
        }


        [HttpPost("postTicket")]
        public IActionResult Add([FromBody] TicketDto dto)
        {
            var ticket = new Ticket
            {
                trip_id = dto.trip_id,
                p_id = dto.p_id,
                payment_id = dto.payment_id
            };

            _context.Tickets.Add(ticket);
            _context.SaveChanges();

            dto.PNR_NO = ticket.PNR_NO;
            return Ok(dto);
        }


        [HttpPut("putTicketByPNR/{pnr}")]
        public IActionResult Update(int pnr, [FromBody] TicketDto updated)
        {
            var ticket = _context.Tickets.FirstOrDefault(t => t.PNR_NO == pnr);
            if (ticket == null)
                return NotFound();

            ticket.trip_id = updated.trip_id;
            ticket.p_id = updated.p_id;
            ticket.payment_id = updated.payment_id;

            _context.SaveChanges();
            return Ok(updated);
        }

        [HttpDelete("deleteTicketByPNR/{pnr}")]
        public IActionResult Delete(int pnr)
        {
            var ticket = _context.Tickets.FirstOrDefault(t => t.PNR_NO == pnr);
            if (ticket == null)
                return NotFound();

            _context.Tickets.Remove(ticket);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
