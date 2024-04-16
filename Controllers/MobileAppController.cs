using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MobileProviderAPI.Models;
using MobileProviderAPI.Data;
using System.Linq;

namespace MobileProviderAPI.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/mobileapp")]
    [Authorize] // All endpoints on this controller require authorization.
    public class MobileAppController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MobileAppController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet("querybill")]
        public IActionResult QueryBill(int subscriberNo, DateTime month)
        {
            var bill = _context.Bills
                .Where(b => b.SubscriberId == subscriberNo && b.BillMonth == month)
                .Select(b => new { b.TotalAmount, b.PaidAmount })
                .FirstOrDefault();

            if (bill == null)
                return NotFound("Bill not found.");

            return Ok(bill);
        }


        /* querybilldetailed endpoint without paging
        [HttpGet("querybilldetailed")]
        public IActionResult QueryBillDetailed(int subscriberNo, DateTime month)
        {
            var billDetails = _context.Bills
                .Where(b => b.SubscriberId == subscriberNo && b.BillMonth == month)
                .Select(b => new { b.BillId, b.SubscriberId, b.TotalAmount, b.PaidAmount, b.BillMonth, b.Subscriber.Name })
                .FirstOrDefault();

            if (billDetails == null)
                return NotFound("Detailed bill information not found.");

            return Ok(billDetails);
        }*/

        //querybilldetailed endpoint with paging
        [HttpGet("querybilldetailed")]
        public IActionResult QueryBillDetailed(int subscriberNo, DateTime month, int pageNumber = 1, int pageSize = 10)
        {
            // Correct calculation is made for pagination
            int skip = (pageNumber - 1) * pageSize;

            var query = _context.Bills
                        .Where(b => b.SubscriberId == subscriberNo && b.BillMonth == month)
                        .Select(b => new { b.BillId, b.SubscriberId, b.TotalAmount, b.PaidAmount, b.BillMonth, b.Subscriber.Name });

            // Counting to get the total number of records
            int totalRecords = query.Count();

            // Implementing Skip and Take on Query
            var billDetails = query
                        .Skip(skip)
                        .Take(pageSize)
                        .ToList();

            if (!billDetails.Any())
                return NotFound("Detailed bill information not found.");

            // Returning an object containing pagination information
            var response = new
            {
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Details = billDetails
            };

            return Ok(response);
        }

    }
}
