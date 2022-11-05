using Application.Contracts.Repos;
using Application.Contracts.Validators;
using Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries.GetProductsQuery
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, APIResponse>
    {
        private readonly IProductRepo _productRepo;
        private readonly IProductValidator _productValidator;

        public GetProductsQueryHandler(IProductRepo productRepo, IProductValidator productValidator)
        {
            _productRepo = productRepo;
            _productValidator = productValidator;
        }

        public async Task<APIResponse> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var products = await _productRepo.GetAllAsync();

                return new APIResponse
                {
                    IsValid = true,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Data = products.Count() > 0 ? products.Select(a => new GetProductsQueryResponse
                    {
                        Id = a.Id,
                        Description = a.Description,
                        Name = a.Name,
                        Price = a.Price
                    }).ToList() : null,
                };
            }
            catch (Exception ex)
            {
                return APIResponse.GetExceptionResponse(ex);
            }
        }
    }
}
