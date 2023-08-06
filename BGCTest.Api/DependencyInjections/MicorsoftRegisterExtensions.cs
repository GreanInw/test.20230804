using BGCTest.Api.BackgroundServices;
using System.Text.Json.Serialization;

namespace BGCTest.Api.DependencyInjections
{
    public static class MicorsoftRegisterExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddOptions();
            services.AddLogging();
            services.AddHttpContextAccessor();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();

                });
            });
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            services.AddHostedService<AutoMigrationService>();
            return services;
        }

        public static void ConfigurationApplication(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors();
            app.UseRouting();
            app.UseAuthorization();
            app.MapControllers();
        }
    }
}