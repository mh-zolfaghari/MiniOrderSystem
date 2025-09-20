using MiniOrderSystem.Domain.Entities;
using MiniOrderSystem.Domain.Repositories;
using MiniOrderSystem.Domain.Types;
using MiniOrderSystem.Infrastructure.Persistence.Context;
using MiniOrderSystem.Shared;
using MiniOrderSystem.Shared.Result;

namespace MiniOrderSystem.Infrastructure.Persistence.Repositories
{
    public class OrderRepository(ApplicationDbContext dbContext, ILogger<OrderRepository> logger) : IOrderRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
        private readonly ILogger<OrderRepository> _logger = logger;

        public Task<Order?> GetActivePreInvoiceOrderAsync(int customerId, CancellationToken cancellationToken)
            => _dbContext.Orders
                    .Include(x => x.Customer)
                    .Include(x => x.OrderItems)
                        .ThenInclude(x => x.Product)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.CustomerId == customerId && x.Status == OrderStatus.PreInvoice, cancellationToken);

        public async Task<Order?> AddAsync(Order order, CancellationToken cancellationToken)
        {
            _dbContext.Orders.Add(order);
            var resultDb = await _dbContext.SaveChangesAsync(cancellationToken);
            return resultDb.IsFailure ? null : order;
        }

        public async Task<Result> ChangeStatusAsync(int id, OrderStatus status, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _dbContext.Orders
                    .Where(x => x.Id == id)
                    .ExecuteUpdateAsync(x => x.SetProperty(p => p.Status, status), cancellationToken);

                return result > 0
                    ? Result.Success()
                    : Result.Failure(ConstantMessages.SaveChangesFailed);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while changing the order status. OrderId: {OrderId}, Status: {Status}", id, status);
                return Result.Failure(ConstantMessages.SaveChangesFailed);
            }
        }

        public Task<Order?> GetByOrderNumberAsync(string orderNumber, CancellationToken cancellationToken)
            => _dbContext.Orders
                .AsNoTracking()
                .Include(x => x.OrderItems)
                .Include(x => x.Customer)
                .Select(x => new Order
                {
                    Id = x.Id,
                    OrderNumber = x.OrderNumber,
                    Status = x.Status,
                    TotalAmount = x.TotalAmount,
                    CustomerId = x.CustomerId,
                    CreatedAt = x.CreatedAt,
                    Customer = new Customer
                    {
                        Id = x.Customer.Id,
                        Name = x.Customer.Name,
                        CreatedAt = x.Customer.CreatedAt
                    },
                    OrderItems = x.OrderItems.Select(x => new OrderItem
                    {
                        OrderId = x.OrderId,
                        ProductId = x.ProductId,
                        Quantity = x.Quantity,
                        Price = x.Price,
                        CreatedAt = x.CreatedAt,
                        Product = new Product
                        {
                            Id = x.Product.Id,
                            Name = x.Product.Name,
                            Price = x.Product.Price,
                            Description = x.Product.Description,
                            CreatedAt = x.Product.CreatedAt
                        }
                    }).ToList()
                })
                .FirstOrDefaultAsync(x => x.OrderNumber == orderNumber, cancellationToken);

        public async Task<Result> AddItemAsync(OrderItem item, CancellationToken cancellationToken)
        {
            using var dbTransaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                _dbContext.OrderItems.Add(item);
                var result = await _dbContext.SaveChangesAsync(cancellationToken);

                if (result.IsFailure)
                    return Result.Failure(ConstantMessages.InsertRecordFailed);

                await CalculateOrderTotalAmountAsync(item.OrderId, cancellationToken);
                await dbTransaction.CommitAsync(cancellationToken);

                return Result.Success();
            }
            catch (Exception ex)
            {
                await dbTransaction.RollbackAsync(cancellationToken);
                _logger.LogError(ex, "An error occurred while adding an order item. OrderId: {OrderId}, ProductId: {ProductId}", item.OrderId, item.ProductId);
                return Result.Failure(ConstantMessages.InsertRecordFailed);
            }
        }

        public async Task<Result> UpdateItemAsync(OrderItem item, CancellationToken cancellationToken)
        {
            using var dbTransaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                _dbContext.OrderItems.Update(item);
                var result = await _dbContext.SaveChangesAsync(cancellationToken);

                if (result.IsFailure)
                    return Result.Failure(ConstantMessages.UpdateRecordFailed);

                await CalculateOrderTotalAmountAsync(item.OrderId, cancellationToken);
                await dbTransaction.CommitAsync(cancellationToken);

                return Result.Success();
            }
            catch (Exception ex)
            {
                await dbTransaction.RollbackAsync(cancellationToken);
                _logger.LogError(ex, "An error occurred while updating an order item. OrderId: {OrderId}, ProductId: {ProductId}", item.OrderId, item.ProductId);
                return Result.Failure(ConstantMessages.UpdateRecordFailed);
            }
        }

        public async Task<Result> RemoveItemAsync(int orderId, int productId, CancellationToken cancellationToken)
        {
            using var dbTransaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                await _dbContext.OrderItems.Where(x => x.OrderId == orderId && x.ProductId == productId).ExecuteDeleteAsync(cancellationToken);
                await CalculateOrderTotalAmountAsync(orderId, cancellationToken);
                await dbTransaction.CommitAsync(cancellationToken);

                return Result.Success();
            }
            catch (Exception ex)
            {
                await dbTransaction.RollbackAsync(cancellationToken);
                _logger.LogError(ex, "An error occurred while removing an order item. OrderId: {OrderId}, ProductId: {ProductId}", orderId, productId);
                return Result.Failure(ConstantMessages.DeleteRecordFailed);
            }
        }

        public Task<OrderItem?> GetItemAsync(int orderId, int productId, bool hasInclude, CancellationToken cancellationToken)
        {
            var query = _dbContext.OrderItems.AsQueryable();

            if (hasInclude)
            {
                query = query.Include(x => x.Order)
                                .ThenInclude(x => x.Customer)
                             .Include(x => x.Product);
            }

            return query.FirstOrDefaultAsync(x => x.OrderId == orderId && x.ProductId == productId, cancellationToken);
        }

        private Task CalculateOrderTotalAmountAsync(int orderId, CancellationToken cancellationToken)
            => _dbContext.Orders
                .Where(x => x.Id == orderId)
                .ExecuteUpdateAsync(x => x.SetProperty(p => p.TotalAmount, p => _dbContext.OrderItems.Where(x => x.OrderId == orderId).Sum(x => (x.Price * x.Quantity))), cancellationToken);
    }
}
