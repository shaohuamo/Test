using DotNetCoreWebApplicationSample.Models;
using DotNetCoreWebApplicationSample.RepositoryContracts;
using DotNetCoreWebApplicationSample.ServiceContracts;

namespace DotNetCoreWebApplicationSample.Repositories
{
    public class OrderRepository: IOrderRepository
    {
        private readonly ICustomLogger _logger; // 新增参数

        public OrderRepository(ICustomLogger logger)
        {
            _logger = logger;
        }

        public List<Order> GetAll()
        {
            _logger.Log("this is from OrderRepository");
            return new List<Order>();
        }
    }
}
