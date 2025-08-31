using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.CORE.Models.ViewModels
{
    public class OrderItemProductCategoryViewModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }
}
