using Autofac;
using BGCTest.Api.Repositories;

namespace BGCTest.Api.Modules
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FoodSaleRepository>().As<IFoodSaleRepository>().InstancePerLifetimeScope();
        }
    }
}