using System;

namespace MobileProviderAPI.Models
{
    public class Bill
    {
        public int BillId { get; set; }
        public int SubscriberId { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public DateTime BillMonth { get; set; }

        
        public Subscriber Subscriber { get; set; }
    }
}
