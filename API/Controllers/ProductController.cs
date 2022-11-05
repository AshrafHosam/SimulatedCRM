using Application.Features.Products.Commands.AddEditProduct;
using Application.Features.Products.Queries.GetProductsQuery;
using Application.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("AddEdit")]
        public async Task<ActionResult<BaseResponse>> AddEditProduct(AddEditProductCommand command)
        {
            var result = await _mediator.Send(command);
            return StatusCode(result.IntStatusCode, APIResponse.GetResponse(result));
        }

        [HttpGet("GetProducts")]
        public async Task<ActionResult<BaseResponse>> GetProducts()
        {
            var result = await _mediator.Send(new GetProductsQuery());
            return StatusCode(result.IntStatusCode, APIResponse.GetResponse(result));
        }
    }
}
