using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projekt.Models.Contract;
using Projekt.Services;

namespace Projekt.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AgreementController : ControllerBase
    {
        private readonly AgreementService _agreementService;
        public AgreementController(AgreementService agreementService)
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
    }
}