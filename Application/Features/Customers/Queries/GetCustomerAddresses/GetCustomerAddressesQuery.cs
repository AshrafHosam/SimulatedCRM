using Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Customers.Queries.GetCustomerAddresses
{
    public class GetCustomerAddressesQuery : IRequest<APIResponse>
    {
        [Required]
        public int CustomerId { get; set; }
    }
}
