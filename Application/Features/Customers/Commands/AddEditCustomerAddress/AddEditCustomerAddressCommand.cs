using Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Customers.Commands.AddEditCustomerAddress
{
    public class AddEditCustomerAddressCommand : IRequest<APIResponse>
    {
        public int? Id { get; set; }
        [Required]
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int PostalCode { get; set; }
        public string Country { get; set; }
        public bool ShippingAddress { get; set; }
        public bool BillingAddress { get; set; }
        [Required]
        public int CustomerId { get; set; }
    }
}
