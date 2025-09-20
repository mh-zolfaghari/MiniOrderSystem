using MiniOrderSystem.Infrastructure.Persistence.Context;

namespace MiniOrderSystem.Presentation.Extensions
{
    public static class InitializerExtensions
    {
        public static async Task InitializeDatabaseAsync(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var initializer = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();
            await initializer.InitializeAsync();
            await initializer.SeedDataAsync();
        }
    }
}
