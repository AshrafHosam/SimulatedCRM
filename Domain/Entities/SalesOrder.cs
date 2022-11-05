using Domain.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class SalesOrder : Auditable
    {
        public int Id { get; set; }
        public OrderStatusEnum OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public decimal OrderTax { get; set; }
        public decimal SubTotal { get; set; }
        public decimal GrandTotal { get; set; }
        public int SalesOrderDetailId { get; set; }
        public virtual SalesOrderDetail SalesOrderDetail { get; set; }
        public virtual List<SalesOrderAddress> SalesOrderAddresses { get; set; }
        //public int ShippingAddressId { get; set; }
        //public virtual SalesOrderAddress ShippingAddress { get; set; }
        //public int BillingAddressId { get; set; }
        //public virtual SalesOrderAddress BillingAddress { get; set; }
    }
}
