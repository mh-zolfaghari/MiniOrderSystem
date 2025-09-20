using MiniOrderSystem.Application.Common.Exceptions;
using MiniOrderSystem.Application.Common.Security;
using MiniOrderSystem.Domain.Common;
using MiniOrderSystem.Domain.Repositories;

namespace MiniOrderSystem.Application.Behaviors
{
    internal class ClientAuthorizationBehavior<TRequest, TResponse>(IClientRepository clientRepository, IClient user)
        : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly IClientRepository _clientRepository = clientRepository;
        private readonly IClient _user = user;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is IAnonymousRequest)
                return await next(cancellationToken);

            if (_user.Token is null)
                throw new ClientUnAuthorizedException();

            var hasAccess = await _clientRepository.HasAccessAsync(_user.Token.Value, cancellationToken);
            if (!hasAccess) throw new ClientUnAuthorizedException();

            return await next(cancellationToken);
        }
    }
}
