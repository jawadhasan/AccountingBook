using System;
using System.Collections.Generic;
using System.Text;
using AccountingBook.Core;
using AccountingBook.Core.Enums;
using AccountingBook.Core.Financial;
using Microsoft.EntityFrameworkCore;

namespace AccountingBook.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<Account> Accounts { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Setup mappings here

            //Company
            modelBuilder.Entity<Company>(entity =>
            {
                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.ShortName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.CompanyCode).IsRequired().HasMaxLength(5);

            });

            //Account
            modelBuilder.Entity<Account>(entity =>
            {
                entity.Property(e => e.AccountName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.AccountCode)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.Property(e => e.AccountType).IsRequired();
                entity.Property(e => e.DrOrCrSide).IsRequired();

            });





            #region seed-data

            //Seed Company
            var company = new Company("hexquote.com","hexquote", "C001");
            company.Id = 1; //consider using negative values

            modelBuilder.Entity<Company>()
                .HasData(
                    company
                );


            //Seed Parent-Accounts
            var assetAccount = new Account
            {
                Id = 1,
                AccountCode = 10000,
                AccountName = "Assets",
                AccountType = AccountType.Assets,
                DrOrCrSide = DrOrCrSide.Dr
            };
            var liabilitiesAccount = new Account
            {
                Id = 2,
                AccountCode = 20000,
                AccountName = "Liabilities",
                AccountType = AccountType.Liabilities,
                DrOrCrSide = DrOrCrSide.Cr
            };
            var equitiesAccount = new Account
            {
                Id = 3,
                AccountCode = 30000,
                AccountName = "Equity",
                AccountType = AccountType.Equity,
                DrOrCrSide = DrOrCrSide.Cr
            };
            var revenueAccount = new Account
            {
                Id = 4,
                AccountCode = 40000,
                AccountName = "Revenue",
                AccountType = AccountType.Revenue,
                DrOrCrSide = DrOrCrSide.Cr
            };
            var expenseAccount = new Account
            {
                Id = 5,
                AccountCode = 50000,
                AccountName = "Expense",
                AccountType = AccountType.Expense,
                DrOrCrSide = DrOrCrSide.Dr
            };


            modelBuilder.Entity<Account>().HasData(
                assetAccount,
                liabilitiesAccount,
                equitiesAccount,
                revenueAccount,
                expenseAccount

            );

            #endregion
            base.OnModelCreating(modelBuilder);
        }
    }
}
