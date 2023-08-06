using BGCTest.Api.UnitOfWorks;

namespace BGCTest.Api.Services.Bases
{
    public abstract class CommandServiceBase
    {
        protected CommandServiceBase(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        protected IUnitOfWork UnitOfWork { get; }

        protected async Task CommitAsync() => await UnitOfWork.CommitAsync();
    }
}
