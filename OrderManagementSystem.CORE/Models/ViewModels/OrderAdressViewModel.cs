using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.CORE.Models.ViewModels
{
    public class OrderAdressViewModel
    {
        public int Id { get; set; }
        public required string Country { get; set; }
        public required string City { get; set; }
        public required string District { get; set; }
        public required string Street { get; set; }
        public required string PostalCode { get; set; }
    }
}
