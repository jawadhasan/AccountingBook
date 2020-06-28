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
        public DbSet<JournalEntryHeader> JournalEntryHeaders { get; set; }
        public DbSet<JournalEntryLine> JournalEntryLines { get; set; }
        public DbSet<GeneralLedgerHeader> GeneralLedgerHeaders { get; set; }
        public DbSet<GeneralLedgerLine> GeneralLedgerLines { get; set; }

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

                entity.HasIndex(e=> e.AccountCode).IsUnique();

                entity.Ignore(a => a.Balance);
                entity.Ignore(a => a.DebitBalance);
                entity.Ignore(a => a.CreditBalance);

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


            //Seed Child-Accounts


            //1. Assets
            var checkingAccount = new Account
            {
                Id = 6,
                AccountCode = 10111,
                AccountName = "Regular Checking Account",
                AccountType = AccountType.Assets,
                DrOrCrSide = DrOrCrSide.Dr,
                ParentAccountId = assetAccount.Id
            };
            var savingAccount = new Account
            {
                Id = 7,
                AccountCode = 10112,
                AccountName = "Savings Account",
                AccountType = AccountType.Assets,
                DrOrCrSide = DrOrCrSide.Dr,
                ParentAccountId = assetAccount.Id
            };
            var cashInHandAccount = new Account
            {
                Id = 8,
                AccountCode = 10113,
                AccountName = "Cash in Hand A/C",
                AccountType = AccountType.Assets,
                DrOrCrSide = DrOrCrSide.Dr,
                ParentAccountId = assetAccount.Id
            };
            var accountReceivable = new Account
            {
                Id = 9,
                AccountCode = 10120,
                AccountName = "Accounts Receivable",
                AccountType = AccountType.Assets,
                DrOrCrSide = DrOrCrSide.Dr,
                ParentAccountId = assetAccount.Id
            };
            var prePaidExpensesAccount = new Account
            {
                Id = 10,
                AccountCode = 10140,
                AccountName = "Prepaid Expenses",
                AccountType = AccountType.Assets,
                DrOrCrSide = DrOrCrSide.Dr,
                ParentAccountId = assetAccount.Id
            };
            var employeeAdvancesAccount = new Account
            {
                Id = 11,
                AccountCode = 10150,
                AccountName = "Employee Advances",
                AccountType = AccountType.Assets,
                DrOrCrSide = DrOrCrSide.Dr,
                ParentAccountId = assetAccount.Id
            };
            var inventoryAccount = new Account
            {
                Id = 12,
                AccountCode = 10800,
                AccountName = "Inventory",
                AccountType = AccountType.Assets,
                DrOrCrSide = DrOrCrSide.Dr,
                ParentAccountId = assetAccount.Id
            };
            var goodsReceivedClearingAccount = new Account
            {
                Id = 13,
                AccountCode = 10810,
                AccountName = "Goods Received Clearing Account",
                AccountType = AccountType.Assets,
                DrOrCrSide = DrOrCrSide.Dr,
                ParentAccountId = assetAccount.Id
            };

            //2. Liabilities
            var accountPayable = new Account
            {
                Id = 14,
                AccountCode = 20110,
                AccountName = "Account Payable",
                AccountType = AccountType.Liabilities,
                DrOrCrSide = DrOrCrSide.Cr,
                ParentAccountId = liabilitiesAccount.Id
            };
            var customerAdvances = new Account
            {
                Id = 15,
                AccountCode = 20120,
                AccountName = "Customer Advances",
                AccountType = AccountType.Liabilities,
                DrOrCrSide = DrOrCrSide.Cr,
                ParentAccountId = liabilitiesAccount.Id
            };
            var wagesPayable = new Account
            {
                Id = 16,
                AccountCode = 20202,
                AccountName = "Wages Payable",
                AccountType = AccountType.Liabilities,
                DrOrCrSide = DrOrCrSide.Cr,
                ParentAccountId = liabilitiesAccount.Id
            };
            var salesTaxAccount = new Account
            {
                Id = 17,
                AccountCode = 20300,
                AccountName = "Sales Tax",
                AccountType = AccountType.Liabilities,
                DrOrCrSide = DrOrCrSide.Cr,
                ParentAccountId = liabilitiesAccount.Id
            };

            //3. Equities
            var memberCapitalAccount = new Account
            {
                Id = 18,
                AccountCode = 30100,
                AccountName = "Member Capital",
                AccountType = AccountType.Equity,
                DrOrCrSide = DrOrCrSide.Cr,
                ParentAccountId = equitiesAccount.Id
            };
            var capitalSurplusAccount = new Account
            {
                Id = 19,
                AccountCode = 30200,
                AccountName = "Capital Surplus",
                AccountType = AccountType.Equity,
                DrOrCrSide = DrOrCrSide.Cr,
                ParentAccountId = equitiesAccount.Id
            };
            var retainedSurplusAccount = new Account
            {
                Id = 20,
                AccountCode = 30300,
                AccountName = "Retained Surplus",
                AccountType = AccountType.Equity,
                DrOrCrSide = DrOrCrSide.Cr,
                ParentAccountId = equitiesAccount.Id
            };
            var accumulatedProfitsAccount = new Account
            {
                Id = 21,
                AccountCode = 30400,
                AccountName = "Accumulated Profits",
                AccountType = AccountType.Equity,
                DrOrCrSide = DrOrCrSide.Cr,
                ParentAccountId = equitiesAccount.Id
            };
            var accumulatedLosses = new Account
            {
                Id = 22,
                AccountCode = 30500,
                AccountName = "Accumulated Losses",
                AccountType = AccountType.Equity,
                DrOrCrSide = DrOrCrSide.Cr,
                ParentAccountId = equitiesAccount.Id
            };

            //4. Revenue
            var salesAccount = new Account
            {
                Id = 23,
                AccountCode = 40100,
                AccountName = "Sales A/C",
                AccountType = AccountType.Revenue,
                DrOrCrSide = DrOrCrSide.Cr,
                ParentAccountId = revenueAccount.Id
            };
            var salesDiscountsAccount = new Account
            {
                Id = 24,
                AccountCode = 40200,
                AccountName = "Sales Discounts",
                AccountType = AccountType.Revenue,
                DrOrCrSide = DrOrCrSide.Cr,
                ParentAccountId = revenueAccount.Id
            };
            var shippingAndHandlingAccount = new Account
            {
                Id = 25,
                AccountCode = 40500,
                AccountName = "Shipping and Handling",
                AccountType = AccountType.Revenue,
                DrOrCrSide = DrOrCrSide.Cr,
                ParentAccountId = revenueAccount.Id
            };

            //5. Expenses
            var salaryExpensesAccount = new Account
            {
                Id = 26,
                AccountCode = 50101,
                AccountName = "Salary Expenses",
                AccountType = AccountType.Expense,
                DrOrCrSide = DrOrCrSide.Dr,
                ParentAccountId = expenseAccount.Id
            };
            var purchaseAccount = new Account
            {
                Id = 27,
                AccountCode = 50200,
                AccountName = "Purchase A/C",
                AccountType = AccountType.Expense,
                DrOrCrSide = DrOrCrSide.Dr,
                ParentAccountId = expenseAccount.Id
            };
            var costOfGoodsSoldAccount = new Account
            {
                Id = 28,
                AccountCode = 50300,
                AccountName = "Cost of Goods Sold",
                AccountType = AccountType.Expense,
                DrOrCrSide = DrOrCrSide.Dr,
                ParentAccountId = expenseAccount.Id
            };
            var purchaseDiscountAccount = new Account
            {
                Id = 29,
                AccountCode = 50400,
                AccountName = "Purchase Discounts",
                AccountType = AccountType.Expense,
                DrOrCrSide = DrOrCrSide.Dr,
                ParentAccountId = expenseAccount.Id
            };
            var purchasePriceVarianceAccount = new Account
            {
                Id = 30,
                AccountCode = 50500,
                AccountName = "Purchase price Variance",
                AccountType = AccountType.Expense,
                DrOrCrSide = DrOrCrSide.Dr,
                ParentAccountId = expenseAccount.Id
            };
            var otherExpensesAccount = new Account
            {
                Id = 31,
                AccountCode = 50600,
                AccountName = "Other Expenses",
                AccountType = AccountType.Expense,
                DrOrCrSide = DrOrCrSide.Dr,
                ParentAccountId = expenseAccount.Id
            };
            var purchaseTaxAccount = new Account
            {
                Id = 32,
                AccountCode = 50700,
                AccountName = "Purchase Tax",
                AccountType = AccountType.Expense,
                DrOrCrSide = DrOrCrSide.Dr,
                ParentAccountId = expenseAccount.Id
            };


            modelBuilder.Entity<Account>().HasData(
                checkingAccount,
                savingAccount,
                cashInHandAccount,
                accountReceivable,
                prePaidExpensesAccount,
                employeeAdvancesAccount,
                inventoryAccount,
                goodsReceivedClearingAccount,

                accountPayable,
                customerAdvances,
                wagesPayable,
                salesTaxAccount,

                memberCapitalAccount,
                capitalSurplusAccount,
                retainedSurplusAccount,
                accumulatedProfitsAccount,
                accumulatedLosses,

                salesAccount,
                salesDiscountsAccount,
                shippingAndHandlingAccount,

                salaryExpensesAccount,
                purchaseAccount,
                costOfGoodsSoldAccount,
                purchaseDiscountAccount,
                purchasePriceVarianceAccount,
                otherExpensesAccount,
                purchaseTaxAccount

            );

            #endregion
            base.OnModelCreating(modelBuilder);
        }
    }
}
