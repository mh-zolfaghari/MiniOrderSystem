namespace MiniOrderSystem.Domain.Repositories
{
    public interface IClientRepository
    {
        Task<bool> HasAccessAsync(Guid token, CancellationToken cancellationToken);
    }
}
