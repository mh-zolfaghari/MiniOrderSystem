namespace MiniOrderSystem.Application.DTOs
{
    public record ProductDto : BaseDto
    {
        public string? Name { get; init; } = default;
        public decimal? Price { get; init; } = default;
        public string? Description { get; init; } = default;
        public DateTime? CreatedAt { get; init; } = default;
    }
}
