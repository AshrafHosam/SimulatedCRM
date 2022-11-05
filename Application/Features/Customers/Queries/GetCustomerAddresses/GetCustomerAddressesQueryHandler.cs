using Application.Contracts.Repos;
using Application.Contracts.Validators;
using Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Customers.Queries.GetCustomerAddresses
{
    public class GetCustomerAddressesQueryHandler : IRequestHandler<GetCustomerAddressesQuery, APIResponse>
    {
        private readonly ICustomerRepo _customerRepo;
        private readonly ICustomerValidator _customerValidator;
        private readonly ICustomerAddressRepo _customerAddressRepo;
        private readonly ICustomerAddressValidator _customerAddressValidator;

        public GetCustomerAddressesQueryHandler(ICustomerRepo customerRepo, ICustomerValidator customerValidator, ICustomerAddressRepo customerAddressRepo, ICustomerAddressValidator customerAddressValidator)
        {
            _customerRepo = customerRepo;
            _customerValidator = customerValidator;
            _customerAddressRepo = customerAddressRepo;
            _customerAddressValidator = customerAddressValidator;
        }

        public async Task<APIResponse> Handle(GetCustomerAddressesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var customer = await _customerRepo.GetAsync(request.CustomerId);

                var customerValidationResult = _customerValidator.ValidateCustomer(customer);
                if (!customerValidationResult.IsValid)
                    return APIResponse.GetErrorResponseFromValidation(customerValidationResult);

                var customerAddressesEntity = await _customerAddressRepo.GetCustomerAddresses(request.CustomerId);

                var customerAddressesValidationResult = _customerAddressValidator.ValidateCustomerAddresses(customerAddressesEntity);
                if (!customerAddressesValidationResult.IsValid)
                    return APIResponse.GetErrorResponseFromValidation(customerAddressesValidationResult);

                return new APIResponse
                {
                    IsValid = true,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Data = customerAddressesEntity.Select(a => new GetCustomerAddressesQueryResponse
                    {
                        Id = a.Id,
                        Address1 = a.Address1,
                        Address2 = a.Address2,
                        City = a.City,
                        State = a.State,
                        Country = a.Country,
                        CustomerId = customer.Id,
                        BillingAddress = a.BillingAddress,
                        ShippingAddress = a.ShippingAddress,
                        PostalCode = a.PostalCode
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
