using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.CORE.Models.Responses.Concrete
{
    public class ApiSuccessDataResponse<T> : ApiDataResponse<T> where T : class
    {
        public ApiSuccessDataResponse(T data, string message, int statusCode) : base(data, true, message, statusCode) { }
    }
}
