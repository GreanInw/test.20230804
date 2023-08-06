using BGCTest.Api.DTOs.Requests;
using BGCTest.Api.Services.Results;

namespace BGCTest.Api.Services.Commands
{
    public interface IFoodSaleCommandService
    {
        ValueTask<ServiceResult> CreateAsync(CreateFoodSaleRequest request);
        ValueTask<ServiceResult> UpdateAsync(UpdateFoodSaleRequest request);
        ValueTask<ServiceResult> DeleteAsync(DeleteFoodSaleRequest request);
    }
}