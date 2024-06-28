using Projekt.ApiClients.Interfaces;
using System.Text.Json;
using Projekt.Models.Currency;

namespace Projekt.ApiClients
{
    public class ExchangeRateClient : IExchangeRateClient
    {
        private readonly HttpClient _httpClient;
        private const string ApiEndpoint = "https://api.nbp.pl/api/exchangerates/tables/a/?format=json";

        public ExchangeRateClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<decimal> ConvertPlnToTargetCurrency(decimal amount, string targetCurrency, CancellationToken cancellationToken)
        {
            var exchangeRates = await FetchExchangeRatesAsync(cancellationToken).ConfigureAwait(false);

            var exchangeRate = exchangeRates
                .FirstOrDefault(rate => rate.Code == targetCurrency)?.Rate;

            if (exchangeRate == null || exchangeRate == 0)
            {
                throw new ArgumentException($"Exchange rate for '{targetCurrency}' not found");
            }

            return Math.Round(amount / exchangeRate.Value, 2);
        }

        private async Task<List<ExchangeRate>> FetchExchangeRatesAsync(CancellationToken cancellationToken)
        {
            try
            {
                var response = await _httpClient.GetAsync(ApiEndpoint, cancellationToken).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();

                var jsonResponse = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
                var jsonElement = JsonDocument.Parse(jsonResponse).RootElement;

                return jsonElement
                    .EnumerateArray()
                    .SelectMany(table => table.GetProperty("rates").EnumerateArray())
                    .Select(rate => new ExchangeRate
                    {
                        Code = rate.GetProperty("code").GetString(),
                        Rate = rate.GetProperty("mid").GetDecimal()
                    })
                    .ToList();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Error fetching exchange rates from API", ex);
            }
        }
    }
}
