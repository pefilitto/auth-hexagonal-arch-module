using auth_hexagonal_arch_module.Infrastructure.Interfaces;
using auth_hexagonal_arch_module.Infrastructure.Repository.User;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ApplicationPostgresContext>(provider =>
{
    var connectionString = builder.Configuration.GetConnectionString("PostgresConnection");
    return new ApplicationPostgresContext(connectionString);
});

builder.Services.AddScoped<IUserPostgresRepository, UserPostgresRepository>();

var app = builder.Build();

app.MapGet("/", () =>
{
    return "Aplicação escutando na porta 5000";
});

app.Run();
