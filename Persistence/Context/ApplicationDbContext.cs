
using Microsoft.EntityFrameworkCore;
using System.Data;
using Interview.Backend.Entities;

namespace Persistence.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasMany(e => e.Products)
                .WithMany(e => e.Orders)
                .UsingEntity<OrderProduct>();
        }

        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<OrderStep> OrderSteps { get; set; }
        public DbSet<OrderStepFlow> OrderStepFlows { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public IDbConnection Connection => Database.GetDbConnection();
    }
}