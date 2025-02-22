using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Data.Contexts;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<CustomerEntity> Customers { get; set; } = null!;
    public DbSet<ProductEntity> Products { get; set; } = null!;
    public DbSet<ProjectEntity> Projects { get; set; } = null!;
    public DbSet<UserEntity> Users { get; set; } = null!;
    public DbSet<StatusTypeEntity> StatusTypes { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ProjectEntity>()
            .HasOne(p => p.User)
            .WithMany(p => p.Projects)
            .HasForeignKey(p => p.UserId);

        modelBuilder.Entity<ProjectEntity>()
            .HasOne(p => p.Status)
            .WithMany(p => p.Projects)
            .HasForeignKey(p => p.StatusId);

        modelBuilder.Entity<ProjectEntity>()
            .HasOne(p => p.Customer)
            .WithMany(p=> p.Projects)
            .HasForeignKey(p => p.CustomerId);

        modelBuilder.Entity<ProjectEntity>()
            .HasOne(p => p.Product)
            .WithMany(p => p.Projects)
            .HasForeignKey(p => p.ProductId);
    }

}


