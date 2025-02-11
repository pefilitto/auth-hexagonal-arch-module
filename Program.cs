using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Cors.Infrastructure;
using System.Text;
using auth_hexagonal_arch_module.Domain.Repository;
using auth_hexagonal_arch_module.Domain.Services;
using auth_hexagonal_arch_module.Infrastructure.Repository;
using auth_hexagonal_arch_module.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

var jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET");
if (string.IsNullOrEmpty(jwtSecret))
{
    throw new InvalidOperationException("JWT_SECRET environment variable is not set.");
}
var key = Encoding.UTF8.GetBytes(jwtSecret);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddScoped<ApplicationPostgresContext>(provider =>
{
    var connectionString = Environment.GetEnvironmentVariable("URL_POSTGRES");
    if (!string.IsNullOrEmpty(connectionString))
    {
        return new ApplicationPostgresContext(connectionString);
    }
    return new ApplicationPostgresContext("");
});

builder.Services.AddScoped<IUserRepository, UserPostgresRepository>();
builder.Services.AddScoped<IAuthService, AuthJwtService>();

var app = builder.Build();

app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/api", () =>
{
    return "Aplicação escutando na porta 5000";
});

app.Run();
