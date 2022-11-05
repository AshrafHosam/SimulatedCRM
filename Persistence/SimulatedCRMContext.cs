using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class SimulatedCRMContext : DbContext
    {
        public SimulatedCRMContext(DbContextOptions<SimulatedCRMContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerAddress> Addresses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<SalesOrder> SalesOrders { get; set; }
        public DbSet<SalesOrderDetail> SalesOrderDetails { get; set; }
        public DbSet<SalesOrderAddress> SalesOrderAddresses { get; set; }
    }
}
