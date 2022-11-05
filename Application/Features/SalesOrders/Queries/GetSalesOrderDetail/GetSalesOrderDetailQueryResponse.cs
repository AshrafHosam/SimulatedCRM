using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SalesOrders.Queries.GetSalesOrderDetail
{
    public class GetSalesOrderDetailQueryResponse
    {
        public int SalesOrderLineId { get; set; }
        public int SalesOrderLineNumber { get; set; }
        public int? ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal LinePrice { get; set; }
        public decimal LineOrderedQuantity { get; set; }
        public decimal LineTaxAmount { get; set; }
        public decimal LineTotal { get; set; }
    }
}
