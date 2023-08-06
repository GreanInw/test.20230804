using BGCTest.Api.Services.Results;
using MediatR;

namespace BGCTest.Api.DTOs.Requests
{
    public class DeleteFoodSaleRequest : IRequest<ServiceResult>
    {
        public Guid Id { get; set; }
    }
}