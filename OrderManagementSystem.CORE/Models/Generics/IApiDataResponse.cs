using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.CORE.Models.Generics
{
    public interface IApiDataResponse<T> : IApiResponse where T : class
    {
        T Data { get; set; }
    }
}
