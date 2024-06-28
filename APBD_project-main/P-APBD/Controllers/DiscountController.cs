using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projekt.Models.Entities;
using Projekt.Services;

namespace Projekt.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountService _discountService;

        public DiscountController(IDiscountService discountService)
        {
            _discountService = discountService;
        }
        
        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<Promotion>> GetAllDiscounts(CancellationToken cancellationToken)
        {
            return await _discountService.GetAllDiscountsAsync(cancellationToken);
        }
        
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Promotion>> GetDiscountById(int id, CancellationToken cancellationToken)
        {
            var discount = await _discountService.GetDiscountByIdAsync(id, cancellationToken);
            if (discount == null)
            {
                return NotFound();
            }
            return discount;
        }
        
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<int>> AddDiscount(Promotion discount, CancellationToken cancellationToken)
        {
            var discountId = await _discountService.AddDiscountAsync(discount, cancellationToken);
            return CreatedAtAction(nameof(GetDiscountById), new { id = discountId }, discountId);
        }
        
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDiscount(int id, Promotion discount, CancellationToken cancellationToken)
        {
            if (id != discount.PromotionId)
            {
                return BadRequest();
            }

            await _discountService.UpdateDiscountAsync(discount, cancellationToken);
            return NoContent();
        }
        
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiscount(int id, CancellationToken cancellationToken)
        {
            await _discountService.DeleteDiscountAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
