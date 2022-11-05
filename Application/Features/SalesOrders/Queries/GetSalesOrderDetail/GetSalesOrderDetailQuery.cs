using Application.Response;
using MediatR;

namespace Application.Features.SalesOrders.Queries.GetSalesOrderDetail
{
    public class GetSalesOrderDetailQuery : IRequest<APIResponse>
    {
        public int SalesOrderId { get; set; }
    }
}
