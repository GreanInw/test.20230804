using BGCTest.Api.Enums;
using BGCTest.Api.Tables;

namespace BGCTest.Api.DTOs.Responses
{
    public class FoodSaleSortColumnResponse
    {
        internal static IEnumerable<string> ExcludeColumns
            => new[] { "Id", "CreatedDate", "CreatedBy", "ModifiedDate", "ModifiedBy" };

        public IEnumerable<string> SortTypes => Enum.GetNames<SortColumnType>();
        public IEnumerable<string> SortColumns
           => typeof(FoodSale).GetProperties().Select(s => s.Name).Where(w => !ExcludeColumns.Contains(w));
    }
}
