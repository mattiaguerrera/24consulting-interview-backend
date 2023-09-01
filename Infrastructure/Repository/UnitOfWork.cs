using Interview.Backend.Entities;
using Interview.Backend.Interfaces;

namespace Interview.Backend.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        public UnitOfWork(
            IProductRepository productRepository,
            ICustomerRepository customerRepository,
            IOrderRepository orderRepository,
            IOrderStepRepository orderStepRepository,
            IOrderStepFlowRepository orderStepFlowRepository,
            IPaymentMethodRepository paymentMethodRepository)
        {
            Products = productRepository;
            Customers = customerRepository;
            Orders = orderRepository;
            OrderSteps = orderStepRepository;
            OrderStepFlows = orderStepFlowRepository;
            PaymentMethods = paymentMethodRepository;
        }
        public IProductRepository Products { get; }
        public ICustomerRepository Customers { get; }
        public IOrderRepository Orders { get; }
        public IOrderStepRepository OrderSteps { get; }
        public IOrderStepFlowRepository OrderStepFlows { get; }
        public IPaymentMethodRepository PaymentMethods { get; }
    }
}
