using DynSec.GraphQL;
using DynSec.MQTT;
using DynSec.Protocol;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Scalar.AspNetCore;
using Serilog;
using System.IdentityModel.Tokens.Jwt;

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

            builder.AddServiceDefaults();

            // Bind configuration objects

            MQTTConfig mqttConfig = new();
            builder.Configuration.Bind("MQTT", mqttConfig);
            builder.Services.AddSingleton(mqttConfig);
            builder.Services.AddMqttOptions(mqttConfig);
            builder.Services.AddMqttClient();
            builder.Services.AddDynamicSecurityProtocol();

            // Add services to the container.

            builder.Services.AddControllers();
            builder.AddDynSecGraphQL();

            // Add Authentication

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddOpenIdConnect(options =>
            {
                var oidcConfig = builder.Configuration.GetSection("OpenIDConnectSettings");

                options.Authority = oidcConfig["Authority"];
                options.ClientId = oidcConfig["ClientId"];
                options.ClientSecret = oidcConfig["ClientSecret"];

                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.SignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;

                options.ResponseType = OpenIdConnectResponseType.Code;
                options.ResponseMode = OpenIdConnectResponseMode.Query;
                options.UsePkce = true;

                options.SaveTokens = true;
                options.GetClaimsFromUserInfoEndpoint = true;

                options.MapInboundClaims = false;
                options.TokenValidationParameters.NameClaimType = JwtRegisteredClaimNames.Name;
                options.TokenValidationParameters.RoleClaimType = "roles";

            });


            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddHealthChecks();

            var app = builder.Build();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseCors("GetorPost");
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

            app.MapControllers();
            app.UseStaticFiles();
            app.UseDefaultFiles();


            app.MapStaticAssets();

            app.MapFallbackToFile("/index.html");

            app.MapDefaultEndpoints();

            app.Run();
        }
    }
}
