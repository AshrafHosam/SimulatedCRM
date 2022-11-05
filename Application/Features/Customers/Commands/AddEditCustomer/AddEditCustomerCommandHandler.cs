using Application.Contracts.Repos;
using Application.Contracts.Validators;
using Application.Response;
using Domain.Entities;
using MediatR;

namespace Application.Features.Customers.Commands.AddEditCustomer
{
    public class AddEditCustomerCommandHandler : IRequestHandler<AddEditCustomerCommand, APIResponse>
    {
        private readonly ICustomerRepo _customerRepo;
        private readonly ICustomerValidator _customerValidator;
        public AddEditCustomerCommandHandler(ICustomerRepo customerRepo, ICustomerValidator customerValidator)
        {
            _customerRepo = customerRepo;
            _customerValidator = customerValidator;
        }

        public async Task<APIResponse> Handle(AddEditCustomerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Id.HasValue)
                {
                    var customer = await _customerRepo.GetAsync(request.Id.Value);
                    var validationResult = _customerValidator.ValidateCustomer(customer);
                    if (!validationResult.IsValid)
                    {
                        return APIResponse.GetErrorResponseFromValidation(validationResult);
                    }
                    customer.FirstName = request.FirstName;
                    customer.LastName = request.LastName;
                    customer.Email = request.Email;
                    customer.UpdatedDate = DateTime.Now;
                    customer.UpdatedBy = "Authorized User";
                    customer.Code = request.Code;
                    customer.IsActive = request.IsActive;
                    customer.PhoneNumber = request?.PhoneNumber ?? string.Empty;
                    customer.IsDeleted = false;

                    await _customerRepo.UpdateAsync(customer);

                    return new APIResponse
                    {
                        StatusCode = System.Net.HttpStatusCode.OK,
                        IsValid = true,
                    };
                }
                else
                {
                    await _customerRepo.AddAsync(new Customer
                    {
                        IsActive = request.IsActive,
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "Authorized User",
                        Code = request.Code,
                        Email = request.Email,
                        IsDeleted = false,
                        PhoneNumber = request?.PhoneNumber ?? string.Empty,
                    });

                    return new APIResponse
                    {
                        StatusCode = System.Net.HttpStatusCode.Created,
                        IsValid = true,
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
