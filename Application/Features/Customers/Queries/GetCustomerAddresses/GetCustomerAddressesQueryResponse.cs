using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Customers.Queries.GetCustomerAddresses
{
    public class GetCustomerAddressesQueryResponse
    {
        public int Id { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int PostalCode { get; set; }
        public string Country { get; set; }
        public bool ShippingAddress { get; set; }
        public bool BillingAddress { get; set; }
        public int CustomerId { get; set; }
    }
}
