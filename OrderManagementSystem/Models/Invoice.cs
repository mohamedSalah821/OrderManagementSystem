using System;

namespace OrderManagementSystem.Models
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public int OrderId { get; set; }     // Foreign Key
        public DateTime InvoiceDate { get; set; } = DateTime.Now;
        public decimal TotalAmount { get; set; }

        // Navigation Property
        public Order? Order { get; set; }
    }
}
