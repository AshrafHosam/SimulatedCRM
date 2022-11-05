using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum OrderStatusEnum
    {
        PendingConfirmation = 1,
        Confirmed = 2,
        PendingPAyment = 3,
        Paid = 4,
        Processing = 5,
        OnHold = 6,
        Shipped = 7,
        Completed = 8,
        Cancelled = 9,
        Refunded = 10
    }
}
