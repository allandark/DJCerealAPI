using Microsoft.AspNetCore.Authentication.Negotiate;

namespace CerealAPI.src
{



    class Program
    {
            WebApplication webApp;

            public Program(string[] args)
            {
                webApp = InitializeWebApp(args);
            }
            public static void Main(string[] args)
            {

                Program driver = new Program(args);
                driver.Run();
            
            }

            public void Run()
            {
                // Configure the HTTP request pipeline.
                if (webApp.Environment.IsDevelopment())
                {
                    webApp.UseSwagger();
                    webApp.UseSwaggerUI();
                }

                webApp.UseHttpsRedirection();

                webApp.UseAuthorization();

                webApp.MapControllers();

                webApp.Run();
            }


            WebApplication InitializeWebApp(string[] args)
            {
                var builder = WebApplication.CreateBuilder(args);

                // Add services to the container.

                builder.Services.AddControllers();
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

                builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
                   .AddNegotiate();

                builder.Services.AddAuthorization(options =>
                {
                    // By default, all incoming requests will be authorized according to the default policy.
                    options.FallbackPolicy = options.DefaultPolicy;
                });

                return builder.Build();
            }
        };




}