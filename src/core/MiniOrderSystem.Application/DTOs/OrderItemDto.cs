namespace MiniOrderSystem.Application.DTOs
{
    public record OrderItemDto : BaseDto
    {
        public int? Quantity { get; init; } = default;
        public decimal? Price { get; init; } = default;
        public decimal? TotalPrice { get; init; } = default;
        public DateTime? CreatedAt { get; init; } = default;

        public ProductDto? Product { get; set; } = default;
        public OrderDto? Order { get; set; } = default;
    }
}
