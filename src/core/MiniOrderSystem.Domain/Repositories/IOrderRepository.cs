using MiniOrderSystem.Domain.Entities;
using MiniOrderSystem.Domain.Types;
using MiniOrderSystem.Shared.Result;

namespace MiniOrderSystem.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task<Order?> GetActivePreInvoiceOrderAsync(int customerId, CancellationToken cancellationToken);
        Task<Order?> GetByOrderNumberAsync(string orderNumber, CancellationToken cancellationToken);
        Task<Order?> AddAsync(Order order, CancellationToken cancellationToken);
        Task<Result> ChangeStatusAsync(int id, OrderStatus status, CancellationToken cancellationToken);
        Task<Result> AddItemAsync(OrderItem item, CancellationToken cancellationToken);
        Task<Result> UpdateItemAsync(OrderItem item, CancellationToken cancellationToken);
        Task<Result> RemoveItemAsync(int orderId, int productId, CancellationToken cancellationToken);
        Task<OrderItem?> GetItemAsync(int orderId, int productId, bool hasInclude, CancellationToken cancellationToken);
    }
}
