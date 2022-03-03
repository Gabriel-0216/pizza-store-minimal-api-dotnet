using Microsoft.EntityFrameworkCore;
using PizzaStore.Infra.Mappings;
using PizzaStore.Models;

namespace PizzaStore.Infra;

public class AppDbContext : DbContext
{
    public DbSet<Model.Product> Products { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options ) : base (options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProductMap());
    }
}