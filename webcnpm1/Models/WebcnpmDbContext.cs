using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace webcnpm1.Models;

public partial class WebcnpmDbContext : DbContext
{
    public WebcnpmDbContext()
    {
    }

    public WebcnpmDbContext(DbContextOptions<WebcnpmDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Component> Components { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Device> Devices { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<RepairComponent> RepairComponents { get; set; }

    public virtual DbSet<RepairOrder> RepairOrders { get; set; }

    public virtual DbSet<RepairService> RepairServices { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-VDMI65C;Initial Catalog=webcnpm;User ID=kien;Password=kien1109@;Encrypt=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Component>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__componen__3213E83F7A54634A");

            entity.ToTable("components");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.QuantityInStock).HasColumnName("quantity_in_stock");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__customer__3213E83FC38DBEEE");

            entity.ToTable("customers");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .HasColumnName("full_name");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
        });

        modelBuilder.Entity<Device>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__devices__3213E83F5FC7C298");

            entity.ToTable("devices");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Brand)
                .HasMaxLength(50)
                .HasColumnName("brand");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.Model)
                .HasMaxLength(100)
                .HasColumnName("model");
            entity.Property(e => e.Type)
                .HasMaxLength(20)
                .HasColumnName("type");

            entity.HasOne(d => d.Customer).WithMany(p => p.Devices)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__devices__custome__5070F446");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__invoices__3213E83F3A491333");

            entity.ToTable("invoices");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(20)
                .HasColumnName("payment_status");
            entity.Property(e => e.RepairOrderId).HasColumnName("repair_order_id");
            entity.Property(e => e.TotalAmount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total_amount");

            entity.HasOne(d => d.RepairOrder).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.RepairOrderId)
                .HasConstraintName("FK__invoices__repair__6754599E");
        });

        modelBuilder.Entity<RepairComponent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__repair_c__3213E83FE32AA1F7");

            entity.ToTable("repair_components");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ComponentId).HasColumnName("component_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.RepairOrderId).HasColumnName("repair_order_id");

            entity.HasOne(d => d.Component).WithMany(p => p.RepairComponents)
                .HasForeignKey(d => d.ComponentId)
                .HasConstraintName("FK__repair_co__compo__6383C8BA");

            entity.HasOne(d => d.RepairOrder).WithMany(p => p.RepairComponents)
                .HasForeignKey(d => d.RepairOrderId)
                .HasConstraintName("FK__repair_co__repai__628FA481");
        });

        modelBuilder.Entity<RepairOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__repair_o__3213E83F4F35BD6F");

            entity.ToTable("repair_orders");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AssignedTo).HasColumnName("assigned_to");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.DeviceId).HasColumnName("device_id");
            entity.Property(e => e.ProblemDescription).HasColumnName("problem_description");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("received")
                .HasColumnName("status");

            entity.HasOne(d => d.AssignedToNavigation).WithMany(p => p.RepairOrders)
                .HasForeignKey(d => d.AssignedTo)
                .HasConstraintName("FK__repair_or__assig__571DF1D5");

            entity.HasOne(d => d.Customer).WithMany(p => p.RepairOrders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__repair_or__custo__5535A963");

            entity.HasOne(d => d.Device).WithMany(p => p.RepairOrders)
                .HasForeignKey(d => d.DeviceId)
                .HasConstraintName("FK__repair_or__devic__5629CD9C");
        });

        modelBuilder.Entity<RepairService>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__repair_s__3213E83FEDC687D6");

            entity.ToTable("repair_services");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Quantity)
                .HasDefaultValue(1)
                .HasColumnName("quantity");
            entity.Property(e => e.RepairOrderId).HasColumnName("repair_order_id");
            entity.Property(e => e.ServiceId).HasColumnName("service_id");

            entity.HasOne(d => d.RepairOrder).WithMany(p => p.RepairServices)
                .HasForeignKey(d => d.RepairOrderId)
                .HasConstraintName("FK__repair_se__repai__5CD6CB2B");

            entity.HasOne(d => d.Service).WithMany(p => p.RepairServices)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK__repair_se__servi__5DCAEF64");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__services__3213E83FD51A5637");

            entity.ToTable("services");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__users__3213E83F98FDBE32");

            entity.ToTable("users");

            entity.HasIndex(e => e.Username, "UQ__users__F3DBC5725BD89AC0").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .HasColumnName("full_name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .HasColumnName("role");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
