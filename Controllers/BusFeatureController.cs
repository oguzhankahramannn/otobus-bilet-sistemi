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

        // Belirli bir plakaya ait otobüs özelliklerini getir
        [HttpGet("getByPlaka/{plaka}")]
        public IActionResult GetByPlaka(string plaka)
        {
            var features = _context.BusFeatures
                .Where(f => f.b_plaka == plaka)
                .Select(f => new BusFeatureDto
                {
                    b_plaka = f.b_plaka,
                    feature_name = f.feature_name
                })
                .ToList();

            if (features == null || features.Count == 0)
            {
                return NotFound($"'{plaka}' plakalı otobüse ait özellik bulunamadı.");
            }

            return Ok(features);
        }


        // Tek özellik getir (b_plaka + feature_name)


        
        
        [HttpPost("postBusFeature")]
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

        // Güncelle 
        [HttpPut("putBusFeature/{plaka}/{feature}")]
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
        
        [HttpDelete("deleteBusFeature/{plaka}/{feature}")]
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
