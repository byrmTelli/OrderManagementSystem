using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.CORE.Models.Responses.Concrete
{
    public class ApiErrorDataResponse<T> : ApiDataResponse<T> where T : class
    {
        public ApiErrorDataResponse(string message, int statusCode) : base(null, false, message, statusCode) { }
    }
}
