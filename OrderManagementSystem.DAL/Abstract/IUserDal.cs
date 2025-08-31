using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderManagementSystem.CORE.Models.Entities;
using OrderManagementSystem.DAL.Generics.DAL;

namespace OrderManagementSystem.DAL.Abstract
{
    public interface IUserDal:IGenericDataAccessLayerBuss<User>
    {
    }
}
