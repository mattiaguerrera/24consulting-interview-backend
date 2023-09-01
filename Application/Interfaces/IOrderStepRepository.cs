
using Interview.Backend.Entities;

namespace Interview.Backend.Interfaces
{
    public interface IOrderStepRepository : IGenericRepository<OrderStep>
    {
        Task<int> InsertDefaultValues(List<OrderStep> list);
        bool CheckOrderStep(int idOrderStep, Order order, out IEnumerable<string> messages);

    }
}
