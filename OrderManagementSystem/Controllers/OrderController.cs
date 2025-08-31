using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.BUSSINESS.Abstract;
using OrderManagementSystem.CORE.Models.Generics;
using OrderManagementSystem.CORE.Models.RequestModels;
using OrderManagementSystem.CORE.Models.ViewModels;

namespace OrderManagementSystem.WEBAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost]
        [ProducesResponseType(typeof(IApiDataResponse<OrderWithDetailsViewModel>), 200)]
        [ProducesResponseType(typeof(IApiDataResponse<OrderWithDetailsViewModel>), 400)]
        [ProducesResponseType(typeof(IApiDataResponse<OrderWithDetailsViewModel>), 500)]
        public async Task<IActionResult> CreateNewOrder(CreateNewOrderRequestModel request)
        {
            var result = await _orderService.CreateNewOrderAsync(request);

            return StatusCode(result.StatusCode, result);
        }
        [HttpGet]
        [ProducesResponseType(typeof(IApiDataResponse<List<OrderWithDetailsViewModel>>), 200)]
        [ProducesResponseType(typeof(IApiDataResponse<List<OrderWithDetailsViewModel>>), 404)]
        [ProducesResponseType(typeof(IApiDataResponse<List<OrderWithDetailsViewModel>>), 400)]
        [ProducesResponseType(typeof(IApiDataResponse<List<OrderWithDetailsViewModel>>), 500)]
        public async Task<IActionResult> GetUsersAllOrders(int userId)
        {
            var result = await _orderService.GetUsersAllOrdersAsync(userId);

            return StatusCode(result.StatusCode, result);
        }
        [HttpGet]
        [ProducesResponseType(typeof(IApiDataResponse<List<OrderWithDetailsViewModel>>), 200)]
        [ProducesResponseType(typeof(IApiDataResponse<List<OrderWithDetailsViewModel>>), 404)]
        [ProducesResponseType(typeof(IApiDataResponse<List<OrderWithDetailsViewModel>>), 400)]
        [ProducesResponseType(typeof(IApiDataResponse<List<OrderWithDetailsViewModel>>), 500)]
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            var result = await _orderService.GetOrderByIdAsync(orderId);

            return StatusCode(result.StatusCode, result);
        }
        [HttpDelete]
        [ProducesResponseType(typeof(IApiResponse), 200)]
        [ProducesResponseType(typeof(IApiResponse), 404)]
        [ProducesResponseType(typeof(IApiResponse), 400)]
        [ProducesResponseType(typeof(IApiResponse), 500)]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            var result = await _orderService.DeleteOrderAsync(orderId);

            return StatusCode(result.StatusCode, result);
        }
    }
}
