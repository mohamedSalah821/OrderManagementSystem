namespace OrderManagementSystem.Dots
{
    public class CreateOrderDto
    {
        public int CustomerId { get; set; }
        public string PaymentMethod { get; set; }
        public List<CreateOrderItemDto> OrderItems { get; set; }
    }

}
