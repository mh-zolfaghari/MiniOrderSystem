namespace MiniOrderSystem.Domain.Common
{
    public interface IClient
    {
        Guid? Token { get; }
    }
}
