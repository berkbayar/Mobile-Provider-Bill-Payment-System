using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MobileProviderAPI.Models;
using MobileProviderAPI.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MobileProviderAPI.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/website")]
    public class WebsiteController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public WebsiteController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Pay bill operation 
        [HttpPost("paybill")]
        public IActionResult PayBill(int subscriberNo, DateTime month)
        {
            var bill = _context.Bills.Include(b => b.Subscriber).ToList().FirstOrDefault(b => b.SubscriberId == subscriberNo && b.BillMonth == month);

            if (bill == null)
                return NotFound("Bill not found.");

            bill.PaidAmount = bill.TotalAmount;

            _context.SaveChanges();

            return Ok(new { Message = "Payment successful", bill });
        }

        // Add new bill
        [HttpPost("addbill")]
        [Authorize]
        public IActionResult AddBill([FromBody] Bill newBill)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // First check if Subscriber exists
            var existingSubscriber = _context.Subscribers.SingleOrDefault(s => s.SubscriberId == newBill.SubscriberId);

            // If Subscriber does not exist, create it first
            if (existingSubscriber == null)
            {
                var newSubscriber = new Subscriber { SubscriberId = newBill.SubscriberId, Name = newBill.Subscriber.Name };
                _context.Subscribers.Add(newSubscriber);
                // It's important to save Subscriber here because Bill is addicted to it
                _context.SaveChanges();
                // Assign the created Subscriber to the new Bill
                newBill.Subscriber = newSubscriber;
            }
            else
            {
                // If Subscriber exists, assign existing Subscriber to new Bill
                newBill.Subscriber = existingSubscriber;
            }

            // Now add new Bill
            _context.Bills.Add(newBill);
            // Call SaveChanges for new Bill
            _context.SaveChanges();

            return Ok(new { Message = "New bill and subscriber added", Bill = newBill });
        }

        [HttpGet("getall")]
        [Authorize]
        public IActionResult GetAllBills()
        {
            var bills = _context.Bills.Include(b => b.Subscriber).ToList(); // Load also subscriber info

            return Ok(bills);
        }
    }
}
