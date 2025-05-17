using Microsoft.AspNetCore.Mvc;
using OtobusBiletiApp.Models;
using OtobusBiletiApp.Dtos;

namespace OtobusBiletiApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Route("[controller]")]
    public class BusController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BusController(AppDbContext context)
        {
            _context = context;
        }

        // Tüm otobüsleri getir
        [HttpGet("getAllBus")]
        public IActionResult GetAll()
        {
            var buses = _context.Buses
                .Select(b => new BusDto
                {
                    b_plaka = b.b_plaka,
                    model = b.model,
                    seat_capacity = b.seat_capacity,
                    company_id = b.company_id
                })
                .ToList();

            return Ok(buses);
        }

        // Tek otobüs getir
        [HttpGet("getBusByPlaka/{plaka}")]
        public IActionResult Get(string plaka)
        {
            var bus = _context.Buses
                .Where(b => b.b_plaka == plaka)
                .Select(b => new BusDto
                {
                    b_plaka = b.b_plaka,
                    model = b.model,
                    seat_capacity = b.seat_capacity,
                    company_id = b.company_id
                })
                .FirstOrDefault();

            if (bus == null)
                return NotFound();

            return Ok(bus);
        }
        

        // Yeni otobüs ekle
        [HttpPost("addBus")]
        public IActionResult Add([FromBody] BusDto dto)
        {
            var bus = new Bus
            {
                b_plaka = dto.b_plaka,
                model = dto.model,
                seat_capacity = dto.seat_capacity,
                company_id = dto.company_id
            };

            _context.Buses.Add(bus);
            _context.SaveChanges();
            return Ok(dto);
        }

        // Otobüs güncelle
        [HttpPut("updateByPlaka")]
        public IActionResult UpdateBusByPlaka([FromQuery] string plaka, [FromBody] Bus updatedBus)
        {
            var bus = _context.Buses.FirstOrDefault(b => b.b_plaka == plaka);
            if (bus == null)
                return NotFound();

            // Alanları güncelle
            bus.model = updatedBus.model;
            //bus.seat_count = updatedBus.seat_count;
            bus.company_id = updatedBus.company_id;

            _context.SaveChanges();
            return Ok(bus);
        }


        // Otobüs sil
        [HttpDelete("deleteByPlaka")]
        public IActionResult Delete(string plaka)
        {
            var bus = _context.Buses.FirstOrDefault(b => b.b_plaka == plaka);
            if (bus == null)
                return NotFound();

            _context.Buses.Remove(bus);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
