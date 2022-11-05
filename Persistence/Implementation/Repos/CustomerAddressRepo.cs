using Application.Contracts.Repos;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Implementation.Repos
{
    public class CustomerAddressRepo : BaseRepo<CustomerAddress>, ICustomerAddressRepo
    {
        public CustomerAddressRepo(SimulatedCRMContext context) : base(context)
        {
        }

        public async Task<List<CustomerAddress>> GetCustomerAddresses(int customerId)
        {
            return await _context.Addresses.Where(a => a.CustomerId == customerId).ToListAsync();
        }

        public async Task<CustomerAddress> GetCustomerBillingAddress(int customerId)
        {
            return await _context.Addresses.FirstOrDefaultAsync(a => a.CustomerId == customerId && a.BillingAddress == true);
        }

        public async Task<CustomerAddress> GetCustomerShippingAddress(int customerId)
        {
            return await _context.Addresses.FirstOrDefaultAsync(a => a.CustomerId == customerId && a.ShippingAddress == true);
        }
    }
}
