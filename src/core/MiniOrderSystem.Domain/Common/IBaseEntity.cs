namespace MiniOrderSystem.Domain.Common
{
    public interface IBaseEntity : IEntity
    {
        int Id { get; }
    }

    public abstract class BaseEntity : IBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }
    }
}
