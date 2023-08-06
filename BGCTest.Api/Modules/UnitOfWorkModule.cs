using Autofac;
using BGCTest.Api.DbContexts;
using BGCTest.Api.UnitOfWorks;

namespace BGCTest.Api.Modules
{
    public class UnitOfWorkModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(com =>
            {
                return new UnitOfWork<IFoodDbContext>(com.Resolve<IFoodDbContext>());
            }).As<IUnitOfWork>().AsSelf().InstancePerLifetimeScope();
        }
    }
}