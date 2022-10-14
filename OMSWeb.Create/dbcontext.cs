//using Microsoft.EntityFrameworkCore;
//using OMS.Data.Access.DAL;
//using OMS.Data.Model;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace OMSWeb.Data.Access.DAL
//{
//    public class dbcontext : DbContext
//    {

//        public DbSet<Category> Categories { get; set; }
//        public DbSet<Product> Products { get; set; }
//        public DbSet<Order> Orders { get; set; }
//        public DbSet<OrderDetails> OrderDetails { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=OMSWeb;Trusted_Connection=True;");
//        }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {

//            var mappings = MappingsHelper.GetMainMappings();

//            foreach (var mapping in mappings)
//            {
//                mapping.Visit(modelBuilder);
//            }

//            modelBuilder.Entity<OrderDetails>(entity =>
//            {
//                entity.HasKey(e => new { e.OrderId, e.ProductId });

//                entity.Property(e => e.OrderId).HasColumnType("int").HasColumnName("OrderID");

//                entity.Property(e => e.ProductId).HasColumnType("int").HasColumnName("ProductID");

//                entity.Property(e => e.Discount).HasColumnType("real").HasColumnName("Discount");

//                entity.Property(e => e.Quantity).HasColumnType("smallint").HasDefaultValueSql("1");

//                entity.Property(e => e.UnitPrice) .IsRequired() .HasColumnType("money").HasDefaultValueSql("0");
//            });


//            modelBuilder.Entity<Category>(entity =>
//            {
//                entity.Property(e => e.CategoryName).IsRequired().HasColumnType("nvarchar(15)");

//                entity.Property(e => e.Description).HasColumnType("ntext");

//                entity.Property(e => e.Picture).HasColumnType("image");
//            });


//            modelBuilder.Entity<Product>(entity =>
//            {
//                entity.Property(e => e.CategoryId).HasColumnType("int").HasColumnName("CategoryID");

//                entity.Property(e => e.Discontinued).IsRequired().HasColumnType("bit");

//                entity.Property(e => e.ProductName).IsRequired().HasColumnType("nvarchar(40)");

//                entity.Property(e => e.QuantityPerUnit).HasColumnType("nvarchar(20)");

//                entity.Property(e => e.ReorderLevel).HasColumnType("smallint");

//                entity.Property(e => e.SupplierId).HasColumnType("int").HasColumnName("SupplierID");

//                entity.Property(e => e.UnitPrice).HasColumnType("money");

//                entity.Property(e => e.UnitsInStock).HasColumnType("smallint");

//                entity.Property(e => e.UnitsOnOrder).HasColumnType("smallint");

//                entity.HasOne(d => d.Category).WithMany(p => p.Products).HasForeignKey(d => d.CategoryId);

//            });


//            modelBuilder.Entity<Order>(entity =>
//            {
//                entity.Property(e => e.OrderId).HasColumnType("int").HasColumnName("OrderID");

//                entity.Property(e => e.CustomerId).HasColumnType("nchar(5)").HasColumnName("CustomerID");

//                entity.Property(e => e.EmployeeId).HasColumnType("int").HasColumnName("EmployeeID");

//                entity.Property(e => e.Freight).HasColumnType("money").HasDefaultValueSql("0");

//                entity.Property(e => e.OrderDate).HasColumnType("datetime");

//                entity.Property(e => e.RequiredDate).HasColumnType("datetime");

//                entity.Property(e => e.ShipAddress).HasColumnType("nvarchar(60)");

//                entity.Property(e => e.ShipCity).HasColumnType("nvarchar(15)");

//                entity.Property(e => e.ShipCountry).HasColumnType("nvarchar(15)");

//                entity.Property(e => e.ShipName).HasColumnType("nvarchar(40)");

//                entity.Property(e => e.ShipPostalCode).HasColumnType("nvarchar(10)");

//                entity.Property(e => e.ShipRegion).HasColumnType("nvarchar(15)");

//                entity.Property(e => e.ShipVia).HasColumnType("int");

//                entity.Property(e => e.ShippedDate).HasColumnType("datetime");

//            });
//        }

//    }
//}
