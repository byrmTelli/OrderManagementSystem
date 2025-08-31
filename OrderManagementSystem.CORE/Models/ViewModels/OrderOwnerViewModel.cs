using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderManagementSystem.CORE.Models.Entities;

namespace OrderManagementSystem.CORE.Models.ViewModels
{
    public class OrderOwnerViewModel
    {
        public int Id { get; set; }
        public required string Firstname { get; set; }
        public required string Lastname { get; set; }
    }
}
