using System.Collections.Generic;
using AccountingBook.Core.Enums;

namespace AccountingBook.Core.Financial
{
    public class Account : EntityBase
    {
        //ctor
        public Account()
        {
            ChildAccounts = new List<Account>();
            //GeneralLedgerLines = new List<GeneralLedgerLine>();

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

       public void AddChildAccount(Account account)
        {
            ChildAccounts.Add(account);
        }
       public bool CanPost()
        {
            return ChildAccounts == null || ChildAccounts.Count <= 0;
        }
    }
}








//public virtual ICollection<GeneralLedgerLine> GeneralLedgerLines { get; set; }
//public decimal Balance => GetBalance();
//public decimal DebitBalance => GetDebitCreditBalance(DrOrCrSide.Dr);
//public decimal CreditBalance => GetDebitCreditBalance(DrOrCrSide.Cr);
