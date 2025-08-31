using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderManagementSystem.CORE.Models.Entities;
using OrderManagementSystem.DAL.Abstract;
using OrderManagementSystem.DAL.EntityFrameworkCore.Context;
using OrderManagementSystem.DAL.Generics.DAL;

namespace OrderManagementSystem.DAL.Concrete
{
    public class OrderDal:GenericDataAccesssLayerBuss<Order>,IOrderDal
    {
        public OrderDal(OrderManagementSystemContext context):base(context){}
    }
}
