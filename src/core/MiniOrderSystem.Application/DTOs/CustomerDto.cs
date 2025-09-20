using MiniOrderSystem.Domain.ValueObjects;

namespace MiniOrderSystem.Application.DTOs
{
    public record CustomerDto : BaseDto
    {
        public Guid? Token { get; init; } = default;
        public string? Name { get; init; } = default;
        public string? PhoneNumber { get; set; } = default;
        public bool? IsActive { get; set; } = default;
        public string? Country { get; init; } = default;
        public string? City { get; init; } = default;
        public string? Street { get; init; } = default;
        public string? PostalCode { get; init; } = default;
        public DateTime? CreatedAt { get; init; } = default;
    }
}
