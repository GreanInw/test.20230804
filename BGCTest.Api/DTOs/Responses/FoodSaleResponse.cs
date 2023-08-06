namespace BGCTest.Api.DTOs.Responses
{
    public class FoodSaleResponse
    {
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string Category { get; set; }
        public string Product { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
