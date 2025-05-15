using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OtobusBiletiApp.Models;
using OtobusBiletiApp.Dtos;

namespace OtobusBiletiApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PersonController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet("gizli")]
        public IActionResult GizliBilgiler()
        {
            return Ok("Bu JWT ile erişilebilen gizli bir endpointtir.");
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var persons = _context.Persons
                .Select(p => new PersonDto
                {
                    p_id = p.p_id,
                    name = p.name,
                    surname = p.surname,
                    email = p.email
                }).ToList();

            return Ok(persons);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var person = _context.Persons
                .Where(p => p.p_id == id)
                .Select(p => new PersonDto
                {
                    p_id = p.p_id,
                    name = p.name,
                    surname = p.surname,
                    email = p.email
                })
                .FirstOrDefault();

            if (person == null)
                return NotFound();

            return Ok(person);
        }

        [HttpPost]
        public IActionResult Add([FromBody] PersonDto dto)
        {
            var person = new Person
            {
                name = dto.name,
                surname = dto.surname,
                email = dto.email,
                password = "default123"
            };

            _context.Persons.Add(person);
            _context.SaveChanges();

            dto.p_id = person.p_id;
            return Ok(dto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] PersonDto updated)
        {
            var person = _context.Persons.FirstOrDefault(p => p.p_id == id);
            if (person == null)
                return NotFound();

            person.name = updated.name;
            person.surname = updated.surname;
            person.email = updated.email;

            _context.SaveChanges();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var person = _context.Persons.FirstOrDefault(p => p.p_id == id);
            if (person == null)
                return NotFound();

            _context.Persons.Remove(person);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
