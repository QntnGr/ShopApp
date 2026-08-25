using Microsoft.EntityFrameworkCore;
using ShopApp.Infrastructure.Persistence;
using ShopApp.Application;
using ShopApp.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Auto-migrate in development (use proper migrations in production!)
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

//app.UseGlobalExceptionHandler();

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health");

app.Run();
