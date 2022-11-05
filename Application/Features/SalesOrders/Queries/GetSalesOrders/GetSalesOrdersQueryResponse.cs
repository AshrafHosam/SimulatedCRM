using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SalesOrders.Queries.GetSalesOrders
{
    public class GetSalesOrdersQueryResponse
    {
        public int Id { get; set; }
        public string OrderStatus { get; set; }
        public int CustomerId { get; set; }
        public decimal SubTotal { get; set; }
        public decimal GrandTotal { get; set; }
        public AddressDTO ShippingAddress { get; set; } = new AddressDTO();
        public AddressDTO BillingAddress { get; set; } = new AddressDTO();
    }

    public class AddressDTO
    {
        public int Id { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int PostalCode { get; set; }
        public string Country { get; set; }
    }
}
