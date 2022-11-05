using Application.Contracts.Repos;
using Application.Contracts.Validators;
using Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Commands.AddEditProduct
{
    public class AddEditProductCommandHandler : IRequestHandler<AddEditProductCommand, APIResponse>
    {
        private readonly IProductRepo _productRepo;
        private readonly IProductValidator _productValidator;

        public AddEditProductCommandHandler(IProductRepo productRepo, IProductValidator productValidator)
        {
            _productRepo = productRepo;
            _productValidator = productValidator;
        }

        public async Task<APIResponse> Handle(AddEditProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Id.HasValue)
                {
                    var product = await _productRepo.GetAsync(request.Id.Value);

                    var productValidationResult = _productValidator.ValidateProduct(product);
                    if (!productValidationResult.IsValid)
                        return APIResponse.GetErrorResponseFromValidation(productValidationResult);

                    product.UpdatedDate = DateTime.Now;
                    product.UpdatedBy = "Authorized User";
                    product.Description = request.Description;
                    product.Name = request.Name;
                    product.Price = request.Price;

                    await _productRepo.UpdateAsync(product);

                    return new APIResponse
                    {
                        IsValid = true,
                        StatusCode = System.Net.HttpStatusCode.OK
                    };
                }
                else
                {
                    await _productRepo.AddAsync(new Domain.Entities.Product
                    {
                        Name = request.Name,
                        CreatedBy = "Authorized USer",
                        CreatedDate = DateTime.Now,
                        IsDeleted = false,
                        Price = request.Price,
                        Description = request.Description,
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
