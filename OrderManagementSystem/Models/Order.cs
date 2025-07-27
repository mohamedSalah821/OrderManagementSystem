using System;
using System.Collections.Generic;

namespace OrderManagementSystem.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; } // Foreign Key
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public decimal TotalAmount { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public string Status { get; set; } = "Pending";

        // Navigation Properties
        public Customer? Customer { get; set; }
        public List<OrderItem>? OrderItems { get; set; }
    }
}
