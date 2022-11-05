using Application.Contracts.Repos;
using Application.Contracts.Validators;
using Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Customers.Commands.AddEditCustomerAddress
{
    public class AddEditCustomerAddressCommandHandler : IRequestHandler<AddEditCustomerAddressCommand, APIResponse>
    {
        private readonly ICustomerRepo _customerRepo;
        private readonly ICustomerAddressRepo _customerAddressRepo;
        private readonly ICustomerValidator _customerValidator;
        private readonly ICustomerAddressValidator _customerAddressValidator;
        public AddEditCustomerAddressCommandHandler(ICustomerRepo customerRepo, ICustomerAddressRepo customerAddressRepo, ICustomerValidator customerValidator, ICustomerAddressValidator customerAddressValidator)
        {
            _customerRepo = customerRepo;
            _customerAddressRepo = customerAddressRepo;
            _customerValidator = customerValidator;
            _customerAddressValidator = customerAddressValidator;
        }

        public async Task<APIResponse> Handle(AddEditCustomerAddressCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var customer = await _customerRepo.GetAsync(request.CustomerId);
                var validationResult = _customerValidator.ValidateCustomer(customer);
                if (!validationResult.IsValid)
                {
                    return APIResponse.GetErrorResponseFromValidation(validationResult);
                }

                if (request.Id.HasValue)
                {
                    var customerAddress = await _customerAddressRepo.GetAsync(request.Id.Value);
                    var addressValidationResult = _customerAddressValidator.ValidateCustomerAddress(customerAddress);
                    if (!addressValidationResult.IsValid)
                    {
                        return APIResponse.GetErrorResponseFromValidation(addressValidationResult);
                    }

                    customerAddress.Address1 = request.Address1;
                    customerAddress.Address2 = request.Address2;
                    customerAddress.BillingAddress = request.BillingAddress;
                    customerAddress.ShippingAddress = request.ShippingAddress;
                    customerAddress.City = request.City;
                    customerAddress.Country = request.Country;
                    customerAddress.State = request.State;
                    customerAddress.PostalCode = request.PostalCode;
                    customerAddress.IsDeleted = false;
                    customerAddress.UpdatedBy = "Authorized User";
                    customerAddress.UpdatedDate = DateTime.Now;

                    await _customerAddressRepo.UpdateAsync(customerAddress);

                    return new APIResponse
                    {
                        IsValid = true,
                        StatusCode = System.Net.HttpStatusCode.OK,
                    };
                }
                else
                {
                    await _customerAddressRepo.AddAsync(new Domain.Entities.CustomerAddress
                    {
                        Address1 = request.Address1,
                        Address2 = request.Address2,
                        BillingAddress = request.BillingAddress,
                        ShippingAddress = request.ShippingAddress,
                        City = request.City,
                        Country = request.Country,
                        State = request.State,
                        PostalCode = request.PostalCode,
                        IsDeleted = false,
                        CreatedBy = "Autorized User",
                        CreatedDate = DateTime.Now,
                        CustomerId = request.CustomerId
                    });

                    return new APIResponse
                    {
                        IsValid = true,
                        StatusCode = System.Net.HttpStatusCode.Created,
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
