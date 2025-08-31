using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderManagementSystem.CORE.Models.Entities;

namespace OrderManagementSystem.CORE.Models.ViewModels
{
    public class OrderItemViewModel
    {
        public int Id { get; set; }
        public required string ProductName { get; set; }
        public required int Quantity { get; set; }
        public DateTime? ShippingDate { get; set; } = null;
    }
}
