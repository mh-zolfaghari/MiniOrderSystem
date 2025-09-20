namespace MiniOrderSystem.Application.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse>(ILogger<TRequest> logger)
        : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly ILogger _logger = logger;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var response = await next(cancellationToken);

            logger.LogInformation("MiniOrderSystem Response: {requestName} {@Response}", requestName, response);

            return response;
        }
    }
}
