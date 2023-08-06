using BGCTest.Api.Tables;
using BGCTest.Api.Tables.Configurations;
using Microsoft.EntityFrameworkCore;

namespace BGCTest.Api.DbContexts
{
    public class FoodDbContext : DbContext, IFoodDbContext
    {
        public FoodDbContext(DbContextOptions<FoodDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AuditableConfiguration<FoodSale>());
        }
    }
}