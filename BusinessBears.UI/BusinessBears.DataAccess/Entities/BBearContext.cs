using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BusinessBears.DataAccess.Entities
{
    public partial class BBearContext : DbContext
    {
        public BBearContext()
        {
        }

        public BBearContext(DbContextOptions<BBearContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Inventory> Inventory { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<SoldBears> SoldBears { get; set; }
        public virtual DbSet<SoldTraining> SoldTraining { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer", "BBear");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.DefLocationId).HasColumnName("DefLocationID");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.HasOne(d => d.DefLocation)
                    .WithMany(p => p.Customer)
                    .HasForeignKey(d => d.DefLocationId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Customer");
            });

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.ToTable("Inventory", "BBear");

                entity.Property(e => e.InventoryId).HasColumnName("InventoryID");

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Inventory)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK_Inventory1");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Inventory)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Inventory2");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("Location", "BBear");

                entity.Property(e => e.LocationId).HasColumnName("LocationID");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK_Order");

                entity.ToTable("Orders", "BBear");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.PriceTag).HasColumnType("decimal(7, 2)");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Order2");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK_Order1");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product", "BBear");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.DefPrice).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<SoldBears>(entity =>
            {
                entity.HasKey(e => e.BearId)
                    .HasName("PK_Bear");

                entity.ToTable("SoldBears", "BBear");

                entity.Property(e => e.BearId).HasColumnName("BearID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.SoldBears)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_Bears");
            });

            modelBuilder.Entity<SoldTraining>(entity =>
            {
                entity.HasKey(e => e.TrainingId)
                    .HasName("PK_Training");

                entity.ToTable("SoldTraining", "BBear");

                entity.Property(e => e.TrainingId).HasColumnName("TrainingID");

                entity.Property(e => e.BearId).HasColumnName("BearID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Bear)
                    .WithMany(p => p.SoldTraining)
                    .HasForeignKey(d => d.BearId)
                    .HasConstraintName("FK_Training");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.SoldTraining)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Training2");
            });
        }
    }
}
