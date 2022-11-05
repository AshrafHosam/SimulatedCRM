using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Repos
{
    public interface ISalesOrderDetailRepo : IBaseRepo<SalesOrderDetail>
    {
        Task<SalesOrderDetail> GetSalesOrderDetailProductIncluded(int salesOrderDetailId);
    }
}
