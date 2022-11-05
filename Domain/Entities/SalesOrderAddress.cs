using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class SalesOrderAddress
    {
        public int Id { get; set; }
        public int? CustomerAddressId { get; set; }
        public virtual CustomerAddress CustomerAddress { get; set; }
        public int? SalesOrderId { get; set; }
        public virtual SalesOrder SalesOrder { get; set; }
        public bool IsShippingAddress { get; set; } = false;
        public bool IsBillingAddress { get; set; } = false;
    }
}
