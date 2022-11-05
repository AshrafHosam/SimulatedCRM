using Application.Contracts.Repos;
using Application.Contracts.Validators;
using Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SalesOrders.Commands.EditSalesOrderDetail
{
    public class EditSalesOrderDetailCommandHandler : IRequestHandler<EditSalesOrderDetailCommand, APIResponse>
    {
        private readonly ISalesOrderDetailRepo _salesOrderDetailRepo;
        private readonly ISalesOrderDetailValidator _salesOrderDetailValidator;
        private readonly IProductRepo _productRepo;
        private readonly IProductValidator _productValidator;
        public EditSalesOrderDetailCommandHandler(ISalesOrderDetailRepo salesOrderDetailRepo, ISalesOrderDetailValidator salesOrderDetailValidator, IProductRepo productRepo, IProductValidator productValidaotr)
        {
            _salesOrderDetailRepo = salesOrderDetailRepo;
            _salesOrderDetailValidator = salesOrderDetailValidator;
            _productRepo = productRepo;
            _productValidator = productValidaotr;
        }

        public async Task<APIResponse> Handle(EditSalesOrderDetailCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var salesOrderDetail = await _salesOrderDetailRepo.GetAsync(request.SalesOrderLineId);

                var salesOrderDetailValidationReslt = _salesOrderDetailValidator.ValidateSalesOrderDetail(salesOrderDetail);
                if (!salesOrderDetailValidationReslt.IsValid)
                    return APIResponse.GetErrorResponseFromValidation(salesOrderDetailValidationReslt);

                var product = await _productRepo.GetAsync(request.ProductId);

                var productValidationResult = _productValidator.ValidateProduct(product);
                if (!productValidationResult.IsValid)
                    return APIResponse.GetErrorResponseFromValidation(productValidationResult);

                salesOrderDetail.UpdatedDate = DateTime.Now;
                salesOrderDetail.UpdatedBy = "Authorized User";
                salesOrderDetail.ProductId = request.ProductId;
                salesOrderDetail.SalesOrderLineNumber = request.SalesOrderLineNumber;
                salesOrderDetail.LinePrice = request.LinePrice;
                salesOrderDetail.LineOrderedQuantity = request.LineOrderedQuantity;
                salesOrderDetail.LineTaxAmount = request.LineTaxAmount;

                await _salesOrderDetailRepo.UpdateAsync(salesOrderDetail);

                return new APIResponse
                {
                    IsValid = true,
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return APIResponse.GetExceptionResponse(ex);
            }

        }
    }
}
