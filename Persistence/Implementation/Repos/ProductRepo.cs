using Application.Contracts.Repos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Implementation.Repos
{
    public class ProductRepo : BaseRepo<Product>, IProductRepo
    {
        public ProductRepo(SimulatedCRMContext context) : base(context)
        {
        }
    }
}
