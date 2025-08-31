using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderManagementSystem.CORE.Models.Generics;

namespace OrderManagementSystem.CORE.Models.Responses.Concrete
{
    public class ApiErrorResponse : ApiResponse, IApiResponse
    {
        public ApiErrorResponse(string message, int statusCode)
        {
            Success = false;
            Message = message;
            StatusCode = statusCode;
        }
    }
}
