using MiniOrderSystem.Domain.Types;

namespace MiniOrderSystem.Application.DTOs
{
    public record OrderDto : BaseDto
    {
        public string? OrderNumber { get; init; } = default;
        public OrderStatus? Status { get; init; } = default;
        public decimal? TotalAmount { get; init; } = default;
        public DateTime? CreatedAt { get; init; } = default;

        public CustomerDto? Customer { get; set; } = default;
        public IEnumerable<OrderItemDto>? OrderItems { get; set; } = default;
    }
}
