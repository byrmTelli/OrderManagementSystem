using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.CORE.Models.Entities
{
    public class UserAdress:BaseEntity
    {
        public required int UserId { get; set; }
        public User User { get; set; }
        public required string Country { get; set; }
        public required string City { get; set; }
        public required string District { get; set; }
        public required string Street { get; set; }
        public required string PostalCode { get; set; }
        public List<Order> Orders { get; set; }
    }
}
