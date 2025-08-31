using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.BUSSINESS.Abstract;
using OrderManagementSystem.CORE.Models.Generics;
using OrderManagementSystem.CORE.Models.RequestModels;
using OrderManagementSystem.CORE.Models.Responses.Concrete;
using OrderManagementSystem.CORE.Models.ViewModels;
using OrderManagementSystem.DAL.Abstract;
using Microsoft.AspNetCore.Http;

namespace OrderManagementSystem.BUSSINESS.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserDal _userDal;
        private readonly IOrderDal _orderDal;

        public UserService(IUserDal userDal, IOrderDal orderDal)
        {
            _userDal = userDal;
            _orderDal = orderDal;
        }

    }
}
