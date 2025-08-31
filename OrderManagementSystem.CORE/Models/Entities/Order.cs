using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.CORE.Models.Entities
{
    public class Order : BaseEntity
    {
        public int UserId { get; set; }
        public User Owner { get; set; }
        public int AdressId { get; set; }
        public UserAdress Adress { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
