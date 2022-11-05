using Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class SalesOrderDetail : Auditable
    {
        [Key]
        public int SalesOrderLineId { get; set; }
        public int SalesOrderLineNumber { get; set; }
        public int? ProductId { get; set; }
        public virtual Product Product { get; set; }
        public decimal LinePrice { get; set; } = 0;
        public decimal LineOrderedQuantity { get; set; } = 0;
        public decimal LineTaxAmount { get; set; }
        private decimal _lineTotal;
        public decimal LineTotal
        {
            get
            {
                return this.LinePrice * this.LineOrderedQuantity;
            }

            set
            {
                this._lineTotal = value;
            }
        }
    }
}