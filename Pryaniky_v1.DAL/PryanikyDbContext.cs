using Microsoft.EntityFrameworkCore;
using Pryaniky_v1.DAL.Domain;

namespace Pryaniky_v1.DAL
{
	public class PryanikyDbContext(DbContextOptions<PryanikyDbContext> options) : DbContext(options)
	{
		public DbSet<Order> Orders { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<OrderItem> OrderItems { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.HasDefaultSchema("PryanikyShop");

			modelBuilder.Entity<OrderItem>(e =>
			{
				e.ToTable("OrderItems");
				
				e.HasKey(p => p.Id);
			});

			modelBuilder.Entity<Order>(e =>
			{
				e.ToTable("Orders");

				e.HasKey(p => p.Id);

				e.Property(p => p.CustomerName).IsRequired().HasMaxLength(100);

				e.Property(p => p.CustomerPhone).IsRequired().HasColumnType("decimal(11,0)");

				e.Property(p => p.OrderDateTime).IsRequired();

				e.HasMany(p => p.OrderItems).WithOne(p => p.Order).HasForeignKey(p => p.OrderId).IsRequired(false).OnDelete(DeleteBehavior.Cascade);
			});

			modelBuilder.Entity<Product>(e =>
			{
				e.ToTable(t =>
				{
					t.Metadata.SetTableName("Products");
					t.HasCheckConstraint("CK_Product_ProductName", "ProductName <> '' and ProductName is not null");
				});

				e.HasKey(p => p.Id).IsClustered();

				e.Property(p => p.ProductName).IsRequired().HasMaxLength(100);

				e.Property(p => p.Price).IsRequired();

				e.Property(p => p.Quantity).IsRequired();

				e.HasMany(p => p.OrderItems).WithOne(p => p.Product).HasForeignKey(p => p.ProductId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);
			});

			modelBuilder.Entity<Product>().HasIndex(p => p.ProductName).IsUnique();

			base.OnModelCreating(modelBuilder);
		}
	}
}
