using Application.Response;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SalesOrders.Commands.EditSalesOrderDetail
{
    public class EditSalesOrderDetailCommand : IRequest<APIResponse>
    {
        [Required]
        public int SalesOrderLineId { get; set; }
        public int SalesOrderLineNumber { get; set; }
        [Required]
        public int ProductId { get; set; }
        public decimal LinePrice { get; set; } = 0;
        public decimal LineOrderedQuantity { get; set; } = 0;
        public decimal LineTaxAmount { get; set; } = 0.14m;
    }
}
