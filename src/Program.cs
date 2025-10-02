using CerealAPI.src.Data;
using CerealAPI.src.Repository;
using CerealAPI.src.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.OpenApi.Models;

namespace CerealAPI.src
{

    class Program
    {

    /// <summary>
    /// Entry point for application
    /// </summary>
    /// <param name="args"></param>
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
                
        builder.Services.AddControllers();
        builder.Services.AddTransient<Seed>();
               
        
        builder.Services.AddEndpointsApiExplorer();

        // Setup swagger for Authentication
        builder.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey, 
                Scheme = "Bearer",              
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter 'Bearer' followed by your JWT token."
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {

                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        In = ParameterLocation.Header,
                        Name = "Authorization",
                        Scheme = "Bearer"
                    },

                new List<string>()
                }
            });
        });

        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        // Build Database
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(
            config.GetConnectionString("DefaultConnection"),
            ServerVersion.AutoDetect(config.GetConnectionString("DefaultConnection"))
        ));

        // Dependency injection
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped<IAuthService, AuthService>();

        // Authentication setup
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["AppSettings:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["AppSettings:Audience"],
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Token"]!)),
            ValidateIssuerSigningKey = true,
        });
      
        var webApp =  builder.Build();

        // If args == seed, populate the database
        if (args.Length > 0 && args[0].ToLower() == "seed")
        {
            using (var scope = webApp.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                Seed.SeedDatabase(context, "data\\Cereal.csv");
            }
        }

        // Configure the HTTP request pipeline.
        if (webApp.Environment.IsDevelopment())
        {
            webApp.UseSwagger(c =>
            {
                c.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi2_0;
            });
            webApp.UseSwaggerUI();
        }
        
        webApp.UseHttpsRedirection();

        webApp.UseAuthentication();
        webApp.UseAuthorization();

        webApp.MapControllers();

        // Run web app
        webApp.Run();

        }
    };
}