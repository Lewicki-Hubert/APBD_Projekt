namespace Projekt.ApiClients.Interfaces
{
    public interface IExchangeRateClient
    {
        Task<decimal> ConvertPlnToTargetCurrency(decimal amount, string targetCurrency, CancellationToken cancellationToken);
    }
}