using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountingBook.Web.Dtos;

namespace AccountingBook.Web.Utils
{
  public static class StaticData
  {
    public static List<JournalEntryDto> GetJournalEntires()
    {
      return new List<JournalEntryDto>
      {
        new JournalEntryDto
        {
          Id = 1000,
          Date = DateTime.Now,
          ReferenceNo = "R100",
          Credit = 13000,
          Debit = 13000,
          Memo = "StaticData1",
          Posted = true,
          ReadyForPosting = true
        },
        new JournalEntryDto
        {
          Id = 2000,
          Date = DateTime.Now,
          ReferenceNo = "R200",
          Credit = 11000,
          Debit = 12000,
          Memo = "StaticData2",
          Posted = false,
          ReadyForPosting = false
        },
        new JournalEntryDto
        {
          Id = 3000,
          Date = DateTime.Now,
          ReferenceNo = "R300",
          Credit = 5000,
          Debit = 5000,
          Memo = "StaticData2",
          Posted = false,
          ReadyForPosting = true
        }
      };
    }
  }
}
