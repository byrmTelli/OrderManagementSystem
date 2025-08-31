using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderManagementSystem.CORE.Models.Entities;

namespace OrderManagementSystem.CORE.Models.ViewModels
{
    public class OrderWithDetailsViewModel
    {
        public int Id { get; set; }
        public OrderOwnerViewModel Owner { get; set; }
        public OrderAdressViewModel Adress { get; set; }
        public List<OrderItemViewModel> OrderItems { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
