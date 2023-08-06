using BGCTest.Api.DTOs.Requests;
using BGCTest.Api.Services.Commands;
using BGCTest.Api.Services.Results;
using MediatR;

namespace BGCTest.Api.CQRS.Commands
{
    public class UpdateFoodSaleCommandHandler : IRequestHandler<UpdateFoodSaleRequest, ServiceResult>
    {
        private readonly IFoodSaleCommandService _foodSaleCommandService;

        public UpdateFoodSaleCommandHandler(IFoodSaleCommandService foodSaleCommandService) 
            => _foodSaleCommandService = foodSaleCommandService;

        public async Task<ServiceResult> Handle(UpdateFoodSaleRequest request, CancellationToken cancellationToken)
            => await _foodSaleCommandService.UpdateAsync(request);
    }
}
