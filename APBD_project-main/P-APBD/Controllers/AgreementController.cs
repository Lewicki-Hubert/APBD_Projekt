using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projekt.Models.Agreement;
using Projekt.Services;

namespace Projekt.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AgreementController : ControllerBase
    {
        private readonly IAgreementService _agreementService;
        public AgreementController(IAgreementService agreementService)
        {
            _agreementService = agreementService;
        }

        [Authorize]
        [HttpPost("productAgreement")]
        public async Task<IActionResult> AddProductAgreement(ProductAgreementAddRequest productAgreementAddRequest, CancellationToken cancellationToken)
        {
            var newAgreementId = await _agreementService.RegisterProductAgreement(productAgreementAddRequest, cancellationToken);
            return Ok("Successfully added a new product agreement, with the id of: " + newAgreementId);
        }

        [Authorize]
        [HttpPut("productAgreement/{agreementId}")]
        public async Task<IActionResult> UpdateProductAgreement(int agreementId, ProductAgreementAddRequest productAgreementAddRequest, CancellationToken cancellationToken)
        {
            await _agreementService.UpdateProductAgreement(agreementId, productAgreementAddRequest, cancellationToken);
            return Ok("Successfully updated the product agreement with id: " + agreementId);
        }

        [Authorize]
        [HttpGet("productAgreement/{agreementId}/isActive")]
        public async Task<IActionResult> IsAgreementActive(int agreementId, CancellationToken cancellationToken)
        {
            var isActive = await _agreementService.IsAgreementActive(agreementId, cancellationToken);
            return Ok(new { AgreementId = agreementId, IsActive = isActive });
        }
    }
}