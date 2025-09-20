using MiniOrderSystem.Application.DTOs;
using MiniOrderSystem.Application.UseCases.Orders.Commands.UpsertOrderItem;
using MiniOrderSystem.Domain.Entities;
using MiniOrderSystem.Domain.Types;
using MiniOrderSystem.Shared.Extensions;

namespace MiniOrderSystem.Application.Helpers
{
    internal static class OrderHelpers
    {

        internal static Order CreateInstance(int customerId)
            => new()
            {
                CustomerId = customerId,
                OrderNumber = OrderExtensions.CreateOrderNumber(),
                Status = OrderStatus.PreInvoice,
                TotalAmount = 0
            };

        internal static OrderItem CreateInstance(this UpsertOrderItemCommand command, int orderId, Product product)
            => new()
            {
                OrderId = orderId,
                Price = product.Price,
                ProductId = command.ProductId,
                Quantity = command.Quantity
            };

        internal static OrderDto? ToDto(this Order? model, int? id = default)
        {
            if (model is not null)
                return new()
                {
                    Id = model.Id,
                    OrderNumber = model.OrderNumber,
                    Status = model.Status,
                    TotalAmount = model.TotalAmount,
                    CreatedAt = model.CreatedAt,
                    Customer = model.Customer.ToDto(model.CustomerId),
                    OrderItems = model.OrderItems?.Select(x => x.ToDto(x.Id)!)
                };
            if (id is not null)
                return new()
                {
                    Id = id.Value
                };
            return null;
        }

        internal static OrderItemDto? ToDto(this OrderItem? model, int? id = default)
        {
            if (model is not null)
                return new()
                {
                    Id = model.Id,
                    Quantity = model.Quantity,
                    Price = model.Price,
                    TotalPrice = model.TotalPrice,
                    CreatedAt = model.CreatedAt,
                    Product = model.Product.ToDto(model.ProductId),
                    Order = model.Order.ToDto(model.OrderId)
                };
            if (id is not null)
                return new()
                {
                    Id = id.Value
                };
            return null;
        }
    }
}
