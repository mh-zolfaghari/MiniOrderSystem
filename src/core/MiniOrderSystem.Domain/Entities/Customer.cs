using MiniOrderSystem.Domain.Common;
using MiniOrderSystem.Domain.ValueObjects;

namespace MiniOrderSystem.Domain.Entities
{
    public class Customer : ModificationProps, IActivationProps, IClient
    {
        public Guid? Token { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public bool? IsActive { get; set; } = default;
        public Address Address { get; set; } = default!;

        public ICollection<Order> Orders { get; set; } = [];
    }
}
