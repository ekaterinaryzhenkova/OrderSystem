using MassTransit;
using Microsoft.EntityFrameworkCore;
using NotificationService.Application;
using NotificationService.Broker;
using NotificationService.Business;
using NotificationService.Business.Repos;

namespace NotificationService
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
            
            
            services.Configure<EmailOptions>(Configuration.GetSection("EmailOptions"));

            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            
            string dbConnStr = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<NotificationServiceContext>(options =>
            {
                options.UseSqlServer(dbConnStr);
            });
            
            services.AddMassTransit(x =>
            {
                x.AddConsumer<SendNotificationConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(Configuration["RabbitMQ:Host"], "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });
                    
                    cfg.UseDelayedRedelivery(r =>
                    {
                        r.Intervals(
                            TimeSpan.FromSeconds(10),
                            TimeSpan.FromSeconds(30),
                            TimeSpan.FromMinutes(1));
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<NotificationServiceContext>();
                db.Database.Migrate();
            }
            
            app.UseHttpsRedirection();
            app.UseRouting();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}