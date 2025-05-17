using Microsoft.AspNetCore.Mvc;
using OtobusBiletiApp.Models;
using OtobusBiletiApp.Dtos;

namespace OtobusBiletiApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PaymentController(AppDbContext context)
        {
            _context = context;
        }

        // Tüm ödemeleri getir (DTO)
        
        [HttpGet("getPayment")]
        public IActionResult GetAll()
        {
            var payments = _context.Payments
                .Select(p => new PaymentDto
                {
                    payment_id = p.payment_id,
                    status = p.status
                }).ToList();

            return Ok(payments);
        }

        
        
        [HttpGet("getPaymentById/{id}")]
        public IActionResult Get(int id)
        {
            var payment = _context.Payments
                .Where(p => p.payment_id == id)
                .Select(p => new PaymentDto
                {
                    payment_id = p.payment_id,
                    status = p.status
                }).FirstOrDefault();

            if (payment == null)
                return NotFound();

            return Ok(payment);
        }

        // Yeni ödeme ekle (DTO ile)
        
        [HttpPost("postPayment")]
        public IActionResult Add([FromBody] PaymentDto dto)
        {
            var payment = new PaymentProcessing
            {
                status = dto.status,
                cvv_no = 123, // örnek sabit (DTO'da yok), gerçek projede ayrı dto yapılabilir
                card_no = "**** **** **** 0000"
            };

            _context.Payments.Add(payment);
            _context.SaveChanges();

            dto.payment_id = payment.payment_id;
            return Ok(dto);
        }

        // Ödeme güncelle (DTO ile)
        
        [HttpPut("putPaymentById/{id}")]

        public IActionResult Update(int id, [FromBody] PaymentDto updated)
        {
            var payment = _context.Payments.FirstOrDefault(p => p.payment_id == id);
            if (payment == null)
                return NotFound();

            payment.status = updated.status;
            _context.SaveChanges();
            return Ok(updated);
        }

        // Ödeme sil
      
        [HttpDelete("deletePaymentById/{id}")]
        public IActionResult Delete(int id)
        {
            var payment = _context.Payments.FirstOrDefault(p => p.payment_id == id);
            if (payment == null)
                return NotFound();

            _context.Payments.Remove(payment);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
