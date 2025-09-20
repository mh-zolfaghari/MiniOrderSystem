using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using MiniOrderSystem.Application;
using MiniOrderSystem.Domain.Common;
using MiniOrderSystem.Infrastructure;
using MiniOrderSystem.Presentation.Extensions;
using MiniOrderSystem.Presentation.Filters;
using MiniOrderSystem.Presentation.Middleware;
using MiniOrderSystem.Presentation.Services;
using MiniOrderSystem.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IClient, CurrentUser>();
builder.Services.AddHttpContextAccessor();

builder.Services
    .AddOptions<AppSettings>()
    .Bind(builder.Configuration.GetSection(AppSettings.ConfigurationSectionName))
    .ValidateDataAnnotations();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services
    .RegisterApplicationServices(builder.Configuration)
    .RegisterInfrastructureServices(builder.Configuration);

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MiniOrderSystem",
        Version = "v1",
    });

    options.OperationFilter<SwaggerAuthenticationFilter>();
});

var app = builder.Build();

app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    await app.InitializeDatabaseAsync();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

// Enabled the Swagger middleware in the development environment
if (app.Environment.IsDevelopment())
{
    // Configure the Swagger middleware
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint($"/swagger/v1/swagger.json", $"Mini Order System API v1");
        options.DefaultModelsExpandDepth(-1);
    });
}

// Configure the Serilog middleware
app.UseSerilogRequestLogging();

// Run the application
app.Run();
