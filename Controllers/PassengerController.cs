using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OtobusBiletiApp.Models;
using OtobusBiletiApp.Dtos;

namespace OtobusBiletiApp.Controllers
{
    [ApiController]
    [Route("api/passengers")]
    public class PassengerController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PassengerController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/passengers
        [HttpGet]
        public IActionResult GetAll()
        {
            var passengers = _context.Passengers
                .Select(p => new PassengerDto
                {
                    p_id = p.p_id,
                    gender = p.gender,
                    tel_no = p.tel_no
                }).ToList();

            return Ok(passengers);
        }

        // GET: api/passengers/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var passenger = _context.Passengers
                .Where(p => p.p_id == id)
                .Select(p => new PassengerDto
                {
                    p_id = p.p_id,
                    gender = p.gender,
                    tel_no = p.tel_no
                })
                .FirstOrDefault();

            if (passenger == null)
                return NotFound();

            return Ok(passenger);
        }

        // POST: api/passengers
        [HttpPost]
        public IActionResult Add([FromBody] PassengerDto dto)
        {
            var passenger = new Passenger
            {
                p_id = dto.p_id,
                gender = dto.gender,
                tel_no = dto.tel_no
            };

            _context.Passengers.Add(passenger);
            _context.SaveChanges();
            return Ok(dto);
        }

        // PUT: api/passengers/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] PassengerDto dto)
        {
            var passenger = _context.Passengers.FirstOrDefault(p => p.p_id == id);
            if (passenger == null)
                return NotFound(new { message = $"ID {id} ile yolcu bulunamadı." });

            passenger.gender = dto.gender;
            passenger.tel_no = dto.tel_no;

            _context.SaveChanges();

            return Ok(new
            {
                message = "Yolcu başarıyla güncellendi.",
                updated = dto
            });
        }

        // DELETE: api/passengers/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var passenger = _context.Passengers.FirstOrDefault(p => p.p_id == id);
            if (passenger == null)
                return NotFound();

            _context.Passengers.Remove(passenger);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
