namespace Interview.Backend.Interfaces
{
    public interface IUnitOfWork
    {
        IProductRepository Products { get; }
        ICustomerRepository Customers { get; }
        IOrderRepository Orders { get; }
        IOrderStepRepository OrderSteps { get; }
        IOrderStepFlowRepository OrderStepFlows { get; }
        IPaymentMethodRepository PaymentMethods { get; }
    }
}
