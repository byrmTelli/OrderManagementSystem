using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.BUSSINESS.Abstract;
using OrderManagementSystem.CORE.Models.Entities;
using OrderManagementSystem.CORE.Models.Generics;
using OrderManagementSystem.CORE.Models.RequestModels;
using OrderManagementSystem.CORE.Models.Responses.Concrete;
using OrderManagementSystem.CORE.Models.ViewModels;
using OrderManagementSystem.DAL.Abstract;
using OrderManagementSystem.DAL.EntityFrameworkCore.Context;

namespace OrderManagementSystem.BUSSINESS.Concrete
{
    public class OrderService : IOrderService
    {
        private readonly IUserDal _userDal;
        private readonly IUserAdressDal _userAdressDal;
        private readonly IOrderDal _orderDal;
        private readonly IProductDal _productDal;
        public OrderService(IUserAdressDal userAdressDal, IUserDal userDal, IOrderDal orderDal, IProductDal productDal)
        {
            _orderDal = orderDal;
            _productDal = productDal;
            _userDal = userDal;
            _userAdressDal = userAdressDal;
        }

        public async Task<IApiDataResponse<OrderWithDetailsViewModel>> CreateNewOrderAsync(CreateNewOrderRequestModel model)
        {
            try
            {
                var usersQuery = _userDal.GetQueryable();
                var user = usersQuery.Where(u => u.Id == model.UserId).FirstOrDefault();

                if (user == null)
                    return new ApiErrorDataResponse<OrderWithDetailsViewModel>("Geçersiz kullanıcı.", StatusCodes.Status400BadRequest);


                var userAdressQuery = _userAdressDal.GetQueryable();
                var orderAdress = userAdressQuery.Where(x => x.UserId == model.UserId && x.Id == model.AdressId).FirstOrDefault();

                if (orderAdress == null)
                    return new ApiErrorDataResponse<OrderWithDetailsViewModel>("Geçersiz kullanıcı adresi.", StatusCodes.Status400BadRequest);

                if (model.Items.Count <= 0)
                {
                    return new ApiErrorDataResponse<OrderWithDetailsViewModel>("Herhangi bir ürün seçmeden sipariş oluşturamazsınız.", StatusCodes.Status400BadRequest
                        );
                }

                if (model.Items.Any(i => i.Quantity <= 0))
                    return new ApiErrorDataResponse<OrderWithDetailsViewModel>("Ürünlerinize ait sipariş adedi girmelisiniz.", StatusCodes.Status400BadRequest
    );
                var normalizedItems = model.Items
                                    .GroupBy(i => i.ProductId)
                                    .Select(g => new { ProductId = g.Key, Quantity = g.Sum(x => x.Quantity) })
                                    .ToList();

                var requestedIds = normalizedItems.Select(x => x.ProductId).ToList();

                var products = await _productDal.GetQueryable()
                                                .Where(p => requestedIds.Contains(p.Id))
                                                .ToListAsync();

                var foundIds = products.Select(p => p.Id).ToHashSet();
                var missingIds = requestedIds.Where(id => !foundIds.Contains(id)).ToList();
                if (missingIds.Count > 0)
                    return new ApiErrorDataResponse<OrderWithDetailsViewModel>($"Belirttiğiniz ürün bulunamaktadır.", StatusCodes.Status400BadRequest);

                var productsQuery = _productDal.GetQueryable();

                var insufficient = normalizedItems
                                    .Select(i => new
                                    {
                                        i.ProductId,
                                        i.Quantity,
                                        Product = products.First(p => p.Id == i.ProductId)
                                    })
                                    .Where(x => x.Product.CurrentStockQuantity < x.Quantity)
                                    .Select(x => new { x.ProductId, Needed = x.Quantity, Available = x.Product.CurrentStockQuantity })
                                    .ToList();

                if (insufficient.Count > 0)
                {
                    return new ApiErrorDataResponse<OrderWithDetailsViewModel>($"Belirttiğiniz ürünler için stok yetersiz", StatusCodes.Status400BadRequest);
                }

                var newOrder = new Order
                {
                    UserId = user.Id,
                    AdressId = orderAdress.Id
                };

                var createOrderResult = await _orderDal.AddAsync(newOrder);

                if (createOrderResult is null)
                    return new ApiErrorDataResponse<OrderWithDetailsViewModel>("Sipariş oluşturulurken bir hata meydana geldi.", StatusCodes.Status500InternalServerError);

                var orderItems = new List<OrderItem>();
                foreach (var orderItem in model.Items)
                {

                    var productBeUpdated = products.Where(p => p.Id == orderItem.ProductId).FirstOrDefault();

                    if (productBeUpdated is null)
                        return new ApiErrorDataResponse<OrderWithDetailsViewModel>("İlgili ürün bulunamadı", StatusCodes.Status404NotFound);

                    productBeUpdated.CurrentStockQuantity -= orderItem.Quantity;

                    var newOrderItem = new OrderItem
                    {
                        ProductId = orderItem.ProductId,
                        Quantity = orderItem.Quantity,
                        OrderId = createOrderResult.Id
                    };
                    orderItems.Add(newOrderItem);
                }

                _productDal.UpdateRange(products);

                createOrderResult.OrderItems = orderItems;
                await _orderDal.UpdateAsync(createOrderResult);

                var ordersQuery = _orderDal.GetQueryable();

                var orderCreated = await ordersQuery.Where(o => o.Id == createOrderResult.Id)
                    .Select(order => new OrderWithDetailsViewModel
                    {
                        Id = order.Id,
                        Owner = new OrderOwnerViewModel
                        {
                            Id = order.Owner.Id,
                            Firstname = order.Owner.Firstname,
                            Lastname = order.Owner.Lastname,
                        },
                        Adress = new OrderAdressViewModel
                        {
                            Id = order.AdressId,
                            Country = order.Adress.Country,
                            City = order.Adress.City,
                            District = order.Adress.District,
                            Street = order.Adress.Street,
                            PostalCode = order.Adress.PostalCode
                        },
                        OrderItems = order.OrderItems
                    .Select(oi => new OrderItemViewModel
                    {
                        Id = oi.Id,
                        ProductName = oi.Product.Name,
                        Quantity = oi.Quantity,
                        ShippingDate = oi.ShippingDate
                    }).ToList(),
                        CreatedAt = order.CreatedAt,

                    })
                    .FirstOrDefaultAsync();

                return new ApiSuccessDataResponse<OrderWithDetailsViewModel>(orderCreated, "Order created successfully.", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return new ApiErrorDataResponse<OrderWithDetailsViewModel>($"Belirttiğiniz ürünler için stok yetersiz", StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<IApiResponse> DeleteOrderAsync(int orderId)
        {
            try
            {
                var ordersQuery = _orderDal.GetQueryable();
                var order = ordersQuery.Where(o => o.Id == orderId).FirstOrDefault();

                if (order is null)
                    return new ApiErrorResponse("Sipariş bulunamadı.", StatusCodes.Status404NotFound);

                order.IsDeleted = true;

                await _orderDal.UpdateAsync(order);

                return new ApiSuccessResponse("Sipariş başarıyla silindi.", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return new ApiErrorResponse("Sipariş silinirken bir hata meydana geldi.", StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<IApiDataResponse<OrderWithDetailsViewModel>> GetOrderByIdAsync(int orderId)
        {
            try
            {
                var ordersQuery = _orderDal.GetQueryable();

                var order = await ordersQuery
                    .Where(o => o.Id == orderId && o.IsDeleted == false)
                    .Include(o => o.Adress)
                    .Include(o => o.Owner)
                    .Include(o => o.OrderItems).ThenInclude(oi => oi.Product)
                    .FirstOrDefaultAsync();
                if (order is null)
                    return new ApiErrorDataResponse<OrderWithDetailsViewModel>("Sipariş bulunamadı.", StatusCodes.Status404NotFound);

                var orderViewModel = new OrderWithDetailsViewModel
                {
                    Id = order.Id,
                    Owner = new OrderOwnerViewModel
                    {
                        Id = order.Owner.Id,
                        Firstname = order.Owner.Firstname,
                        Lastname = order.Owner.Lastname,
                    },
                    Adress = new OrderAdressViewModel
                    {
                        Id = order.AdressId,
                        Country = order.Adress.Country,
                        City = order.Adress.City,
                        District = order.Adress.District,
                        Street = order.Adress.Street,
                        PostalCode = order.Adress.PostalCode
                    },
                    OrderItems = order.OrderItems
                    .Select(oi => new OrderItemViewModel
                    {
                        Id = oi.Id,
                        ProductName = oi.Product.Name,
                        Quantity = oi.Quantity,
                        ShippingDate = oi.ShippingDate
                    }).ToList(),
                    CreatedAt = order.CreatedAt,
                };

                return new ApiSuccessDataResponse<OrderWithDetailsViewModel>(orderViewModel, "Order fetched successfully.", StatusCodes.Status200OK);

            }
            catch (Exception ex)
            {
                return new ApiErrorDataResponse<OrderWithDetailsViewModel>("Something went wrong while fetching order.", StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<IApiDataResponse<List<OrderWithDetailsViewModel>>> GetUsersAllOrdersAsync(int userId)
        {
            try
            {
                var allOrdersQuery = _orderDal.GetQueryable()
                    .Where(order => order.UserId == userId)
                    .OrderBy(order => order.CreatedAt);

                var allOrders = await allOrdersQuery.Select(order => new OrderWithDetailsViewModel
                {
                    Id = order.Id,
                    Owner = new OrderOwnerViewModel
                    {
                        Id = order.Owner.Id,
                        Firstname = order.Owner.Firstname,
                        Lastname = order.Owner.Lastname,
                    },
                    Adress = new OrderAdressViewModel
                    {
                        Id = order.AdressId,
                        Country = order.Adress.Country,
                        City = order.Adress.City,
                        District = order.Adress.District,
                        Street = order.Adress.Street,
                        PostalCode = order.Adress.PostalCode
                    },
                    OrderItems = order.OrderItems
                    .Select(oi => new OrderItemViewModel
                    {
                        Id = oi.Id,
                        ProductName = oi.Product.Name,
                        Quantity = oi.Quantity,
                        ShippingDate = oi.ShippingDate
                    }).ToList(),
                    CreatedAt = order.CreatedAt,

                }).ToListAsync();


                return new ApiSuccessDataResponse<List<OrderWithDetailsViewModel>>(allOrders, "Orders fetched successfully.", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                // Logging will be here...
                return new ApiErrorDataResponse<List<OrderWithDetailsViewModel>>("Something went wrong while fetching user's orders.", StatusCodes.Status500InternalServerError);
            }
        }
    }
}
