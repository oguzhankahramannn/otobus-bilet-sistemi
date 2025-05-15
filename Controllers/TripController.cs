using Microsoft.AspNetCore.Mvc;
using OtobusBiletiApp.Models;
using OtobusBiletiApp.Dtos;

namespace OtobusBiletiApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TripController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TripController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var trips = _context.Trips
                .Select(t => new TripDto
                {
                    trip_id = t.trip_id,
                    startpoint = t.startpoint,
                    end_point = t.end_point,
                    start_time = t.start_time,
                    end_time = t.end_time,
                    price = t.price,
                    b_plaka = t.b_plaka,
                    p_id = t.p_id
                })
                .ToList();

            return Ok(trips);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var trip = _context.Trips
                .Where(t => t.trip_id == id)
                .Select(t => new TripDto
                {
                    trip_id = t.trip_id,
                    startpoint = t.startpoint,
                    end_point = t.end_point,
                    start_time = t.start_time,
                    end_time = t.end_time,
                    price = t.price,
                    b_plaka = t.b_plaka,
                    p_id = t.p_id
                })
                .FirstOrDefault();

            if (trip == null)
                return NotFound();

            return Ok(trip);
        }

        [HttpPost]
        public IActionResult Add([FromBody] TripDto dto)
        {
            var trip = new Trip
            {
                startpoint = dto.startpoint,
                end_point = dto.end_point,
                start_time = dto.start_time,
                end_time = dto.end_time,
                price = dto.price,
                b_plaka = dto.b_plaka,
                p_id = dto.p_id
            };

            _context.Trips.Add(trip);
            _context.SaveChanges();

            dto.trip_id = trip.trip_id;
            return Ok(dto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] TripDto updated)
        {
            var trip = _context.Trips.FirstOrDefault(t => t.trip_id == id);
            if (trip == null)
                return NotFound();

            trip.startpoint = updated.startpoint;
            trip.end_point = updated.end_point;
            trip.start_time = updated.start_time;
            trip.end_time = updated.end_time;
            trip.price = updated.price;
            trip.b_plaka = updated.b_plaka;
            trip.p_id = updated.p_id;

            _context.SaveChanges();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var trip = _context.Trips.FirstOrDefault(t => t.trip_id == id);
            if (trip == null)
                return NotFound();

            _context.Trips.Remove(trip);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
