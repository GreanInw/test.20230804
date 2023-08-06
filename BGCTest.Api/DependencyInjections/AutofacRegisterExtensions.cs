using Autofac;
using Autofac.Extensions.DependencyInjection;
using BGCTest.Api.Modules;

namespace BGCTest.Api.DependencyInjections
{
    public static class AutofacRegisterExtensions
    {
        public static IHostBuilder RegisterComponentWithAutofac(this IHostBuilder host, IConfiguration configuration)
        {
            host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureContainer<ContainerBuilder>(builder =>
                {
                    builder.RegisterModule(new DbContextModule("FoodSaleConnectionString"))
                        .RegisterModule(new UnitOfWorkModule())
                        .RegisterModule(new RepositoryModule())
                        .RegisterModule(new ServiceModule());
                });

            return host;
        }
    }
}