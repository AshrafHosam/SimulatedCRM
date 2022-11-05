using Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries.GetProductsQuery
{
    public class GetProductsQuery : IRequest<APIResponse>
    {
    }
}
