using DotNetCoreWebApplicationSample.Models;
using DotNetCoreWebApplicationSample.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreWebApplicationSample.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;

        // 构造函数签名不变，DI容器会自动注入新依赖
        
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        [Route("/orders")]
        public async Task<IActionResult> CreateOrder()
        {
             _orderService.PlaceOrder();
            return Ok();
        }
    }
}
