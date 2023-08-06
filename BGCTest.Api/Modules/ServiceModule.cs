using Autofac;
using BGCTest.Api.Services.Commands;
using BGCTest.Api.Services.Queries;

namespace BGCTest.Api.Modules
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FoodSaleCommandService>().As<IFoodSaleCommandService>().InstancePerLifetimeScope();
            builder.RegisterType<FoodSaleQueryService>().As<IFoodSaleQueryService>().InstancePerLifetimeScope();
        }
    }
}
