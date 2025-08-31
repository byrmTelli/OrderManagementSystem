using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.CORE.Models.Entities
{
    public class OrderItem : BaseEntity
    {
        public required int ProductId { get; set; }
        public Product Product { get; set; }
        public required int Quantity { get; set; }
        public required int OrderId { get; set; }
        public Order Order { get; set; }
        public DateTime? ShippingDate { get; set; } = null;
    }
}
