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
        public string AccountName { get; protected set; }
        public int AccountCode { get; protected set; }
        public DrOrCrSide DrOrCrSide { get; protected set; }
        public AccountType AccountType { get; set; }

        //FK to parent account
        public long? ParentAccountId { get; protected set; }
        public virtual Account ParentAccount { get; protected set; }

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







       //public virtual ICollection<GeneralLedgerLine> GeneralLedgerLines { get; set; }
       //public decimal Balance => GetBalance();
       //public decimal DebitBalance => GetDebitCreditBalance(DrOrCrSide.Dr);
       //public decimal CreditBalance => GetDebitCreditBalance(DrOrCrSide.Cr);
    }
}
