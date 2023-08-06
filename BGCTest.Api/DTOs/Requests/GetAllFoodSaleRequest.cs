using BGCTest.Api.DTOs.Bases;
using BGCTest.Api.DTOs.Responses;
using BGCTest.Api.Enums;
using BGCTest.Api.Services.Results;
using BGCTest.Api.Tables;
using MediatR;

namespace BGCTest.Api.DTOs.Requests
{
    public class GetAllFoodSaleRequest : PagingData, IRequest<ServiceResultPaging<FoodSaleResponse>>
    {
        public FilterDate OrderDate { get; set; } = new FilterDate();

        public string Region { get; set; }
        public string City { get; set; }
        public string Category { get; set; }
        public string Product { get; set; }

        public FilterLength<int> Quantity { get; set; } = new FilterLength<int>();
        public FilterLength<decimal> UnitPrice { get; set; } = new FilterLength<decimal>();
        public FilterLength<decimal> TotalPrice { get; set; } = new FilterLength<decimal>();

        public string SortColumnName { get; set; } = nameof(FoodSale.OrderDate);
        public SortColumnType SortType { get; set; } = SortColumnType.Asc;

        public class FilterDate
        {
            public DateTime? FromDate { get; set; }
            public DateTime? ToDate { get; set; }
        }

        public class FilterLength<T> where T : struct
        {
            public T? Min { get; set; }
            public T? Max { get; set; }
        }
    }
}
