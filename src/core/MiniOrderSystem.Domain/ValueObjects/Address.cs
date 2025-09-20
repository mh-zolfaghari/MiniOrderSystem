namespace MiniOrderSystem.Domain.ValueObjects
{
    public record Address
        (
            string Street,
            string City,
            string Country,
            string PostalCode
        );
}
