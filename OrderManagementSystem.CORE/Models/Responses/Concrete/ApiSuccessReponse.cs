using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderManagementSystem.CORE.Models.Generics;

namespace OrderManagementSystem.CORE.Models.Responses.Concrete
{
    public class ApiSuccessResponse : ApiResponse, IApiResponse
    {
        public ApiSuccessResponse(string message, int statusCode)
        {
            Success = true;
            Message = message;
            StatusCode = statusCode;
        }
    }
}
