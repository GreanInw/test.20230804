namespace BGCTest.Api.DTOs.Bases
{
    public interface IPaging
    {
        int Limit { get; set; }
        int PageNumber { get; set; }
    }
}