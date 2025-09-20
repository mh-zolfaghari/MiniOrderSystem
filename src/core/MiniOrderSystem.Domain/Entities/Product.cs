using MiniOrderSystem.Domain.Common;

namespace MiniOrderSystem.Domain.Entities
{
    public class Product : ModificationProps
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? Description { get; set; } = default;

        public ICollection<OrderItem> OrderItems { get; set; } = [];
    }
}