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



        [HttpGet("getallPerson")]
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


        [HttpGet("personWithId/{id}")]
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


        [HttpPost("postPerson")]
        public IActionResult Add([FromBody] PersonDto dto)
        {
            var person = new Person
            {
                p_id = dto.p_id,
                name = dto.name,
                surname = dto.surname,
                email = dto.email,
                password = dto.password
            };

            _context.Persons.Add(person);
            _context.SaveChanges();
            return Ok(dto);
        }

        [HttpPut("personPutWithId/{id}")]
        public IActionResult Update(int id, [FromBody] PersonDto updated)
        {
            var person = _context.Persons.FirstOrDefault(p => p.p_id == id);
            if (person == null)
                return NotFound(new { message = $"ID {id} ile kayıt bulunamadı." });

            // Güncelleme
            person.name = updated.name;
            person.surname = updated.surname;
            person.email = updated.email;

            _context.SaveChanges();

            return Ok(new
            {
                message = "Kayıt başarıyla güncellendi.",
                updated = new
                {
                    id = id,
                    updated.name,
                    updated.surname,
                    updated.email
                }
            });
        }


        [HttpDelete("personDeleteWith/{id}")]
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
