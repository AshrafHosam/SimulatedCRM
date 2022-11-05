using Application.Contracts.Repos;
using Application.Contracts.Validators;
using Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SalesOrders.Queries.GetSalesOrderDetail
{
    public class GetSalesOrderDetailQueryHandler : IRequestHandler<GetSalesOrderDetailQuery, APIResponse>
    {
        private readonly ISalesOrderDetailRepo _salesOrderDetailRepo;
        private readonly ISalesOrderValidator _SalesOrderValidator;
        private readonly ISalesOrderRepo _salesOrderRepo;
        public GetSalesOrderDetailQueryHandler(ISalesOrderDetailRepo salesOrderDetailRepo, ISalesOrderValidator salesOrderValidator, ISalesOrderRepo salesOrderRepo)
        {
            _salesOrderDetailRepo = salesOrderDetailRepo;
            _SalesOrderValidator = salesOrderValidator;
            _salesOrderRepo = salesOrderRepo;
        }

        public GetSalesOrderDetailQueryHandler(ISalesOrderValidator salesOrderValidator)
        {
            _SalesOrderValidator = salesOrderValidator;
        }

        public async Task<APIResponse> Handle(GetSalesOrderDetailQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var salesOrder = await _salesOrderRepo.GetAsync(request.SalesOrderId);

                var salesOrderValidationResult = _SalesOrderValidator.ValidateSalesOrder(salesOrder);
                if (!salesOrderValidationResult.IsValid)
                    return APIResponse.GetErrorResponseFromValidation(salesOrderValidationResult);

                var salesOrderDetail = await _salesOrderDetailRepo.GetSalesOrderDetailProductIncluded(salesOrder.SalesOrderDetailId);

                return new APIResponse
                {
                    IsValid = true,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Data = new GetSalesOrderDetailQueryResponse
                    {
                        SalesOrderLineId = salesOrderDetail.SalesOrderLineId,
                        LineOrderedQuantity = salesOrderDetail.LineOrderedQuantity,
                        LinePrice = salesOrderDetail.LinePrice,
                        LineTaxAmount = salesOrderDetail.LineTaxAmount,
                        ProductId = salesOrderDetail?.ProductId ?? 0,
                        ProductName = salesOrderDetail?.Product?.Name ?? string.Empty,
                        SalesOrderLineNumber = salesOrderDetail.SalesOrderLineId,
                        LineTotal = salesOrderDetail.LineTotal
                    }
                };
            }
            catch (Exception ex)
            {
                return APIResponse.GetExceptionResponse(ex);
            }
        }
    }
}
