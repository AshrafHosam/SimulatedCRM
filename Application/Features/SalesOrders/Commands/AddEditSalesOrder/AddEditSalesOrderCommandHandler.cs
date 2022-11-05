using Application.Contracts.Repos;
using Application.Contracts.Validators;
using Application.Response;
using Domain.Entities;
using MediatR;

namespace Application.Features.SalesOrders.Commands.AddEditSalesOrder
{
    public class AddEditSalesOrderCommandHandler : IRequestHandler<AddEditSalesOrderCommand, APIResponse>
    {
        private readonly ISalesOrderRepo _salesOrderRepo;
        private readonly ISalesOrderDetailRepo _salesOrderDetilRepo;
        private readonly ISalesOrderValidator _salesOrderValidator;
        private readonly ICustomerRepo _customerRepo;
        private readonly ICustomerValidator _customerValidator;
        private readonly ICustomerAddressRepo _customerAddressRepo;
        private readonly ICustomerAddressValidator _customerAddressValidator;

        public AddEditSalesOrderCommandHandler(ISalesOrderRepo salesOrderRepo, ISalesOrderDetailRepo salesOrderDetilRepo, ISalesOrderValidator salesOrderValidator, ICustomerRepo customerRepo, ICustomerValidator customerValidator, ICustomerAddressRepo customerAddressRepo, ICustomerAddressValidator customerAddressValidator)
        {
            _salesOrderRepo = salesOrderRepo;
            _salesOrderDetilRepo = salesOrderDetilRepo;
            _salesOrderValidator = salesOrderValidator;
            _customerRepo = customerRepo;
            _customerValidator = customerValidator;
            _customerAddressRepo = customerAddressRepo;
            _customerAddressValidator = customerAddressValidator;
        }

        public async Task<APIResponse> Handle(AddEditSalesOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var customer = await _customerRepo.GetAsync(request.CustomerId);
                var customerValidationResult = _customerValidator.ValidateCustomer(customer);
                if (!customerValidationResult.IsValid)
                {
                    return APIResponse.GetErrorResponseFromValidation(customerValidationResult);
                }

                if (request.Id.HasValue)
                {
                    var salesOrder = await _salesOrderRepo.GetSalesOrderAddressesIncluded(request.Id.Value);
                    var salesOrderValidationResult = _salesOrderValidator.ValidateSalesOrder(salesOrder);
                    if (!salesOrderValidationResult.IsValid)
                    {
                        return APIResponse.GetErrorResponseFromValidation(salesOrderValidationResult);
                    }

                    salesOrder.OrderStatus = request.OrderStatus;
                    //edit or remove old address billing and shipping
                    if (request.CustomerShippingAddress)
                    {
                        var ship = salesOrder.SalesOrderAddresses.FirstOrDefault(a => a.IsShippingAddress);
                        if (ship != null) salesOrder.SalesOrderAddresses.Remove(ship);

                        salesOrder.SalesOrderAddresses.Add(new SalesOrderAddress
                        {
                            CustomerAddress = await _customerAddressRepo.GetCustomerShippingAddress(request.CustomerId),
                            IsShippingAddress = true
                        });
                    }
                    else if (request.ShippingAddressId.HasValue)
                    {
                        var shippingAddress = await _customerAddressRepo.GetAsync(request.ShippingAddressId.Value);
                        var shippingAddressValidationResult = _customerAddressValidator.ValidateCustomerAddress(shippingAddress);
                        if (!shippingAddressValidationResult.IsValid)
                            return APIResponse.GetErrorResponseFromValidation(shippingAddressValidationResult);

                        salesOrder.SalesOrderAddresses.Add(new SalesOrderAddress
                        {
                            CustomerAddressId = request.ShippingAddressId.Value,
                            IsShippingAddress = true
                        });
                    }

                    if (request.CustomerBillingAddress)
                    {
                        var bill = salesOrder.SalesOrderAddresses.FirstOrDefault(a => a.IsBillingAddress);
                        if (bill != null) salesOrder.SalesOrderAddresses.Remove(bill);

                        salesOrder.SalesOrderAddresses.Add(new SalesOrderAddress
                        {
                            CustomerAddress = await _customerAddressRepo.GetCustomerBillingAddress(request.CustomerId),
                            IsBillingAddress = true
                        });
                    }
                    else if (request.BillingAddressId.HasValue)
                    {
                        var billingAddress = await _customerAddressRepo.GetAsync(request.BillingAddressId.Value);
                        var billingAddressValidationResult = _customerAddressValidator.ValidateCustomerAddress(billingAddress);
                        if (!billingAddressValidationResult.IsValid)
                            return APIResponse.GetErrorResponseFromValidation(billingAddressValidationResult);

                        salesOrder.SalesOrderAddresses.Add(new SalesOrderAddress
                        {
                            CustomerAddressId = request.BillingAddressId.Value,
                            IsBillingAddress = true
                        });
                    }

                    salesOrder.SubTotal = request.SubTotal;
                    salesOrder.GrandTotal = request.GrandTotal;

                    salesOrder.UpdatedBy = "Authorized User";
                    salesOrder.UpdatedDate = DateTime.Now;

                    await _salesOrderRepo.UpdateAsync(salesOrder);

                    return new APIResponse
                    {
                        IsValid = true,
                        StatusCode = System.Net.HttpStatusCode.OK
                    };
                }
                else
                {
                    var shippingAddress = new CustomerAddress();
                    int shippingAddressId = 0;
                    var billingAddress = new CustomerAddress();
                    int billingAddressId = 0;

                    if (request.CustomerShippingAddress)
                        shippingAddress = await _customerAddressRepo.GetCustomerShippingAddress(request.CustomerId);

                    else if (request.ShippingAddressId.HasValue)
                    {
                        shippingAddress = await _customerAddressRepo.GetAsync(request.ShippingAddressId.Value);
                        var shippingAddressValidationResult = _customerAddressValidator.ValidateCustomerAddress(shippingAddress);
                        if (!shippingAddressValidationResult.IsValid)
                            return APIResponse.GetErrorResponseFromValidation(shippingAddressValidationResult);

                        shippingAddressId = request.ShippingAddressId.Value;
                    }

                    if (request.CustomerBillingAddress)
                        billingAddress = await _customerAddressRepo.GetCustomerBillingAddress(request.CustomerId);

                    else if (request.BillingAddressId.HasValue)
                    {
                        billingAddress = await _customerAddressRepo.GetAsync(request.BillingAddressId.Value);
                        var billingAddressValidationResult = _customerAddressValidator.ValidateCustomerAddress(billingAddress);
                        if (!billingAddressValidationResult.IsValid)
                            return APIResponse.GetErrorResponseFromValidation(billingAddressValidationResult);

                        billingAddressId = request.BillingAddressId.Value;
                    }

                    await _salesOrderRepo.AddAsync(new Domain.Entities.SalesOrder
                    {
                        CreatedBy = "Authorized User",
                        CreatedDate = DateTime.Now,
                        CustomerId = request.CustomerId,
                        SubTotal = request.SubTotal,
                        GrandTotal = request.GrandTotal,
                        OrderDate = DateTime.Now,
                        OrderTax = 0.14m,
                        IsDeleted = false,
                        OrderStatus = Domain.Enums.OrderStatusEnum.PendingConfirmation,
                        SalesOrderAddresses = new List<SalesOrderAddress>()
                        {
                            new SalesOrderAddress{
                                CustomerAddressId = billingAddressId == 0 ? billingAddress.Id : billingAddressId,
                                IsBillingAddress= true
                            } ,
                            new SalesOrderAddress
                            {
                                CustomerAddressId = shippingAddressId == 0 ? shippingAddress.Id : shippingAddressId,
                                IsShippingAddress=true
                            }
                        },
                        SalesOrderDetail = new SalesOrderDetail()
                        {
                            CreatedBy = "Authorized User",
                            CreatedDate = DateTime.Now,
                        }
                    });

                    return new APIResponse
                    {
                        IsValid = true,
                        StatusCode = System.Net.HttpStatusCode.Created
                    };
                }
            }
            catch (Exception ex)
            {
                return APIResponse.GetExceptionResponse(ex);
            }
        }
    }
}
