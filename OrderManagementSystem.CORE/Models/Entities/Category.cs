using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.CORE.Models.Entities
{
    public class Category:BaseEntity
    {
        public required string Name { get; set; }
        public List<Product>? Items { get; set; } = null;
    }
}
