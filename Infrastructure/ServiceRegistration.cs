using Dapper.Infrastructure.Repository;
using Interview.Backend.Interfaces;
using Interview.Backend.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Interview.Backend.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IOrderStepRepository, OrderStepRepository>();
            services.AddTransient<IOrderStepFlowRepository, OrderStepFlowRepository>();
            services.AddTransient<IPaymentMethodRepository, PaymentMethodRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
