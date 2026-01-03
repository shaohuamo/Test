using DotNetCoreWebApplicationSample.Models;

namespace DotNetCoreWebApplicationSample.RepositoryContracts
{
    public interface IOrderRepository
    {
        public List<Order> GetAll();
    }
}
