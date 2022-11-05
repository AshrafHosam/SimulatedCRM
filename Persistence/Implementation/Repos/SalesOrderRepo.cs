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
    public class SalesOrderRepo : BaseRepo<SalesOrder>, ISalesOrderRepo
    {
        public SalesOrderRepo(SimulatedCRMContext context) : base(context)
        {
        }

        public async Task<SalesOrder> GetSalesOrderAddressesIncluded(int salesOrderId)
        {
            return await _context.SalesOrders
                .Include(a => a.SalesOrderAddresses)
                .FirstOrDefaultAsync(a => a.Id == salesOrderId);
        }

        public async Task<List<SalesOrder>> GetSalesOrdersAddressesIncluded()
        {
            return await _context.SalesOrders
                .Include(a => a.SalesOrderAddresses)
                .ThenInclude(a => a.CustomerAddress)
                .ToListAsync();
        }
    }
}
