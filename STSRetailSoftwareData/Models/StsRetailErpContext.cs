using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace STSRetailSoftwareData.Models;

public partial class StsRetailErpContext : DbContext
{
    private string ConnectionString { get; set; }
    public StsRetailErpContext(string conStr)
    {
        this.ConnectionString = conStr;
    }

    public StsRetailErpContext(DbContextOptions<StsRetailErpContext> options)
        : base(options)
    {
    }

    public virtual DbSet<EmployeeCredential> EmployeeCredentials { get; set; }

    public virtual DbSet<EmployeeProfile> EmployeeProfiles { get; set; }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Payroll> Payrolls { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<TimeTable> TimeTables { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(this.ConnectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmployeeCredential>(entity =>
        {
            entity.HasKey(e => e.EmpCredId).HasName("PK__Employee__3ED3552A39E82BB3");

            entity.Property(e => e.EmpCredId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(8, 0)");
            entity.Property(e => e.Password).IsUnicode(false);
        });

        modelBuilder.Entity<EmployeeProfile>(entity =>
        {
            entity.HasKey(e => e.EmpProfileId).HasName("PK__Employee__155EDE9E59F114A9");

            entity.ToTable("EmployeeProfile");

            entity.Property(e => e.EmpProfileId).HasColumnType("numeric(8, 0)");
            entity.Property(e => e.Name).IsUnicode(false);
            entity.Property(e => e.PayRate).HasColumnType("numeric(3, 2)");
            entity.Property(e => e.Role).IsUnicode(false);

            entity.HasOne(d => d.EmpProfile).WithOne(p => p.EmployeeProfile)
                .HasForeignKey<EmployeeProfile>(d => d.EmpProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EmployeeP__EmpPr__267ABA7A");
        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasKey(e => e.InventoryId).HasName("PK__Inventor__F5FDE6B3735D4C3D");

            entity.ToTable("Inventory");

            entity.Property(e => e.InventoryId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(5, 0)");
            entity.Property(e => e.Catagory)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Cost).HasColumnType("numeric(5, 2)");
            entity.Property(e => e.ItemName).IsUnicode(false);
            entity.Property(e => e.Quantity).HasColumnType("numeric(4, 2)");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Order__C3905BCF5B5AA9A1");

            entity.ToTable("Order");

            entity.Property(e => e.OrderId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(5, 0)");
            entity.Property(e => e.ItemList).IsUnicode(false);
            entity.Property(e => e.Quantity).IsUnicode(false);
        });

        modelBuilder.Entity<Payroll>(entity =>
        {
            entity.HasKey(e => e.PayrollId).HasName("PK__Payroll__99DFC672815CB23A");

            entity.ToTable("Payroll");

            entity.Property(e => e.PayrollId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(5, 0)");
            entity.Property(e => e.EmpId).HasColumnType("numeric(8, 0)");
            entity.Property(e => e.EndDate).HasColumnType("date");
            entity.Property(e => e.ManagerId).HasColumnType("numeric(8, 0)");
            entity.Property(e => e.StartDate).HasColumnType("date");

            entity.HasOne(d => d.Emp).WithMany(p => p.PayrollEmps)
                .HasForeignKey(d => d.EmpId)
                .HasConstraintName("FK__Payroll__EmpId__2C3393D0");

            entity.HasOne(d => d.Manager).WithMany(p => p.PayrollManagers)
                .HasForeignKey(d => d.ManagerId)
                .HasConstraintName("FK__Payroll__Manager__2D27B809");
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.SaleId).HasName("PK__Sale__1EE3C3FF46421372");

            entity.ToTable("Sale");

            entity.Property(e => e.SaleId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(5, 0)");
            entity.Property(e => e.EmpId).HasColumnType("numeric(8, 0)");
            entity.Property(e => e.ItemList).IsUnicode(false);
            entity.Property(e => e.Quantity).IsUnicode(false);

            entity.HasOne(d => d.Emp).WithMany(p => p.Sales)
                .HasForeignKey(d => d.EmpId)
                .HasConstraintName("FK__Sale__EmpId__33D4B598");
        });

        modelBuilder.Entity<TimeTable>(entity =>
        {
            entity.HasKey(e => e.TimeTableId).HasName("PK__TimeTabl__C087BD0A832AF5E1");

            entity.ToTable("TimeTable");

            entity.Property(e => e.TimeTableId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(5, 0)");
            entity.Property(e => e.EmpProfileId).HasColumnType("numeric(8, 0)");
            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.StartTime).HasColumnType("datetime");

            entity.HasOne(d => d.EmpProfile).WithMany(p => p.TimeTables)
                .HasForeignKey(d => d.EmpProfileId)
                .HasConstraintName("FK__TimeTable__EmpPr__29572725");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
