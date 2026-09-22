using ConsoleApp39.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp39.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(x => x.ProductName)
            .HasMaxLength(100)
            .HasDefaultValue("No product name")
            .IsRequired();

        builder.Property(x => x.Price)
            .IsRequired();

        builder.Property(x => x.Stock)
            .IsRequired();

        builder.HasOne(x => x.Category)
            .WithMany(x => x.Products)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasData(
           new Product { Id = 1, ProductName = "Laptop", Price = 1800, Stock = 10, CategoryId = 1 },
           new Product { Id = 2, ProductName = "Mouse", Price = 30, Stock = 50, CategoryId = 1 },
           new Product { Id = 3, ProductName = "Keyboard", Price = 70, Stock = 35, CategoryId = 1 },

           new Product { Id = 4, ProductName = "T-Shirt", Price = 25, Stock = 100, CategoryId = 2 },
           new Product { Id = 5, ProductName = "Jeans", Price = 60, Stock = 50, CategoryId = 2 },
           new Product { Id = 6, ProductName = "Jacket", Price = 120, Stock = 25, CategoryId = 2 },

           new Product { Id = 7, ProductName = "C# Book", Price = 35, Stock = 40, CategoryId = 3 },
           new Product { Id = 8, ProductName = "SQL Book", Price = 40, Stock = 30, CategoryId = 3 },
           new Product { Id = 9, ProductName = "C++ Book", Price = 45, Stock = 20, CategoryId = 3 },

           new Product { Id = 10, ProductName = "Football", Price = 30, Stock = 60, CategoryId = 4 },
           new Product { Id = 11, ProductName = "Basketball", Price = 35, Stock = 45, CategoryId = 4 },
           new Product { Id = 12, ProductName = "Tennis Racket", Price = 100, Stock = 15, CategoryId = 4 },

           new Product { Id = 13, ProductName = "Table", Price = 250, Stock = 10, CategoryId = 5 },
           new Product { Id = 14, ProductName = "Chair", Price = 80, Stock = 30, CategoryId = 5 },
           new Product { Id = 15, ProductName = "Lamp", Price = 50, Stock = 40, CategoryId = 5 }
       );
    }
}
