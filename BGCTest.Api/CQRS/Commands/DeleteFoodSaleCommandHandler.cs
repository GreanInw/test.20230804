using BGCTest.Api.DTOs.Requests;
using BGCTest.Api.Services.Commands;
using BGCTest.Api.Services.Results;
using MediatR;

namespace BGCTest.Api.CQRS.Commands
{
    public class DeleteFoodSaleCommandHandler : IRequestHandler<DeleteFoodSaleRequest, ServiceResult>
    {
        private readonly IFoodSaleCommandService _foodSaleCommandService;

        public DeleteFoodSaleCommandHandler(IFoodSaleCommandService foodSaleCommandService) 
            => _foodSaleCommandService = foodSaleCommandService;

        public async Task<ServiceResult> Handle(DeleteFoodSaleRequest request, CancellationToken cancellationToken)
            => await _foodSaleCommandService.DeleteAsync(request);
    }
}
