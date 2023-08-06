using BGCTest.Api.DTOs.Requests;
using BGCTest.Api.DTOs.Responses;
using BGCTest.Api.Enums;
using BGCTest.Api.Helpers;
using BGCTest.Api.Repositories;
using BGCTest.Api.Services.Results;
using BGCTest.Api.Tables;
using Microsoft.EntityFrameworkCore;

namespace BGCTest.Api.Services.Queries
{
    public class FoodSaleQueryService : IFoodSaleQueryService
    {
        private readonly IFoodSaleRepository _foodSaleRepository;

        public FoodSaleQueryService(IFoodSaleRepository foodSaleRepository)
        {
            _foodSaleRepository = foodSaleRepository;
        }

        public async ValueTask<ServiceResultPaging<FoodSaleResponse>> GetAllAsync(GetAllFoodSaleRequest request)
        {
            //Get query and order by data.
            var query = OrderColumn(GetQuery(request), request);
            var entities = await query.Paging(request.Limit, request.PageNumber).ToListAsync();

            return new ServiceResultPaging<FoodSaleResponse>
            {
                IsSuccess = true,
                Limit = request.Limit,
                PageNumber = CommonHelper.GetNextPageNumber(entities.Count, request.Limit, request.PageNumber),
                Result = entities.Select(s => new FoodSaleResponse
                {
                    Id = s.Id,
                    Category = s.Category,
                    City = s.City,
                    OrderDate = s.OrderDate,
                    Product = s.Product,
                    Quantity = s.Quantity,
                    Region = s.Region,
                    TotalPrice = s.TotalPrice,
                    UnitPrice = s.UnitPrice
                })
            };
        }

        private IQueryable<FoodSale> GetQuery(GetAllFoodSaleRequest request)
        {
            var query = _foodSaleRepository.All.AsNoTracking();

            if (!request.Region.IsEmpty())
            {
                query = query.Where(w => w.Region.Contains(request.Region.Trim()));
            }

            if (!request.City.IsEmpty())
            {
                query = query.Where(w => w.City.Contains(request.City.Trim()));
            }

            if (!request.Category.IsEmpty())
            {
                query = query.Where(w => w.Category.Contains(request.Category.Trim()));
            }

            if (!request.Product.IsEmpty())
            {
                query = query.Where(w => w.Product.Contains(request.Product.Trim()));
            }

            query = FilterOrderDate(query, request.OrderDate);
            query = FilterLength(query, FilterLengthColumn.Quantity, request.Quantity);
            query = FilterLength(query, FilterLengthColumn.UnitPrice, request.UnitPrice);
            query = FilterLength(query, FilterLengthColumn.TotalPrice, request.TotalPrice);

            return query;
        }

        private IQueryable<FoodSale> FilterOrderDate(IQueryable<FoodSale> query, GetAllFoodSaleRequest.FilterDate filterDate)
        {
            if (filterDate is null || !filterDate.FromDate.HasValue || !filterDate.ToDate.HasValue)
            {
                return query;
            }

            return query.Where(w => w.OrderDate.Date >= filterDate.FromDate.Value.Date
                && w.OrderDate.Date <= filterDate.ToDate.Value.Date);
        }

        private IQueryable<FoodSale> FilterLength<T>(IQueryable<FoodSale> query, FilterLengthColumn column
            , GetAllFoodSaleRequest.FilterLength<T> filterLength)
            where T : struct
        {
            if (filterLength is null || !filterLength.Min.HasValue || !filterLength.Max.HasValue)
            {
                return query;
            }

            switch (column)
            {
                case FilterLengthColumn.Quantity:
                    int minQty = (int)Convert.ChangeType(filterLength.Min.Value, typeof(int));
                    int maxQty = (int)Convert.ChangeType(filterLength.Max.Value, typeof(int));

                    return query.Where(w => w.Quantity >= minQty && w.Quantity <= maxQty);
                case FilterLengthColumn.UnitPrice:
                case FilterLengthColumn.TotalPrice:
                    decimal minDecValue = (decimal)Convert.ChangeType(filterLength.Min.Value, typeof(decimal));
                    decimal maxDecValue = (decimal)Convert.ChangeType(filterLength.Max.Value, typeof(decimal));

                    if (column == FilterLengthColumn.UnitPrice)
                    {
                        query = query.Where(w => w.UnitPrice >= minDecValue && w.UnitPrice <= maxDecValue);
                    }
                    else if (column == FilterLengthColumn.TotalPrice)
                    {
                        query = query.Where(w => w.TotalPrice >= minDecValue && w.TotalPrice <= maxDecValue);
                    }

                    return query;
                default:
                    return query;
            }
        }

        private IQueryable<FoodSale> OrderColumn(IQueryable<FoodSale> query, GetAllFoodSaleRequest request)
        {
            if (request.SortColumnName.IsEmpty())
            {
                request.SortColumnName = nameof(FoodSale.OrderDate);
            }

            switch (request.SortColumnName)
            {
                case nameof(FoodSale.OrderDate):
                    query = request.SortType == SortColumnType.Asc
                        ? query.OrderBy(s => s.OrderDate)
                        : query.OrderByDescending(s => s.OrderDate);
                    break;
                case nameof(FoodSale.Region):
                    query = request.SortType == SortColumnType.Asc
                        ? query.OrderBy(s => s.Region)
                        : query.OrderByDescending(s => s.Region);
                    break;
                case nameof(FoodSale.City):
                    query = request.SortType == SortColumnType.Asc
                        ? query.OrderBy(s => s.City)
                        : query.OrderByDescending(s => s.City);
                    break;
                case nameof(FoodSale.Category):
                    query = request.SortType == SortColumnType.Asc
                        ? query.OrderBy(s => s.Category)
                        : query.OrderByDescending(s => s.Category);
                    break;
                case nameof(FoodSale.Product):
                    query = request.SortType == SortColumnType.Asc
                        ? query.OrderBy(s => s.Product)
                        : query.OrderByDescending(s => s.Product);
                    break;
                case nameof(FoodSale.Quantity):
                    query = request.SortType == SortColumnType.Asc
                        ? query.OrderBy(s => s.Quantity)
                        : query.OrderByDescending(s => s.Quantity);
                    break;
                case nameof(FoodSale.UnitPrice):
                    query = request.SortType == SortColumnType.Asc
                        ? query.OrderBy(s => s.UnitPrice)
                        : query.OrderByDescending(s => s.UnitPrice);
                    break;
                case nameof(FoodSale.TotalPrice):
                    query = request.SortType == SortColumnType.Asc
                        ? query.OrderBy(s => s.TotalPrice)
                        : query.OrderByDescending(s => s.TotalPrice);
                    break;
                default:
                    break;
            }

            return query;
        }

        internal enum FilterLengthColumn
        {
            Quantity, UnitPrice, TotalPrice
        }
    }
}