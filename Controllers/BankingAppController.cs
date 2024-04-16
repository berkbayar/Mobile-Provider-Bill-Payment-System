using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MobileProviderAPI.Models;
using MobileProviderAPI.Data;
using System.Linq;

namespace MobileProviderAPI.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/bankingapp")]
    [Authorize] // All endpoints on this controller require authorization.
    public class BankingAppController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BankingAppController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Query unpaid bills
        [HttpGet("queryunpaidbills")]
        public IActionResult QueryUnpaidBills(int subscriberNo)
        {
            var unpaidBills = _context.Bills
                .Where(b => b.SubscriberId == subscriberNo && b.PaidAmount != b.TotalAmount)
                .OrderBy(b => b.BillMonth)
                .Select(b => new { b.BillMonth, b.TotalAmount })
                .ToList();

            if (unpaidBills == null || !unpaidBills.Any())
                return NotFound("No unpaid bills found.");

            return Ok(unpaidBills);
        }
    }
}
