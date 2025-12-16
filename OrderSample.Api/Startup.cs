using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

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
            
            services.AddScoped<Application.Orders.OrderService>();
            services.AddScoped<Application.Orders.IOrderRepository,
                               Infrastructure.Persistence.OrderRepository>();

            // DbContext
            services.AddDbContext<OrderSample.Infrastructure.Persistence.OrderDbContext>(
                options => options.UseInMemoryDatabase("OrdersDb")
            );

            // Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "OrderSample API",
                    Version = "v1"
                });
            });
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

            app.UseAuthorization();

            // Endpoints SIEMPRE después de UseRouting
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
