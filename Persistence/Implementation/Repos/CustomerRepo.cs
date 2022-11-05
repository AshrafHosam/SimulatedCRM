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
    public class CustomerRepo : BaseRepo<Customer>, ICustomerRepo
    {
        public CustomerRepo(SimulatedCRMContext context) : base(context)
        {
        }

        public async Task<int> GetAllCount()
        {
            return await _context.Customers.CountAsync();
        }
    }
}
