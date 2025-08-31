using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.CORE.Models.Entities
{
    public class Product:BaseEntity
    {
        public required string Name { get; set; }
        public required int CategoryId { get; set; }
        public Category Category { get; set; }
        public int  CurrentStockQuantity { get; set; }
    }
}
