using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace kursach.Models;

public partial class KursachContext : DbContext
{
    public KursachContext()
    {
    }

    public KursachContext(DbContextOptions<KursachContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Info> Infos { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Доходы> Доходыs { get; set; }

    public virtual DbSet<Расходы> Расходыs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost; Database=kursach; Trusted_Connection=True; MultipleActiveResultSets=true; TrustServerCertificate=true;encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Info>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Категория расходов");

            entity.ToTable("Info");

            entity.HasOne(d => d.IdDohodNavigation).WithMany(p => p.Infos)
                .HasForeignKey(d => d.IdDohod)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Info_Доходы");

            entity.HasOne(d => d.IdRashodNavigation).WithMany(p => p.Infos)
                .HasForeignKey(d => d.IdRashod)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Info_Расходы");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.RoleName)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.UserLogin)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.UserName)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.UserPassword)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.UserSurname)
                .HasMaxLength(10)
                .IsFixedLength();

            entity.HasOne(d => d.UserRoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.UserRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Role");
        });

        modelBuilder.Entity<Доходы>(entity =>
        {
            entity.HasKey(e => e.IdDohod);

            entity.ToTable("Доходы");

            entity.Property(e => e.ДатаДохода).HasColumnName("Дата дохода");
            entity.Property(e => e.КатегорияДохода)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("Категория дохода");
            entity.Property(e => e.НазваниеДохода)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("Название дохода");
            entity.Property(e => e.СуммаДохода).HasColumnName("Сумма дохода");
        });

        modelBuilder.Entity<Расходы>(entity =>
        {
            entity.HasKey(e => e.IdRashod);

            entity.ToTable("Расходы");

            entity.Property(e => e.ДатаРасхода).HasColumnName("Дата расхода");
            entity.Property(e => e.КатегорияРасхода)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("Категория расхода");
            entity.Property(e => e.НазваниеРасхода)
                .HasMaxLength(50)
                .HasColumnName("Название расхода");
            entity.Property(e => e.СуммаРасхода).HasColumnName("Сумма расхода");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
