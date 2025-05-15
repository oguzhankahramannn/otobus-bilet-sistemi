using Microsoft.AspNetCore.Mvc;
using OtobusBiletiApp.Models;
using OtobusBiletiApp.Dtos;

namespace OtobusBiletiApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompanyTelController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CompanyTelController(AppDbContext context)
        {
            _context = context;
        }

        // Tüm company_tel kayıtlarını getir (DTO ile)
        [HttpGet]
        public IActionResult GetAll()
        {
            var tels = _context.CompanyTels
                .Select(t => new CompanyTelDto
                {
                    id = t.id,
                    company_id = t.company_id,
                    tel_no = t.tel_no
                }).ToList();

            return Ok(tels);
        }

        // Tek kayıt getir (DTO ile)
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var tel = _context.CompanyTels
                .Where(t => t.id == id)
                .Select(t => new CompanyTelDto
                {
                    id = t.id,
                    company_id = t.company_id,
                    tel_no = t.tel_no
                })
                .FirstOrDefault();

            if (tel == null)
                return NotFound();

            return Ok(tel);
        }

        // Yeni telefon ekle (DTO ile)
        [HttpPost]
        public IActionResult Add([FromBody] CompanyTelDto dto)
        {
            var company = _context.Companies.FirstOrDefault(c => c.company_id == dto.company_id);
            if (company == null)
                return BadRequest("Geçerli bir company_id bulunamadı.");

            var tel = new CompanyTel
            {
                company_id = dto.company_id,
                tel_no = dto.tel_no
            };

            _context.CompanyTels.Add(tel);
            _context.SaveChanges();

            dto.id = tel.id;
            return Ok(dto);
        }

        // Güncelle (DTO ile)
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] CompanyTelDto updated)
        {
            var tel = _context.CompanyTels.FirstOrDefault(t => t.id == id);
            if (tel == null)
                return NotFound();

            tel.company_id = updated.company_id;
            tel.tel_no = updated.tel_no;

            _context.SaveChanges();
            return Ok(updated);
        }

        // Sil
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var tel = _context.CompanyTels.FirstOrDefault(t => t.id == id);
            if (tel == null)
                return NotFound();

            _context.CompanyTels.Remove(tel);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
