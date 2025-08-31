using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderManagementSystem.CORE.Models.Entities;

namespace OrderManagementSystem.CORE.Models.ViewModels
{
    public class OrderItemProductViewModel
    {
        public required string Name { get; set; }
        public OrderItemProductCategoryViewModel Category { get; set; }
        public int OrderQuantity { get; set; }
    }
}
