using OrderManagementSystem.Models;

namespace OrderManagementSystem.Repositories
{
    public interface IInvoiceRepository
    {
        Task AddAsync(Invoice invoice);
        Task<Invoice?> GetByIdAsync(int id);
        Task<List<Invoice>> GetAllAsync();
        Task SaveChangesAsync();
    }
}
