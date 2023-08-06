using BGCTest.Api.Controllers.Bases;
using BGCTest.Api.DTOs.Requests;
using BGCTest.Api.DTOs.Responses;
using BGCTest.Api.Services.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BGCTest.Api.Controllers
{
    [ApiController]
    [Route("api/foodsales")]
    public class FoodSaleController : InternalControllerBase
    {
        public FoodSaleController(IMediator mediator) : base(mediator) { }

        [HttpPost("all")]
        public async ValueTask<IActionResult> Get([FromBody] GetAllFoodSaleRequest request)
            => await ReturnResponseWithPagingAsync<GetAllFoodSaleRequest, FoodSaleResponse>(request);

        [HttpPost]
        public async ValueTask<IActionResult> Post([FromBody] CreateFoodSaleRequest request)
            => await ReturnResponseAsync(request);

        [HttpPut]
        public async ValueTask<IActionResult> Put([FromBody] UpdateFoodSaleRequest request)
            => await ReturnResponseAsync(request);

        [HttpDelete]
        public async ValueTask<IActionResult> Delete([FromQuery] DeleteFoodSaleRequest request)
            => await ReturnResponseAsync(request);

        [HttpGet("sort")]
        public IActionResult SortColumns()
            => Ok(new ServiceResult { IsSuccess = true, Result = new FoodSaleSortColumnResponse() });
    }
}
