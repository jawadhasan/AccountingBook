using AccountingBook.Core.Enums;

namespace AccountingBook.Core.Financial
{
    public class GeneralLedgerLine : EntityBase
    {
        public DrOrCrSide DrCr { get; set; }
        public decimal Amount { get; set; }

        //FK Account
        public long AccountId { get; set; }
        public Account Account { get; set; }

        //FK GeneralLedgerHeader
        public long GeneralLedgerHeaderId { get; set; }
        public GeneralLedgerHeader GeneralLedgerHeader { get; set; }

        public static GeneralLedgerLine Create(Account account, DrOrCrSide drOrCr, decimal amount)
        {
            //Validation Rules: TODO
            //Account can not be null
            //Amount can not be zero or less
            //DrCr should be valid

            var line = new GeneralLedgerLine
            {
                AccountId = account.Id,
                Account = account,
                DrCr = drOrCr,
                Amount = amount
            };
            return line;
        }
    }
}
