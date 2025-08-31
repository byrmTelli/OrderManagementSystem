using OrderManagementSystem.BUSSINESS.Abstract;
using OrderManagementSystem.BUSSINESS.Concrete;
using OrderManagementSystem.DAL.Abstract;
using OrderManagementSystem.DAL.Concrete;
using OrderManagementSystem.DAL.EntityFrameworkCore.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

#region Swagger UI Settings
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#endregion

#region DB Settings
builder.Services.AddDbContext<OrderManagementSystemContext>();
#endregion

#region Data Access Layer DI's
builder.Services.AddScoped<ICategoryDal, CategoryDal>();
builder.Services.AddScoped<IOrderDal, OrderDal>();
builder.Services.AddScoped<IOrderItemDal, OrderItemDal>();
builder.Services.AddScoped<IProductDal, ProductDal>();
builder.Services.AddScoped<IUserAdressDal, UserAdressDal>();
builder.Services.AddScoped<IUserDal, UserDal>();
#endregion

#region Service Layer DI's
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IUserService, UserService>();
#endregion



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "OrderManagementSystem API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
