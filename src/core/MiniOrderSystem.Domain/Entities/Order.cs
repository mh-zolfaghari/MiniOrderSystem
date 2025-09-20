using MiniOrderSystem.Domain.Common;
using MiniOrderSystem.Domain.Types;

namespace MiniOrderSystem.Domain.Entities
{
    public class Order : ModificationProps
    {
        public int CustomerId { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public OrderStatus Status { get; set; } = OrderStatus.PreInvoice;
        public decimal TotalAmount { get; set; }

        public Customer Customer { get; set; } = default!;
        public ICollection<OrderItem> OrderItems { get; set; } = [];
    }
}
