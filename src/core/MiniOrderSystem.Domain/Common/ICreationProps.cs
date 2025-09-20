namespace MiniOrderSystem.Domain.Common
{
    public interface ICreationProps
    {
        DateTime CreatedAt { get; }
    }

    public abstract class CreationProps : BaseEntity, ICreationProps
    {
        [Required]
        public DateTime CreatedAt { get; init; }
    }
}
