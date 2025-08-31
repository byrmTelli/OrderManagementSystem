using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.CORE.Models.RequestModels
{
    public class CreateNewOrderOrderItemRequestModel
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
