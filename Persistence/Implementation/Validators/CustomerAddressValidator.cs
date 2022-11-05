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
    public class CustomerAddressValidator : ICustomerAddressValidator
    {
        public ValidationResult ValidateCustomerAddress(CustomerAddress customerAddress)
        {
            try
            {
                if (customerAddress == null)
                {
                    return new ValidationResult
                    {
                        IsValid = false,
                        Errors = new List<string>()
                    {
                        "Invalid User Address Id"
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

        public ValidationResult ValidateCustomerAddresses(List<CustomerAddress> customerAddresses)
        {
            try
            {
                if (customerAddresses == null || customerAddresses.Count == 0)
                {
                    return new ValidationResult
                    {
                        IsValid = false,
                        Errors = new List<string>()
                    {
                        "No Addresses Found"
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
