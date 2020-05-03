﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AccountingBook.Core.Financial
{
    public class JournalEntryHeader : EntityBase
    {
        public JournalEntryHeader()
        {
            JournalEntryLines = new List<JournalEntryLine>();
        }
        public DateTime Date { get; set; }
        public string ReferenceNo { get; set; }
        public string Memo { get; set; }
        public bool Posted { get; set; }

        public virtual ICollection<JournalEntryLine> JournalEntryLines { get; set; }
    }
}
