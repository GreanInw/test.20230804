using BGCTest.Api.DTOs.Requests;
using BGCTest.Api.Services.Commands;
using BGCTest.Api.Services.Results;
using MediatR;

namespace BGCTest.Api.CQRS.Commands
{
    public class CreateFoodSaleCommandHandler : IRequestHandler<CreateFoodSaleRequest, ServiceResult>
    {
        private readonly IFoodSaleCommandService _foodSaleCommandService;

        public CreateFoodSaleCommandHandler(IFoodSaleCommandService foodSaleCommandService) 
            => _foodSaleCommandService = foodSaleCommandService;

        public async Task<ServiceResult> Handle(CreateFoodSaleRequest request, CancellationToken cancellationToken)
            => await _foodSaleCommandService.CreateAsync(request);
    }
}
