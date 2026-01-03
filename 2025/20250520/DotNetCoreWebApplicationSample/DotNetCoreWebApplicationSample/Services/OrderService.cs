using DotNetCoreWebApplicationSample.Models;
using DotNetCoreWebApplicationSample.RepositoryContracts;
using DotNetCoreWebApplicationSample.ServiceContracts;

namespace DotNetCoreWebApplicationSample.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepo;

        public OrderService(IOrderRepository orderRepo)
        {
            _orderRepo = orderRepo;
        }

        public void PlaceOrder()
        {
             _orderRepo.GetAll();
        }
    }
}
