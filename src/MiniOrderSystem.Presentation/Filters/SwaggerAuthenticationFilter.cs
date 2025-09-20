using MiniOrderSystem.Domain.Common;

namespace MiniOrderSystem.Presentation.Filters
{
    /// <summary>
    /// Adds a custom parameter named “Token” to the header of every operation,
    /// which requires the user to enter their subscription key.
    /// </summary>
    public class SwaggerAuthenticationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters ??= new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = nameof(IClient.Token),
                In = ParameterLocation.Header,
                Description = $"Enter your client {nameof(IClient.Token)}"
            });
        }
    }
}
