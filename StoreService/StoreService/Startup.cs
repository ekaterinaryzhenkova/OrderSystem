using Microsoft.EntityFrameworkCore;
using StoreService.Business.Repos;
using StoreService.Data;
using MassTransit;
using StoreService.Broker;
using StoreService.Broker.Consumers;

namespace StoreService
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
            services.AddControllers();
            
            services.AddScoped<IStoreRepository, StoreRepository>();
            services.AddScoped<IPublisher, Publisher>();
            
            string dbConnStr = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<StoreServiceContext>(options =>
            {
                options.UseSqlServer(dbConnStr);
            });
            
            services.AddMassTransit(x =>
            {
                x.AddConsumer<CreateOrderConsumer>();

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
                var db = scope.ServiceProvider.GetRequiredService<StoreServiceContext>();
                db.Database.Migrate();
            }
            
            app.UseHttpsRedirection();
            app.UseRouting();
            
            //app.UseAuthentication();
            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}