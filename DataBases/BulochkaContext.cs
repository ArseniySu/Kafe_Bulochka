using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Kafe_Bulochka.DataBases;

public partial class BulochkaContext : DbContext
{
    public BulochkaContext()
    {
    }

    public BulochkaContext(DbContextOptions<BulochkaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CategoryDish> CategoryDishes { get; set; }

    public virtual DbSet<Change> Changes { get; set; }

    public virtual DbSet<Dish> Dishes { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderedDish> OrderedDishes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-6697S0P\\SQLEXPRESS;Database=bulochka;Trusted_Connection=True;Encrypt=False;");

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//    => optionsBuilder.UseSqlServer("Server=DESKTOP-IVCQ0VD;Database=bulochka;Trusted_Connection=True;Encrypt=False;");


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CategoryDish>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Category__3214EC0736FF92B1");

            entity.Property(e => e.Tittle).HasMaxLength(50);
        });

        modelBuilder.Entity<Change>(entity =>
        {
            entity.HasKey(e => new { e.DateChanges, e.UsersId }).HasName("PK__Changes___5294F81F9EE9D03E");

            entity.ToTable("Changes_");

            entity.Property(e => e.DateChanges).HasColumnType("date");
            entity.Property(e => e.UsersId).HasColumnName("UsersID");
            entity.Property(e => e.ChangesTitle).HasMaxLength(50);

            entity.HasOne(d => d.Users).WithMany(p => p.Changes)
                .HasForeignKey(d => d.UsersId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Changes___UsersI__5535A963");
        });

        modelBuilder.Entity<Dish>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Dishes__3214EC07EFF9B5CC");

            entity.Property(e => e.CategoryDishesId).HasColumnName("CategoryDishesID");
            entity.Property(e => e.Prise).HasColumnType("money");
            entity.Property(e => e.Tittle).HasMaxLength(50);

            entity.HasOne(d => d.CategoryDishes).WithMany(p => p.Dishes)
                .HasForeignKey(d => d.CategoryDishesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Dishes__Category__4BAC3F29");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Orders__3214EC0782CD53C5");

            entity.Property(e => e.CookingStatus).HasMaxLength(50);
            entity.Property(e => e.DateTimes).HasColumnType("datetime");
            entity.Property(e => e.OrderStatus).HasMaxLength(50);
            entity.Property(e => e.PaymentMethod).HasMaxLength(50);
            entity.Property(e => e.Tables).HasColumnName("Tables_");

            entity.HasOne(d => d.WaiterNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Waiter)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__Waiter__4E88ABD4");
        });

        modelBuilder.Entity<OrderedDish>(entity =>
        {
            entity.HasKey(e => new { e.OrdersId, e.DishesId }).HasName("PK__OrderedD__8771190024C84740");

            entity.Property(e => e.OrdersId).HasColumnName("OrdersID");
            entity.Property(e => e.DishesId).HasColumnName("DishesID");

            entity.HasOne(d => d.Dishes).WithMany(p => p.OrderedDishes)
                .HasForeignKey(d => d.DishesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderedDi__Dishe__52593CB8");

            entity.HasOne(d => d.Orders).WithMany(p => p.OrderedDishes)
                .HasForeignKey(d => d.OrdersId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderedDi__Order__5165187F");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3213E83FA2D84066");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Tittle).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07B8920A2B");

            entity.Property(e => e.Fname).HasMaxLength(50);
            entity.Property(e => e.Logins)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Mname).HasMaxLength(50);
            entity.Property(e => e.Passwords)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Sname).HasMaxLength(50);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__RoleId__398D8EEE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
