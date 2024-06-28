using Microsoft.AspNetCore.Mvc;
using Projekt.Entities;
using Projekt.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Projekt.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly DiscountService _discountService;

        public DiscountController(DiscountService discountService)
        {
            _discountService = discountService;
        }

        [HttpGet]
        public async Task<IEnumerable<Promotion>> GetAllDiscounts(CancellationToken cancellationToken)
        {
            return await _discountService.GetAllDiscountsAsync(cancellationToken);
        }

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

        [HttpPost]
        public async Task<ActionResult<int>> AddDiscount(Promotion discount, CancellationToken cancellationToken)
        {
            var discountId = await _discountService.AddDiscountAsync(discount, cancellationToken);
            return CreatedAtAction(nameof(GetDiscountById), new { id = discountId }, discountId);
        }

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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiscount(int id, CancellationToken cancellationToken)
        {
            await _discountService.DeleteDiscountAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
