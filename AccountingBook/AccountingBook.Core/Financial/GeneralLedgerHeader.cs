using System;
using System.Collections.Generic;
using System.Linq;
using AccountingBook.Core.Enums;

namespace AccountingBook.Core.Financial
{
    public class GeneralLedgerHeader : EntityBase
    {
        //ctor
        public GeneralLedgerHeader()
        {
            Date = DateTime.UtcNow;
            GeneralLedgerLines = new List<GeneralLedgerLine>();
        }

        public DateTime Date { get; set; }
        public string Description { get; set; }

        public virtual ICollection<GeneralLedgerLine> GeneralLedgerLines { get; set; }

        public List<GeneralLedgerLine> Assets()
        {
            var lines = GeneralLedgerLines.Where(a => a.Account.AccountType == AccountType.Assets).ToList();
            return lines;
        }
        public List<GeneralLedgerLine> Liabilities()
        {
            var lines = GeneralLedgerLines.Where(a => a.Account.AccountType == AccountType.Liabilities).ToList();
            return lines;
        }
        public List<GeneralLedgerLine> Equities()
        {
            var lines = GeneralLedgerLines.Where(a => a.Account.AccountType == AccountType.Equity).ToList();
            return lines;
        }
        public List<GeneralLedgerLine> Revenues()
        {
            var lines = GeneralLedgerLines.Where(a => a.Account.AccountType == AccountType.Revenue).ToList();
            return lines;
        }
        public List<GeneralLedgerLine> Expenses()
        {
            var lines = GeneralLedgerLines.Where(a => a.Account.AccountType == AccountType.Expense).ToList();
            return lines;
        }

        public bool HaveAtLeastTwoAccountTypes()
        {
            var grouped = GeneralLedgerLines.GroupBy(l => l.Account.AccountType);
            return grouped.Count() > 1;
        }

        /// <summary>
        /// Assets = Liabilities + Equities
        /// </summary>
        /// <returns></returns>
        public bool ValidateAccountingEquation()
        {
            var assetsAmount = Assets() != null ? Assets().Sum(a => a.Amount) : 0;
            var liabilitiesAmount = Liabilities() != null ? Liabilities().Sum(a => a.Amount) : 0;
            var equitiesAmount = Equities() != null ? Equities().Sum(a => a.Amount) : 0;

            var isEqual = assetsAmount == liabilitiesAmount + equitiesAmount;
            return isEqual;
        }

        public bool DrCrEqualityValidated()
        {
            var totalDebit = GeneralLedgerLines.Where(d => d.DrCr == DrOrCrSide.Dr).Sum(d => d.Amount);
            var totalCredit = GeneralLedgerLines.Where(d => d.DrCr == DrOrCrSide.Cr).Sum(d => d.Amount);
            return totalDebit - totalCredit == 0;
        }
        public bool NoLineAmountIsEqualToZero()
        {
            var totalDebit = GeneralLedgerLines.Where(d => d.DrCr == DrOrCrSide.Dr).Sum(d => d.Amount);
            var totalCredit = GeneralLedgerLines.Where(d => d.DrCr == DrOrCrSide.Cr).Sum(d => d.Amount);

            if (totalDebit == 0)
                return false;

            return totalCredit != 0;
        }

    }
}
