using ConsoleApp39.Configurations;
using ConsoleApp39.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp39.Context;

public class AppDBContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string cs = @"Data Source=JAVID\SQLEXPRESS;Initial Catalog=Dapper;Integrated Security=True;Connect Timeout=30;TrustServerCertificate=True;";
        optionsBuilder.UseSqlServer(cs);
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration<Category>(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration<Product>(new ProductConfiguration());

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
}
