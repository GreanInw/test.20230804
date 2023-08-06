using BGCTest.Api.Services.Results;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BGCTest.Api.DTOs.Requests
{
    public class CreateFoodSaleRequest : IRequest<ServiceResult>
    {
        public DateTime OrderDate { get; set; }

        [Required]
        public string Region { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}