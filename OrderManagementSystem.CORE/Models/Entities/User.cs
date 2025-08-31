using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.CORE.Models.Entities
{
    public class User:BaseEntity
    {
        public required string UserName { get; set; }
        public required string Firstname { get; set; }
        public required string Lastname { get; set; }
        public required string EmailAdress { get; set; }
        public required string PhoneNumber { get; set; }
        public List<Order>? Orders { get; set; } = null;
        public List<UserAdress>? Adresses { get; set; } = null;
    }
}
