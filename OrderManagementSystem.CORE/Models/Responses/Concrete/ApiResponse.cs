using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderManagementSystem.CORE.Models.Generics;

namespace OrderManagementSystem.CORE.Models.Responses.Concrete
{
    public abstract class ApiResponse : IApiResponse
    {
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }

    }
}
