using BGCTest.Api.DbContexts;
using BGCTest.Api.Repositories.Bases;
using BGCTest.Api.Tables;

namespace BGCTest.Api.Repositories
{
    public class FoodSaleRepository : Repository<IFoodDbContext, FoodSale>, IFoodSaleRepository
    {
        public FoodSaleRepository(IFoodDbContext dbContext) : base(dbContext) { }
    }
}