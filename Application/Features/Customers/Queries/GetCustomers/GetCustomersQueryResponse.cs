using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Customers.Queries.GetCustomers
{
    public class GetCustomersQueryResponse
    {
        public List<CustomerDTO> Customers { get; set; } = new List<CustomerDTO>();
        public int TotalCount { get; set; } = 0;
        public int TotalFilteredCount { get; set; } = 0;
    }

    public class CustomerDTO
    {
        public int? Id { get; set; }
        public string Code { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsActive { get; set; }
    }
}
