using Microsoft.AspNetCore.Mvc;
using Projekt.Models.Payment.Request;
using Projekt.Services;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Projekt.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FinancialController : ControllerBase
    {
        private readonly FinancialService _financialService;
        public FinancialController(FinancialService financialService)
        {
            _financialService = financialService;
        }
        
        [Authorize]
        [HttpPost("productAgreementPayment")]
        public async Task<IActionResult> AddProductAgreementPayment(ProductAgreementPaymentRequest productAgreementPaymentRequest, CancellationToken cancellationToken)
        {
            var newPaymentId = await _financialService.RecordProductAgreementPayment(productAgreementPaymentRequest, cancellationToken);
            return Ok("Successfully added a new product agreement payment, with the id of: " + newPaymentId);
        }
        
        [Authorize]
        [HttpGet("totalActualIncome")]
        public async Task<IActionResult> GetTotalActualIncome([FromQuery] IncomeRequest incomeRequest, [FromQuery] int productId, [FromQuery] string? currency, CancellationToken cancellationToken)
        {
            if (productId == 0)
            {
                var totalActualIncome = await _financialService.CalculateTotalActualIncome(incomeRequest, currency, cancellationToken);
                return Ok(totalActualIncome);
            }
            else
            {
                var productActualIncome = await _financialService.CalculateProductActualIncome(incomeRequest, productId, currency, cancellationToken);
                return Ok(new { Currency = currency ?? "PLN", ProductActualIncome = productActualIncome });
            }
        }
        
        [Authorize]
        [HttpGet("totalForecastIncome")]
        public async Task<IActionResult> GetTotalForecastIncome([FromQuery] IncomeRequest incomeRequest, [FromQuery] int productId, [FromQuery] string? currency, CancellationToken cancellationToken)
        {
            if (productId == 0)
            {
                var totalForecastIncome = await _financialService.CalculateTotalForecastIncome(incomeRequest, currency, cancellationToken);
                return Ok(totalForecastIncome);
            }
            else
            {
                var productForecastIncome = await _financialService.CalculateProductForecastIncome(incomeRequest, productId, currency, cancellationToken);
                return Ok(new { Currency = currency ?? "PLN", ProductForecastIncome = productForecastIncome });
            }
        }
    }
}
