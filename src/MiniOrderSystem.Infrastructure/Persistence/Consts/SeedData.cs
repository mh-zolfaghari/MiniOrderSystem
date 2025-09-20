namespace MiniOrderSystem.Infrastructure.Persistence.Consts
{
    public static class SeedData
    {
        public static class Customer
        {
            public static Guid Token = Guid.Parse("d290f1ee-6c54-4b01-90e6-d701748f0851");
            public const string Name = "Mohammad Hossein Zolfaghari";
            public const string PhoneNumber = "+989100036374";

            public static class Address
            {
                public const string Country = "Iran";
                public const string City = "Tehran";
                public const string Street = "Sample Street, Sample Alley, Plaque 1";
                public const string PostalCode = "1234567890";
            }
        }

        public static class Product
        {
            public static IReadOnlyList<MiniOrderSystem.Domain.Entities.Product> DefaultProducts = new List<MiniOrderSystem.Domain.Entities.Product>
            {
                new() {
                    Name = "Product 1",
                    Description = "This is the description for Product 1.",
                    Price = 10.99m
                },
                new() {
                    Name = "Product 2",
                    Description = "This is the description for Product 2.",
                    Price = 20.50m
                },
                new() {
                    Name = "Product 3",
                    Description = "This is the description for Product 3.",
                    Price = 15.75m
                },
                new() {
                    Name = "Product 4",
                    Description = "This is the description for Product 4.",
                    Price = 24m
                },
                new() {
                    Name = "Product 5",
                    Description = "This is the description for Product 5.",
                    Price = 66.15m
                }
            };
        }
    }
}
