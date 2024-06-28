using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projekt.Models.Client.Request;
using Projekt.Services;

namespace Projekt.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerManagementService _customerService;

        public CustomerController(CustomerManagementService customerService)
        {
            _customerService = customerService;
        }
        
        [Authorize]
        [HttpPost("company")]
        public async Task<IActionResult> AddCompanyClient(CompanyAddRequest companyAddRequest, CancellationToken cancellationToken)
        {
            var newCompanyId = await _customerService.RegisterCorporateClient(companyAddRequest, cancellationToken);
            return Ok("Successfully added new company client, with the id of: " + newCompanyId);
        }
        [Authorize]
        [HttpPost("individual")]
        public async Task<IActionResult> AddIndividualClient(IndividualAddRequest individualAddRequest, CancellationToken cancellationToken)
        {
            var newIndividualId = await _customerService.RegisterIndividualClient(individualAddRequest, cancellationToken);
            return Ok("Successfully added new individual client, with the id of: " + newIndividualId);
        }

        [Authorize]
        [HttpDelete("delete/{clientId:int}")]
        public async Task<IActionResult> DeleteClient(int clientId, CancellationToken cancellationToken)
        {
            await _customerService.RemoveCustomer(clientId, cancellationToken);
            return Ok("The client with id: " + clientId + ", has been marked as deprecated");
        }

        [Authorize]
        [HttpPut("company/{clientId:int}/edit")]
        public async Task<IActionResult> EditCompanyClient(int clientId, CompanyModifyRequest companyUpdateRequest, CancellationToken cancellationToken)
        {
            await _customerService.UpdateCorporateClient(clientId, companyUpdateRequest, cancellationToken);
            return Ok("The company client with id: " + clientId + ", has been updated");
        }
        
        [Authorize]
        [HttpPut("individual/{clientId:int}/edit")]
        public async Task<IActionResult> EditIndividualClient(int clientId, IndividualModifyRequest individualUpdateRequest, CancellationToken cancellationToken)
        {
            await _customerService.UpdateIndividualClient(clientId, individualUpdateRequest, cancellationToken);
            return Ok("The individual client with id: " + clientId + ", has been updated");
        }
    }
}
