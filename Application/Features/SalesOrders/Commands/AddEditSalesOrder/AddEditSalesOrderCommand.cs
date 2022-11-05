using Application.Response;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SalesOrders.Commands.AddEditSalesOrder
{
    public class AddEditSalesOrderCommand : IRequest<APIResponse>
    {
        public int? Id { get; set; }
        public OrderStatusEnum OrderStatus { get; set; }
        public int CustomerId { get; set; }
        public decimal SubTotal { get; set; }
        private decimal _grandTotal;
        public decimal GrandTotal
        {
            get
            {
                return (decimal)SubTotal * 1.14m;
            }
            set
            {
                _grandTotal = value;
            }
        }
        public bool CustomerShippingAddress { get; set; }
        public int? ShippingAddressId { get; set; }
        public bool CustomerBillingAddress { get; set; }
        public int? BillingAddressId { get; set; }
    }
}
