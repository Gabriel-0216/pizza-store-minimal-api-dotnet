using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PizzaStore.Models;

namespace PizzaStore.Infra.Mappings;

public class ProductMap : IEntityTypeConfiguration<Model.Product>
{
    public void Configure(EntityTypeBuilder<Model.Product> builder)
    {
        builder.ToTable("Products");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        builder.Property(p => p.Description)
            .HasColumnName("Description")
            .HasColumnType("varchar")
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(p => p.Name)
            .HasColumnName("Name")
            .HasColumnType("varchar")
            .HasMaxLength(40)
            .IsRequired();
        
        builder.Property(p => p.Value)
            .HasColumnName("Value")
            .HasColumnType("decimal(5,2)")
            .IsRequired();

    }
}