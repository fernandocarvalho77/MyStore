using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyStore.Models;

namespace MyStore.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; } = default!;
    public DbSet<Product> Products { get; set; } = default!;
    public DbSet<Sale> Sales { get; set; } = default!;
    public DbSet<Client> Clients { get; set; } = default!;
    public DbSet<SaleProduct> SaleProducts { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Category>()
            .HasOne(c => c.ParentCategory)
            .WithMany(c => c.SubCategories)
            .HasForeignKey(c => c.ParentCategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Product>()
            .Property(p => p.FinalPrice)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Sale>()
            .Property(s => s.FinalValue)
            .HasPrecision(18, 2);
        
        modelBuilder.Entity<SaleProduct>()
            .HasKey(sp => new { sp.SaleId, sp.ProductId });
    }
}