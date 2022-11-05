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
    public class SalesOrderDetailRepo : BaseRepo<SalesOrderDetail>, ISalesOrderDetailRepo
    {
        public SalesOrderDetailRepo(SimulatedCRMContext context) : base(context)
        {
        }

        public async Task<SalesOrderDetail> GetSalesOrderDetailProductIncluded(int salesOrderDetailId)
        {
            return await _context.SalesOrderDetails
                .Include(a => a.Product)
                .FirstOrDefaultAsync(a => a.SalesOrderLineId == salesOrderDetailId);
        }
    }
}
