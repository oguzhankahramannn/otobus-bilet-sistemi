using Microsoft.AspNetCore.Mvc;
using OtobusBiletiApp.Models;
using OtobusBiletiApp.Dtos;
using System.Linq;

namespace OtobusBiletiApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CompanyController(AppDbContext context)
        {
            _context = context;
        }

        // Tüm firmaları getir (DTO)
        [HttpGet("getAllFirma")]
        public IActionResult GetAllCompanies()
        {
            var companies = _context.Companies
                .Select(c => new BusCompanyDto
                {
                    company_id = c.company_id,
                    c_name = c.c_name,
                    c_telno = c.c_telno
                })
                .ToList();

            return Ok(companies);
        }

        // ID'ye göre firma getir (DTO)
        [HttpGet("getById/{id}")]
        public IActionResult GetCompany(int id)
        {
            var company = _context.Companies
                .Where(c => c.company_id == id)
                .Select(c => new BusCompanyDto
                {
                    company_id = c.company_id,
                    c_name = c.c_name,
                    c_telno = c.c_telno
                })
                .FirstOrDefault();

            if (company == null)
                return NotFound();

            return Ok(company);
        }

        // Yeni firma ekle (DTO)
        [HttpPost("postFirma")]
        public IActionResult AddCompany([FromBody] BusCompanyDto dto)
        {
            var company = new BusCompany
            {
                c_name = dto.c_name,
                c_telno = dto.c_telno
            };

            _context.Companies.Add(company);
            _context.SaveChanges();

            dto.company_id = company.company_id;
            return Ok(dto);
        }

        // Firma güncelle (DTO)
        [HttpPut("updateFirmabyId/{id}")]
        public IActionResult UpdateCompany(int id, [FromBody] BusCompanyDto updated)
        {
            var company = _context.Companies.FirstOrDefault(c => c.company_id == id);
            if (company == null)
                return NotFound();

            company.c_name = updated.c_name;
            company.c_telno = updated.c_telno;

            _context.SaveChanges();
            return Ok(updated);
        }

        // Firma sil
        [HttpDelete("deleteFirmaById/{id}")]
        public IActionResult DeleteCompany(int id)
        {
            var company = _context.Companies.FirstOrDefault(c => c.company_id == id);
            if (company == null)
                return NotFound();

            _context.Companies.Remove(company);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
