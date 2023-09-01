
using Interview.Backend.Entities;

namespace Interview.Backend.Interfaces
{
    public interface IOrderStepFlowRepository : IGenericRepository<OrderStepFlow>
    {
        Task<int> InsertDefaultValues(List<OrderStepFlow> list);
    }
}
