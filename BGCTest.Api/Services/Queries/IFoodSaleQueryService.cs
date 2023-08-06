using BGCTest.Api.DTOs.Requests;
using BGCTest.Api.DTOs.Responses;
using BGCTest.Api.Services.Results;

namespace BGCTest.Api.Services.Queries
{
    public interface IFoodSaleQueryService
    {
        ValueTask<ServiceResultPaging<FoodSaleResponse>> GetAllAsync(GetAllFoodSaleRequest request);
    }
}
