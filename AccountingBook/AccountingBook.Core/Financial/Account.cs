using System.Collections.Generic;
using System.Linq;
using AccountingBook.Core.Enums;

namespace AccountingBook.Core.Financial
{
    public class Account : EntityBase
    {
        //ctor
        public Account()
        {
            ChildAccounts = new List<Account>();
            GeneralLedgerLines = new List<GeneralLedgerLine>();
        }
        public string AccountName { get; set; }
        public int AccountCode { get; set; }
        public DrOrCrSide DrOrCrSide { get; set; }
        public AccountType AccountType { get; set; }

        //FK to parent account
        public long? ParentAccountId { get; set; }
        public virtual Account ParentAccount { get; set; }

        //An Account can have child accounts
        public virtual ICollection<Account> ChildAccounts { get; set; }
        public virtual ICollection<GeneralLedgerLine> GeneralLedgerLines { get; set; }

        public decimal Balance => GetBalance();
        public decimal DebitBalance => GetDebitCreditBalance(DrOrCrSide.Dr);
        public decimal CreditBalance => GetDebitCreditBalance(DrOrCrSide.Cr);

        public void AddChildAccount(Account account)
        {
            ChildAccounts.Add(account);
        }
       public bool CanPost()
        {
            return ChildAccounts == null || ChildAccounts.Count <= 0;
        }

       private decimal GetBalance()
       {
           if (ParentAccountId == null)
           {
               //top-level accounts
               var val = ChildAccounts.Sum(c => c.Balance);
               return val;
           }
           var drAmount = GeneralLedgerLines.Where(l => l.DrCr == DrOrCrSide.Dr).Sum(l => l.Amount);
           var crAmount = GeneralLedgerLines.Where(l => l.DrCr == DrOrCrSide.Cr).Sum(l => l.Amount);

           var balance = DrOrCrSide == DrOrCrSide.Dr ? drAmount - crAmount : crAmount - drAmount;
           return balance;
       }

       private decimal GetDebitCreditBalance(DrOrCrSide side)
       {
           return side == DrOrCrSide.Dr ?
               GeneralLedgerLines.Where(l => l.DrCr == DrOrCrSide.Dr).Sum(l => l.Amount) :
               GeneralLedgerLines.Where(l => l.DrCr == DrOrCrSide.Cr).Sum(l => l.Amount);
       }
    }
}