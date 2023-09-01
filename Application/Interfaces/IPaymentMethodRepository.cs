
using Interview.Backend.Entities;

namespace Interview.Backend.Interfaces
{
    public interface IPaymentMethodRepository : IGenericRepository<PaymentMethod>
    {
        Task<int> InsertDefaultValues(List<PaymentMethod> list);
    }
}
