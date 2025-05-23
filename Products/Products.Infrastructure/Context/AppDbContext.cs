using Microsoft.EntityFrameworkCore;
using Products.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Products.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>(r =>
            {
                r.HasKey(r => r.Id);

                r.Property(r => r.Name)
                    .IsRequired()
                    .HasMaxLength(300);

                r.Property(r => r.Code)                    
                    .IsRequired()
                    .HasMaxLength(300);

                r.HasIndex(u => u.Code)
                    .IsUnique();
            });
        }
        public DbSet<Product> Products { get; set; }
    }
}
