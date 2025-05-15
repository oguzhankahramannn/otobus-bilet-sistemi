using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OtobusBiletiApp.Models;
using OtobusBiletiApp.Dtos;

namespace OtobusBiletiApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BusFeatureController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BusFeatureController(AppDbContext context)
        {
            _context = context;
        }

        // Tüm otobüs özelliklerini getir (DTO ile)
        [HttpGet]
        public IActionResult GetAll()
        {
            var features = _context.BusFeatures
                .Select(f => new BusFeatureDto
                {
                    b_plaka = f.b_plaka,
                    feature_name = f.feature_name
                }).ToList();

            return Ok(features);
        }

        // Tek özellik getir (b_plaka + feature_name)
        [HttpGet("{plaka}/{feature}")]
        public IActionResult Get(string plaka, string feature)
        {
            var item = _context.BusFeatures
                .Where(f => f.b_plaka == plaka && f.feature_name == feature)
                .Select(f => new BusFeatureDto
                {
                    b_plaka = f.b_plaka,
                    feature_name = f.feature_name
                })
                .FirstOrDefault();

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        // Yeni özellik ekle (DTO ile)
        [HttpPost]
        public IActionResult Add([FromBody] BusFeatureDto dto)
        {
            var bus = _context.Buses.Find(dto.b_plaka);
            if (bus == null)
                return BadRequest("Plaka sistemde yok.");

            var feature = new BusFeature
            {
                b_plaka = dto.b_plaka,
                feature_name = dto.feature_name
            };

            _context.BusFeatures.Add(feature);
            _context.SaveChanges();
            return Ok(dto);
        }

        // Güncelle (pek yapılmaz ama örnek)
        [HttpPut("{plaka}/{feature}")]
        public IActionResult Update(string plaka, string feature, [FromBody] BusFeatureDto updated)
        {
            var item = _context.BusFeatures.FirstOrDefault(f => f.b_plaka == plaka && f.feature_name == feature);
            if (item == null)
                return NotFound();

            item.b_plaka = updated.b_plaka;
            item.feature_name = updated.feature_name;

            _context.SaveChanges();
            return Ok(updated);
        }

        // Özellik sil
        [HttpDelete("{plaka}/{feature}")]
        public IActionResult Delete(string plaka, string feature)
        {
            var item = _context.BusFeatures.FirstOrDefault(f => f.b_plaka == plaka && f.feature_name == feature);
            if (item == null)
                return NotFound();

            _context.BusFeatures.Remove(item);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
