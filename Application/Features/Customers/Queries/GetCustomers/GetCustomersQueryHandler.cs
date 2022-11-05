using Application.Contracts.Repos;
using Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Customers.Queries.GetCustomers
{
    public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, APIResponse>
    {
        private readonly ICustomerRepo _customerRepo;

        public GetCustomersQueryHandler(ICustomerRepo customerRepo)
        {
            _customerRepo = customerRepo;
        }

        public async Task<APIResponse> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = new GetCustomersQueryResponse();
                result.TotalCount = await _customerRepo.GetAllCount();
                var customersEntity = await _customerRepo.GetAllAsync();
                result.Customers = customersEntity?.Select(a => new CustomerDTO
                {
                    Id = a.Id,
                    Code = a.Code,
                    Email = a.Email,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    IsActive = a.IsActive,
                    PhoneNumber = a?.PhoneNumber ?? string.Empty
                }).ToList() ?? new List<CustomerDTO>();

                return new APIResponse
                {
                    IsValid = true,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return APIResponse.GetExceptionResponse(ex);
            }
        }
    }
}
