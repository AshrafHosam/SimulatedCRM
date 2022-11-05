using Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Customers.Commands.AddEditCustomer
{
    public class AddEditCustomerCommand : IRequest<APIResponse>
    {
        public int? Id { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsActive { get; set; }
    }
}
