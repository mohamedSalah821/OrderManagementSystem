namespace OrderManagementSystem.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }

        // Navigation Property
        public List<OrderItem>? OrderItems { get; set; }
    }
}
