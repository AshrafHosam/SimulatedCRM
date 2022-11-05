using Application.Features.SalesOrders.Commands.AddEditSalesOrder;
using Application.Features.SalesOrders.Commands.EditSalesOrderDetail;
using Application.Features.SalesOrders.Queries.GetSalesOrderDetail;
using Application.Features.SalesOrders.Queries.GetSalesOrders;
using Application.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SalesOrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SalesOrderController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("AddEdit")]
        public async Task<ActionResult<BaseResponse>> AddEdit(AddEditSalesOrderCommand command)
        {
            var result = await _mediator.Send(command);
            return StatusCode(result.IntStatusCode, APIResponse.GetResponse(result));
        }

        [HttpGet("GetSalesOrders")]
        public async Task<ActionResult<BaseResponse>> GetSalesOrder()
        {
            var result = await _mediator.Send(new GetSalesOrdersQuery());
            return StatusCode(result.IntStatusCode, APIResponse.GetResponse(result));
        }

        [HttpGet("/Detail/GetSalesOrderDetail")]
        public async Task<ActionResult<BaseResponse>> GetSalesOrderDetail([FromQuery] GetSalesOrderDetailQuery query)
        {
            var result = await _mediator.Send(query);
            return StatusCode(result.IntStatusCode, APIResponse.GetResponse(result));
        }

        [HttpPost("/Detail/Edit")]
        public async Task<ActionResult<BaseResponse>> EditSalesOrderDetail(EditSalesOrderDetailCommand command)
        {
            var result = await _mediator.Send(command);
            return StatusCode(result.IntStatusCode, APIResponse.GetResponse(result));
        }
    }
}
