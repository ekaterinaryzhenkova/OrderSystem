using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using OrderService.Application;
using OrderService.Broker;
using OrderService.Broker.Consumers;
using OrderService.Business;
using OrderService.Business.Commands;
using OrderService.Business.Repos;

namespace OrderService
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                });
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Order system API",
                    Version = "v1",
                    Description = "Creating orders and sending notifications"
                });
            });
            
            services.AddScoped<ICreateOrder, CreateOrder>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IPublisher, Publisher>();
            
            string dbConnStr = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<OrderServiceContext>(options =>
            {
                options.UseSqlServer(dbConnStr);
            });
            
            services.AddMassTransit(x =>
            {
                x.AddConsumer<ConfirmOrderConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(Configuration["RabbitMQ:Host"], "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<OrderServiceContext>();
                db.Database.Migrate();
            }
            
            app.UseHttpsRedirection();
            app.UseRouting();
            
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}