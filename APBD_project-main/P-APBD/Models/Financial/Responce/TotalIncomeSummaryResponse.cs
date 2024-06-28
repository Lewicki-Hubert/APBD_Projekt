namespace Projekt.Models.Financial.Responses
{
    public class TotalIncomeSummaryResponse
    {
        public string? CurrencyCode { get; set; }
        public List<IncomeByProductResponse> ProductIncomes { get; set; }
        public decimal OverallIncome { get; set; }
    }
}