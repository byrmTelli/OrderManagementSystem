using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using OrderManagementSystem.CORE.Models.Entities;

namespace OrderManagementSystem.DAL.EntityFrameworkCore.Context
{
    public class OrderManagementSystemContext : DbContext
    {
        public OrderManagementSystemContext(DbContextOptions options) : base(options) { }

        #region DB sets
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<UserAdress> UserAdresses { get; set; }
        #endregion
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=OrderManagementSystemDB.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            #region Relations
            // Order - User
            modelBuilder.Entity<Order>()
                .HasOne(order => order.Owner)
                .WithMany(user => user.Orders)
                .HasForeignKey(order => order.UserId);
            // Order - OrderItem
            modelBuilder.Entity<Order>()
                .HasMany(order => order.OrderItems)
                .WithOne(item => item.Order)
                .HasForeignKey(item => item.OrderId);
            // Item - Category
            modelBuilder.Entity<Product>()
                .HasOne(item => item.Category)
                .WithMany(category => category.Items)
                .HasForeignKey(item => item.CategoryId);
            // User - User Adress
            modelBuilder.Entity<User>()
                .HasMany(user => user.Adresses)
                .WithOne(adress => adress.User)
                .HasForeignKey(adress => adress.UserId);
            modelBuilder.Entity<UserAdress>()
                 .HasMany(ua => ua.Orders)
                 .WithOne(order => order.Adress)
                 .HasForeignKey(order => order.AdressId);
            #endregion

            #region Seedings
            var T0 = new DateTime(2025, 8, 31, 12, 0, 0, DateTimeKind.Utc);

            var sampleUser = new User
            {
                Id = 1,
                UserName = "btelli123",
                Firstname = "Bayram",
                Lastname = "Telli",
                EmailAdress = "byrm.telli.contact@gmail.com",
                PhoneNumber = "0 555 444 33 22",
                CreatedAt = T0
            };
            #region Sample User Adresses
            var sampleUserAdress1 = new UserAdress
            {
                Id = 1,
                UserId = 1,
                Country = "Türkiye",
                City = "Ankara",
                District = "Yeni Mahalle",
                Street = "X Street",
                PostalCode = "06500",
                CreatedAt = T0
            };
            var sampleUserAdress2 = new UserAdress
            {
                Id = 2,
                UserId = 1,
                Country = "Türkiye",
                City = "Istanbul",
                District = "Şişli",
                Street = "X Street",
                PostalCode = "34500",
                CreatedAt = T0
            };
            var sampleUserAdress3 = new UserAdress
            {
                Id = 3,
                UserId = 1,
                Country = "Türkiye",
                City = "Tekirdağ",
                District = "Çerkezköy",
                Street = "X Street",
                PostalCode = "59500",
                CreatedAt = T0
            };
            #endregion

            #region Sample Categories

            var sampleCategory = new Category
            {
                Id = 1,
                Name = "Food",
                CreatedAt = T0
            };
            #endregion

            #region Sample Products
            var sampleProduct = new Product
            {
                Id = 1,
                Name = "X-Biscuit",
                CategoryId = 1,
                CurrentStockQuantity = 199,
                CreatedAt = T0.AddDays(-365)
            };
            #endregion


            #region Sample Orders
            var sampleOrder1 = new Order
            {
                Id = 1,
                UserId = 1,
                CreatedAt = T0,
                AdressId = 3
            };

            #endregion
            #region Sample OrderItems
            var sampleOrderItem1 = new OrderItem
            {
                Id = 1,
                ProductId = 1,
                Quantity = 5,
                OrderId = 1,
                CreatedAt = T0.AddDays(-25),
                ShippingDate = T0.AddDays(-23)
            };

            #endregion

            modelBuilder.Entity<User>()
                .HasData(sampleUser);
            modelBuilder.Entity<UserAdress>()
                .HasData(sampleUserAdress1, sampleUserAdress2, sampleUserAdress3);
            modelBuilder.Entity<Category>()
                .HasData(sampleCategory);
            modelBuilder.Entity<Product>()
                .HasData(sampleProduct);
            modelBuilder.Entity<Order>()
                .HasData(sampleOrder1);
            modelBuilder.Entity<OrderItem>()
                .HasData(sampleOrderItem1);
            #endregion

        }
    }
}
