using System;
using System.Collections.Generic;
using System.Text;
using AccountingBook.Core.Enums;

namespace AccountingBook.Core.Financial
{
    public class JournalEntryLine : EntityBase
    {
        public DrOrCrSide DrCr { get; set; }
        public decimal Amount { get; set; }
        public string Memo { get; set; }

        //FK Account
        public long AccountId { get; set; }
        public virtual Account Account { get; set; }

        //Fk JournalEntryHeader
        public long JournalEntryHeaderId { get; set; }
        public virtual JournalEntryHeader JournalEntryHeader { get; set; }
    }
}
