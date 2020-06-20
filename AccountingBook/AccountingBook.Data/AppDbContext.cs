using System;
using System.Collections.Generic;
using System.Text;
using AccountingBook.Core;
using Microsoft.EntityFrameworkCore;

namespace AccountingBook.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Setup mappings here
            modelBuilder.Entity<Company>(entity =>
            {
                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.ShortName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.CompanyCode).IsRequired().HasMaxLength(5);

            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
