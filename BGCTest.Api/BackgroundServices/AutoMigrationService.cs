using BGCTest.Api.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace BGCTest.Api.BackgroundServices
{
    public class AutoMigrationService : IHostedService
    {
        private readonly ILogger<AutoMigrationService> _logger;
        private readonly IFoodDbContext _foodDbContext;

        public AutoMigrationService(ILogger<AutoMigrationService> logger, IFoodDbContext foodDbContext)
        {
            _logger = logger;
            _foodDbContext = foodDbContext;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Start auto migration.");
                await _foodDbContext.Database.MigrateAsync(cancellationToken);
                _logger.LogInformation("End auto migration.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
            => Task.CompletedTask;
    }
}
