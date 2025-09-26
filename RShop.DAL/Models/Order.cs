using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RShop.DAL.Models
{
    public enum OrderStatus { 
        Pending=1,
        Cancelled=2, 
        Approved=3, 
        Shipped=4, 
        Delivered=5
    }
    public enum PaymentMethod
    {
        Visa=1,
        CashOnDelivery=2,
    }


    public class Order
    {
        public int Id { get; set; }
        public OrderStatus status { get; set; }= OrderStatus.Pending;
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public DateTime ShippedDate { get; set; }
        public double TotalAmount { get; set; }

        public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.CashOnDelivery;
        public string? PaymentId { get; set; }
        public string? CarrierName { get; set; }
        public string? TrackingNumber { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

    }
}
