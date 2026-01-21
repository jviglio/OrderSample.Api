using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using OrderSample.Application.Commands.Orders.CancelOrder;
using OrderSample.Application.Commands.Orders.CreateOrder;
using OrderSample.Application.Abstractions;
using OrderSample.Infrastructure.Persistence;
using OrderSample.Application.Queries.Orders.GetOrders;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using OrderSample.Infrastructure.Ai;

namespace OrderSample.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Register services
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        var jwt = Configuration.GetSection("Jwt");

                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,

                            ValidIssuer = jwt["Issuer"],
                            ValidAudience = jwt["Audience"],
                            IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(jwt["Key"]!)
                            ),

                            ClockSkew = TimeSpan.Zero
                        };
                    });

            services.AddAuthorization();

            // DbContext
            services.AddDbContext<OrderSample.Infrastructure.Persistence.OrderDbContext>(
                options => options.UseInMemoryDatabase("OrdersDb")
            );

            services.AddScoped<CreateOrderCommandHandler>();
            services.AddScoped<CancelOrderCommandHandler>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<GetOrdersQueryHandler>();
            services.AddScoped<IOrderReadRepository, OrderReadRepository>();

            // Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "OrderSample API",
                    Version = "v1"
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header usando el esquema Bearer.\r\n\r\n" +
                                  "Ejemplo: \"Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
            });


            var aiSection = Configuration.GetSection("Ai");
            var baseUrl = aiSection["BaseUrl"] ?? throw new Exception("Ai:BaseUrl missing");
            var apiKey = aiSection["ApiKey"] ?? throw new Exception("Ai:ApiKey missing");
            var model = aiSection["Model"] ?? "gpt-4o-mini";

            services.AddHttpClient<IAiClient, OpenAiClient>(http =>
            {
                OpenAiClient.ConfigureHttpClient(http, baseUrl, apiKey);
            });

            services.AddSingleton(model);

        }


        // Configure HTTP pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            // Swagger (antes del routing)
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "OrderSample API v1");
            });

            app.UseHttpsRedirection();

            // Routing SIEMPRE antes de endpoints
            app.UseRouting();

            app.UseAuthentication();            
            app.UseAuthorization();

            // Endpoints SIEMPRE después de UseRouting
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
