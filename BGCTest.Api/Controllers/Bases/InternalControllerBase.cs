using Azure;
using BGCTest.Api.Services.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BGCTest.Api.Controllers.Bases
{
    public abstract class InternalControllerBase : ControllerBase
    {
        protected InternalControllerBase(IMediator mediator)
        {
            Mediator = mediator;
        }

        protected IMediator Mediator { get; }

        protected async ValueTask<IActionResult> ReturnResponseAsync<TRequest, TResponse>(TRequest request)
            where TRequest : IRequest<ServiceResult<TResponse>>
            => ModelState.IsValid ? Ok(await Mediator.Send(request)) : ReturnInvalidModelState();

        protected async Task<IActionResult> ReturnResponseAsync<TRequest>(TRequest request)
            where TRequest : IRequest<ServiceResult>
            => ModelState.IsValid ? Ok(await Mediator.Send(request)) : ReturnInvalidModelState();

        protected async ValueTask<IActionResult> ReturnResponseWithPagingAsync<TRequest, TResponse>(TRequest request)
            where TRequest : IRequest<ServiceResultPaging<TResponse>>
            => ModelState.IsValid ? Ok(await Mediator.Send(request)) : ReturnInvalidModelState();

        protected IActionResult ReturnInvalidModelState()
          => BadRequest(new ServiceResult { Errors = GetErrorMessages() });

        protected IEnumerable<string> GetErrorMessages()
            => ModelState.Values.SelectMany(s => s.Errors).Select(s => s.ErrorMessage);
    }
}
