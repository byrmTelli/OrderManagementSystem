using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderManagementSystem.CORE.Models.Generics;

namespace OrderManagementSystem.CORE.Models.Responses.Concrete
{
    public abstract class ApiDataResponse<T> : IApiDataResponse<T> where T : class
    {
        public T Data { get; set; }
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }

        protected ApiDataResponse()
        {
            Data = null;
        }
        protected ApiDataResponse(T data, bool success, string message, int statusCode)
        {
            Data = data;
            Success = success;
            Message = message;
            StatusCode = statusCode;
        }
    }
}
