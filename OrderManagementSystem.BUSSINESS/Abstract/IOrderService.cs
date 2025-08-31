using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderManagementSystem.CORE.Models.Generics;
using OrderManagementSystem.CORE.Models.RequestModels;
using OrderManagementSystem.CORE.Models.ViewModels;

namespace OrderManagementSystem.BUSSINESS.Abstract
{
    public interface IOrderService
    {
        Task<IApiDataResponse<List<OrderWithDetailsViewModel>>> GetUsersAllOrdersAsync(int userId);
        Task<IApiDataResponse<OrderWithDetailsViewModel>> GetOrderByIdAsync(int orderId);
        Task<IApiDataResponse<OrderWithDetailsViewModel>> CreateNewOrderAsync(CreateNewOrderRequestModel model);
        Task<IApiResponse> DeleteOrderAsync(int orderId);
    }
}
