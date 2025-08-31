using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.CORE.Models.RequestModels
{
    public class CreateNewOrderRequestModel
    {
        public int UserId { get; set; }
        public int AdressId { get; set; }
        public required List<CreateNewOrderOrderItemRequestModel> Items { get; set; }
    }
}
