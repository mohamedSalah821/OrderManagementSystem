using OrderManagementSystem.Models;
using OrderManagementSystem.Repositories;

public class OrderService
{
    private readonly IProductRepository _productRepository;

    public OrderService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<bool> HasSufficientStockAsync(List<OrderItem> items)
    {
        foreach (var item in items)
        {
            var product = await _productRepository.GetByIdAsync(item.ProductId);
            if (product == null || item.Quantity > product.Stock)
                return false;
        }
        return true;
    }

    public decimal CalculateTotalWithDiscount(List<OrderItem> items)
    {
        decimal total = 0;

        foreach (var item in items)
        {
            var priceAfterDiscount = item.UnitPrice * (1 - item.Discount);
            total += priceAfterDiscount * item.Quantity;
        }

        if (total > 200)
            total *= 0.90m; // 10% discount
        else if (total > 100)
            total *= 0.95m; // 5% discount

        return total;
    }
}
