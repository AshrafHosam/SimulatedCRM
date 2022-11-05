using Domain.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Validators
{
    public interface ISalesOrderDetailValidator
    {
        ValidationResult ValidateSalesOrderDetail(SalesOrderDetail salesOrderDetail);
    }
}
