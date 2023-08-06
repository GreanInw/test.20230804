using Autofac;
using BGCTest.Api.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace BGCTest.Api.Modules
{
    public class DbContextModule : Module
    {
        private readonly string _keyOfConnectionString;

        public DbContextModule(string keyOfConnectionString)
        {
            _keyOfConnectionString = keyOfConnectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(com =>
            {
                var configuration = com.Resolve<IConfiguration>();
                string connectionString = configuration.GetValue<string>(_keyOfConnectionString);
                return new DbContextOptionsBuilder<FoodDbContext>()
                    .UseSqlServer(connectionString, options =>
                    {
                        options.EnableRetryOnFailure(3);
                        options.MigrationsAssembly(typeof(FoodDbContext).Assembly.GetName().Name);
                    }).Options;
            }).SingleInstance();

            builder.Register(com =>
            {
                var dbContextOptions = com.Resolve<DbContextOptions<FoodDbContext>>();
                return new FoodDbContext(dbContextOptions);
            }).As<IFoodDbContext>().AsSelf().InstancePerLifetimeScope();
        }
    }
}
