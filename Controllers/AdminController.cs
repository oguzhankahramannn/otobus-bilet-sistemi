using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OtobusBiletiApp.Models;
using OtobusBiletiApp.Dtos;

namespace OtobusBiletiApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        // Tüm adminleri DTO ile getir
        [HttpGet]
        public IActionResult GetAllAdmins()
        {
            var admins = _context.Admins
                .Select(a => new AdminDto
                {
                    p_id = a.p_id
                }).ToList();

            return Ok(admins);
        }

        // Tek admin DTO ile getir
        [HttpGet("{p_id}")]
        public IActionResult GetAdmin(int p_id)
        {
            var admin = _context.Admins
                .Where(a => a.p_id == p_id)
                .Select(a => new AdminDto
                {
                    p_id = a.p_id
                })
                .FirstOrDefault();

            if (admin == null)
                return NotFound();

            return Ok(admin);
        }

        // Yeni admin ekle
        [HttpPost]
        public IActionResult AddAdmin([FromBody] AdminDto dto)
        {
            var person = _context.Persons.Find(dto.p_id);
            if (person == null)
                return BadRequest("Bu ID ile kayıtlı Person yok.");

            var admin = new Admin
            {
                p_id = dto.p_id,
                Person = person
            };

            _context.Admins.Add(admin);
            _context.SaveChanges();
            return Ok(new { message = "Admin eklendi", p_id = dto.p_id });
        }

        // Admin sil
        [HttpDelete("{p_id}")]
        public IActionResult DeleteAdmin(int p_id)
        {
            var admin = _context.Admins.Find(p_id);
            if (admin == null)
                return NotFound();

            _context.Admins.Remove(admin);
            _context.SaveChanges();
            return NoContent();
        }
    }
} 
