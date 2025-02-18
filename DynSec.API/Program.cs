using DynSec.GraphQL;
using DynSec.MQTT;
using DynSec.Protocol;
using Microsoft.AspNetCore.Builder;
using Scalar.AspNetCore;
using Serilog;


namespace DynSec.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configure Serilog for logging

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .CreateLogger();

            builder.Host.UseSerilog();

            // Bind configuration objects

            MQTTConfig mqttConfig = new();
            builder.Configuration.Bind("MQTT", mqttConfig);
            builder.Services.AddSingleton(mqttConfig);
            builder.Services.AddMqttOptions(mqttConfig);
            builder.Services.AddMqttClient();
            builder.Services.AddDynamicSecurityProtocol();


            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDynSecGraphQL();



            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddHealthChecks();

            var app = builder.Build();

            app.UseRouting();
            

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference(options =>
                {
                    options.WithTitle("Mosquitto Dynamic Security Plugin")
                    .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient)
                    .WithTheme(ScalarTheme.BluePlanet);
                });
            }


            app.UseHttpsRedirection();
            app.MapDynSecGraphQL();
            app.MapHealthChecks("/health");

            app.MapControllers();

            app.UseAuthorization();
            app.UseStaticFiles();
            app.UseDefaultFiles();

            app.MapStaticAssets();



            app.MapFallbackToFile("/index.html");


            app.Run();
        }
    }
}
