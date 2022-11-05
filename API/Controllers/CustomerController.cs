using Application.Features.Customers.Commands.AddEditCustomer;
using Application.Features.Customers.Commands.AddEditCustomerAddress;
using Application.Features.Customers.Queries.GetCustomerAddresses;
using Application.Features.Customers.Queries.GetCustomers;
using Application.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("AddEdit")]
        public async Task<ActionResult<BaseResponse>> AddEditCustomer(AddEditCustomerCommand command)
        {
            var result = await _mediator.Send(command);
            return StatusCode(result.IntStatusCode, APIResponse.GetResponse(result));
        }
        [HttpGet("GetCustomers")]
        public async Task<ActionResult<BaseResponse>> GetCustomers()
        {
            var result = await _mediator.Send(new GetCustomersQuery());
            return StatusCode(result.IntStatusCode, APIResponse.GetResponse(result));
        }

        [HttpPost("Address/AddEdit")]
        public async Task<ActionResult<BaseResponse>> AddEditCustomerAddress(AddEditCustomerAddressCommand command)
        {
            var result = await _mediator.Send(command);
            return StatusCode(result.IntStatusCode, APIResponse.GetResponse(result));
        }
        [HttpGet("Address/GetAddresses")]
        public async Task<ActionResult<BaseResponse>> GetAddresses([FromHeader] int customerId)
        {
            var result = await _mediator.Send(new GetCustomerAddressesQuery
            {
                CustomerId = customerId
            });
            return StatusCode(result.IntStatusCode, APIResponse.GetResponse(result));
        }
    }
}
