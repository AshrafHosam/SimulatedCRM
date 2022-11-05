using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Repos
{
    public interface ICustomerAddressRepo : IBaseRepo<CustomerAddress>
    {
        Task<List<CustomerAddress>> GetCustomerAddresses(int customerId);
        Task<CustomerAddress> GetCustomerShippingAddress(int customerId);
        Task<CustomerAddress> GetCustomerBillingAddress(int customerId);
    }
}
