using Application.Contracts.Validators;
using Domain.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Implementation.Validators
{
    public class ProductValidator : IProductValidator
    {
        public ValidationResult ValidateProduct(Product product)
        {
            try
            {
                if (product == null)
                {
                    return new ValidationResult
                    {
                        IsValid = false,
                        Errors = new List<string>()
                    {
                        "Invalid Product Id"
                    },
                        StatusCode = System.Net.HttpStatusCode.NotFound
                    };
                }
                return new ValidationResult();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
