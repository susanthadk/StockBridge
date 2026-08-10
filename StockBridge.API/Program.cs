using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using StockBridge.API.Middleware;
using StockBridge.Application.Common;
using StockBridge.Domain.Interfaces;
using StockBridge.Infrastructure.Persistence;
using System.Text;

public partial class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Database configuration
        builder.Services.AddDbContext<StockBridgeDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("StockBridgeConnection")));

        // Mandatory for accessing User Claims in a service
        builder.Services.AddHttpContextAccessor();

        builder.Services.AddAutoMapper(cfg => { }, typeof(ResponseInfo<>).Assembly);

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["Auth:Issuer"],
                    ValidateAudience = true,
                    ValidAudiences = [builder.Configuration["Auth:Issuer"]!, builder.Configuration["Auth:Audience"]!],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["Auth:SigningKey"]!)),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        builder.Services.AddAuthorization();

        builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Auth"));

        // Add services to the container.

        builder.Services.AddControllers()
            .AddJsonOptions(options =>
                options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter()));
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        ConfigureServices(builder.Services);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseCors(option => option
            .SetIsOriginAllowed(_ => true)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseUserLogging();

        app.UseGlobalExceptionHandler();

        app.UseRequestThrottling();

        app.UseRateLimiting();

        app.MapControllers();
        app.MapGet("/", () => Results.Redirect("/scalar"));
        app.MapScalarApiReference("/scalar", options =>
        {
            options.Title = "StockBridge API";
            options.Theme = ScalarTheme.BluePlanet;
        });

        app.Run();
    }
}