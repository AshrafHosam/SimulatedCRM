using Domain.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Validators
{
    public interface ICustomerAddressValidator
    {
        ValidationResult ValidateCustomerAddress(CustomerAddress customerAddress);
        ValidationResult ValidateCustomerAddresses(List<CustomerAddress> customerAddresses);
    }
}
