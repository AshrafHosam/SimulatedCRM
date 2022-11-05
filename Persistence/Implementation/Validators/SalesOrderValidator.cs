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
    public class SalesOrderValidator : ISalesOrderValidator
    {
        public ValidationResult ValidateSalesOrder(SalesOrder salesOrder)
        {
            try
            {
                if (salesOrder == null)
                {
                    return new ValidationResult
                    {
                        IsValid = false,
                        Errors = new List<string>()
                    {
                        "Invalid Sales Order Id"
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
