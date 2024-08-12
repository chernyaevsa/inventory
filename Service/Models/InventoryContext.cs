using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Service.Models;

public partial class InventoryContext : DbContext
{
    private string connectionString = "server=inventory;user=root;password=megapass;database=inventory";
    public InventoryContext(IConfiguration configuration)
    {
        string? server = configuration.GetValue<string>(Constants.ServiceDBServerName);
        string? user = configuration.GetValue<string>(Constants.ServiceDBUserName);
        string? password = configuration.GetValue<string>(Constants.ServiceDBPasswordName);
        string? database = configuration.GetValue<string>(Constants.ServiceDBDatabaseName);
        string? port = configuration.GetValue<string>(Constants.ServiceDBPortName);
        connectionString = $"server={server};port={port};user={user};password={password};database={database}";
    }

    public InventoryContext(DbContextOptions<InventoryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Building> Buildings { get; set; }

    public virtual DbSet<Cabinet> Cabinets { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Equipment> Equipment { get; set; }

    public virtual DbSet<Range> Ranges { get; set; }

    public virtual DbSet<Responsible> Responsibles { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Type> Types { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL(connectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Building>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("buildings");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(45)
                .HasColumnName("address");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Cabinet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("cabinets");

            entity.HasIndex(e => e.BuildingId, "fk_cabinets_1_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BuildingId).HasColumnName("building_id");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");

            entity.HasOne(d => d.Building).WithMany(p => p.Cabinets)
                .HasForeignKey(d => d.BuildingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_cabinets_1");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("employees");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
            entity.Property(e => e.Patronymic)
                .HasMaxLength(45)
                .HasColumnName("patronymic");
            entity.Property(e => e.Surname)
                .HasMaxLength(45)
                .HasColumnName("surname");
        });

        modelBuilder.Entity<Equipment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("equipment");

            entity.HasIndex(e => e.CabinetId, "fk_equipment_1_idx");

            entity.HasIndex(e => e.TypeId, "fk_equipment_2_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CabinetId).HasColumnName("cabinet_id");
            entity.Property(e => e.Count).HasColumnName("count");
            entity.Property(e => e.Name)
                .HasMaxLength(85)
                .HasColumnName("name");
            entity.Property(e => e.Number)
                .HasMaxLength(45)
                .HasColumnName("number");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.TypeId).HasColumnName("type_id");
            entity.Property(e => e._1cKod)
                .HasMaxLength(45)
                .HasColumnName("1c_kod");

            entity.HasOne(d => d.Cabinet).WithMany(p => p.Equipment)
                .HasForeignKey(d => d.CabinetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_equipment_1");

            entity.HasOne(d => d.Type).WithMany(p => p.Equipment)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_equipment_2");
        });

        modelBuilder.Entity<Range>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("ranges");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DatetimeFrom)
                .HasColumnType("datetime")
                .HasColumnName("datetime_from");
            entity.Property(e => e.DatetimeTo)
                .HasColumnType("datetime")
                .HasColumnName("datetime_to");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Responsible>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("responsible");

            entity.HasIndex(e => e.EmployeeId, "fk_responsible_1_idx");

            entity.HasIndex(e => e.CabinetId, "fk_responsible_2_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CabinetId).HasColumnName("cabinet_id");
            entity.Property(e => e.Datetime)
                .HasColumnType("datetime")
                .HasColumnName("datetime");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

            entity.HasOne(d => d.Cabinet).WithMany(p => p.Responsibles)
                .HasForeignKey(d => d.CabinetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_responsible_2");

            entity.HasOne(d => d.Employee).WithMany(p => p.Responsibles)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_responsible_1");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("statuses");

            entity.HasIndex(e => e.EquipmentId, "fk_statuses_1_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Datetime)
                .HasMaxLength(45)
                .HasColumnName("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(45)
                .HasColumnName("description");
            entity.Property(e => e.EquipmentId).HasColumnName("equipment_id");
            entity.Property(e => e.Status1).HasColumnName("status");
            entity.Property(e => e.UserName)
                .HasMaxLength(45)
                .HasColumnName("user_name");

            entity.HasOne(d => d.Equipment).WithMany(p => p.Statuses)
                .HasForeignKey(d => d.EquipmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_statuses_1");
        });

        modelBuilder.Entity<Type>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("types");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
