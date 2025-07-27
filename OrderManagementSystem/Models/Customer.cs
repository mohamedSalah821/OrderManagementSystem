﻿using System.Collections.Generic;

namespace OrderManagementSystem.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // Navigation Property
        public List<Order>? Orders { get; set; }
    }
}
