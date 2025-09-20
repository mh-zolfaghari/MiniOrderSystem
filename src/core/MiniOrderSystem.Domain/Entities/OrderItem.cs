using MiniOrderSystem.Domain.Common;

namespace MiniOrderSystem.Domain.Entities
{
    public class OrderItem : ModificationProps
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; } = default!;
        public byte[] RowVersion { get; set; } = [];

        [NotMapped]
        public decimal TotalPrice => Price * Quantity;

        public Order Order { get; set; } = default!;
        public Product Product { get; set; } = default!;

        public OrderItem UpdateQuantity(int quantity, decimal price = 0)
        {
            Quantity = quantity;
            Price = Product is not null ? Product.Price : price;
            return this;
        }
    }
}
