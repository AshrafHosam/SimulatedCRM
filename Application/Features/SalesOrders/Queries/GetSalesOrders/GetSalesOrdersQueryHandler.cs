using Application.Contracts.Repos;
using Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SalesOrders.Queries.GetSalesOrders
{
    public class GetSalesOrdersQueryHandler : IRequestHandler<GetSalesOrdersQuery, APIResponse>
    {
        private readonly ISalesOrderRepo _salesOrderRepo;

        public GetSalesOrdersQueryHandler(ISalesOrderRepo salesOrderRepo)
        {
            _salesOrderRepo = salesOrderRepo;
        }

        public async Task<APIResponse> Handle(GetSalesOrdersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var salesOrdersEntity = await _salesOrderRepo.GetSalesOrdersAddressesIncluded();
                return new APIResponse
                {
                    IsValid = true,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Data = salesOrdersEntity.Select(a => new GetSalesOrdersQueryResponse
                    {
                        Id = a.Id,
                        CustomerId = a.Id,
                        GrandTotal = a.GrandTotal,
                        SubTotal = a.SubTotal,
                        OrderStatus = a.OrderStatus.ToString(),
                        BillingAddress = a.SalesOrderAddresses.Where(b => b.IsBillingAddress).Select(c => new AddressDTO
                        {
                            Address1 = c.CustomerAddress.Address1,
                            Address2 = c.CustomerAddress.Address2,
                            City = c.CustomerAddress.City,
                            Country = c.CustomerAddress.Country,
                            Id = c.CustomerAddress.Id,
                            PostalCode = c.CustomerAddress.PostalCode,
                            State = c.CustomerAddress.State
                        }).FirstOrDefault(),
                        ShippingAddress = a.SalesOrderAddresses.Where(b => b.IsShippingAddress).Select(c => new AddressDTO
                        {
                            Address1 = c.CustomerAddress.Address1,
                            Address2 = c.CustomerAddress.Address2,
                            City = c.CustomerAddress.City,
                            Country = c.CustomerAddress.Country,
                            Id = c.CustomerAddress.Id,
                            PostalCode = c.CustomerAddress.PostalCode,
                            State = c.CustomerAddress.State
                        }).FirstOrDefault()
                    }).ToList()
                };
            }
            catch (Exception ex)
            {
                return APIResponse.GetExceptionResponse(ex);
            }
        }
    }
}
