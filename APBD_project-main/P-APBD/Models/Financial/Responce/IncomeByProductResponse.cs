namespace Projekt.Models.Financial.Responses
{
    public class IncomeByProductResponse
    {
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal TotalIncome { get; set; }
    }
}