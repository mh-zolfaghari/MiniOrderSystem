namespace MiniOrderSystem.Domain.Common
{
    public interface IModificationProps : ICreationProps
    {
        DateTime? UpdatedAt { get; }
    }

    public abstract class ModificationProps : CreationProps, IModificationProps
    {
        public DateTime? UpdatedAt { get; set; }
    }
}
