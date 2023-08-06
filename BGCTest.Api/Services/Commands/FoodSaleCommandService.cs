using BGCTest.Api.DTOs.Requests;
using BGCTest.Api.Repositories;
using BGCTest.Api.Services.Bases;
using BGCTest.Api.Services.Results;
using BGCTest.Api.Tables;
using BGCTest.Api.UnitOfWorks;

namespace BGCTest.Api.Services.Commands
{
    public class FoodSaleCommandService : CommandServiceBase, IFoodSaleCommandService
    {
        private readonly IFoodSaleRepository _foodSaleRepository;

        public FoodSaleCommandService(IUnitOfWork unitOfWork, IFoodSaleRepository foodSaleRepository)
            : base(unitOfWork)
        {
            _foodSaleRepository = foodSaleRepository;
        }

        public async ValueTask<ServiceResult> CreateAsync(CreateFoodSaleRequest request)
            => await CreateOrUpdateAsync(request);

        public async ValueTask<ServiceResult> UpdateAsync(UpdateFoodSaleRequest request)
            => await CreateOrUpdateAsync(request, request);

        public async ValueTask<ServiceResult> DeleteAsync(DeleteFoodSaleRequest request)
        {
            var entity = await _foodSaleRepository.GetByAsync(request.Id);
            if (entity is null)
            {
                return new ServiceResult("Cannot delete, Data not found.");
            }

            _foodSaleRepository.Delete(entity);
            await CommitAsync();
            return new ServiceResult(true);
        }

        protected async ValueTask<ServiceResult> CreateOrUpdateAsync(CreateFoodSaleRequest createRequest
            , UpdateFoodSaleRequest updateRequest = null)
        {
            bool isNew = updateRequest is null;
            var newOrUpdateEntity = isNew ? new FoodSale()
                : await _foodSaleRepository.GetByAsync(updateRequest.Id);

            if (!isNew && newOrUpdateEntity is null)//Case edit data not found.
            {
                return new ServiceResult("Cannot update, Data not found.");
            }

            PassingData(isNew, newOrUpdateEntity, isNew ? createRequest : updateRequest);

            if (isNew)
            {
                await _foodSaleRepository.AddAsync(newOrUpdateEntity);
            }
            else
            {
                _foodSaleRepository.Edit(newOrUpdateEntity);
            }

            await CommitAsync();
            return new ServiceResult(true);
        }

        protected void PassingData(bool isNew, FoodSale entity, CreateFoodSaleRequest request)
        {
            if (isNew)
            {
                entity.Id = Guid.NewGuid();
            }

            entity.OrderDate = request.OrderDate.Date;
            entity.Region = request.Region;
            entity.City = request.City;
            entity.Category = request.Category;
            entity.Product = request.Product;
            entity.Quantity = request.Quantity;
            entity.UnitPrice = request.UnitPrice;
            entity.TotalPrice = request.Quantity * request.UnitPrice;
        }
    }
}