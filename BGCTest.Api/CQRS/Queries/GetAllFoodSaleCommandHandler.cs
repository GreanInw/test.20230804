using BGCTest.Api.DTOs.Requests;
using BGCTest.Api.DTOs.Responses;
using BGCTest.Api.Services.Queries;
using BGCTest.Api.Services.Results;
using MediatR;

namespace BGCTest.Api.CQRS.Queries
{
    public class GetAllFoodSaleCommandHandler : IRequestHandler<GetAllFoodSaleRequest, ServiceResultPaging<FoodSaleResponse>>
    {
        private readonly IFoodSaleQueryService _foodSaleQueryService;

        public GetAllFoodSaleCommandHandler(IFoodSaleQueryService foodSaleQueryService)
            => _foodSaleQueryService = foodSaleQueryService;

        public async Task<ServiceResultPaging<FoodSaleResponse>> Handle(GetAllFoodSaleRequest request, CancellationToken cancellationToken)
            => await _foodSaleQueryService.GetAllAsync(request);
    }
}
