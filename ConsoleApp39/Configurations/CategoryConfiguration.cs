using ConsoleApp39.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp39.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Property(x => x.CategoryName)
            .HasMaxLength(100)
            .HasDefaultValue("No category name")
            .IsRequired();

        builder.HasData(
             new Category { Id = 1, CategoryName = "Electronics" },
             new Category { Id = 2, CategoryName = "Clothing" },
             new Category { Id = 3, CategoryName = "Books" },
             new Category { Id = 4, CategoryName = "Sports" },
             new Category { Id = 5, CategoryName = "Home" }
         );
    }
}
