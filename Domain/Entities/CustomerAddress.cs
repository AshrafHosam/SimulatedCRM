using Domain.Common;

namespace Domain.Entities
{
    public class CustomerAddress : Auditable
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
        public virtual Customer Customer { get; set; }
        public virtual List<SalesOrderAddress> SalesOrderAddresses { get; set; }
    }
}