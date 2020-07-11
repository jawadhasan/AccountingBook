using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingBook.Web.Dtos
{

  //For main-grid
  public class JournalEntryDto : BaseDto
  {
    public DateTime Date { get; set; }
    public string ReferenceNo { get; set; }
    public string Memo { get; set; }
    public bool Posted { get; set; }
    public bool ReadyForPosting { get; set; }
    public decimal Debit { get; set; }
    public decimal Credit { get; set; }

  }

  //Header
  public class JournalEntryHeaderDto : BaseDto
  {
    public JournalEntryHeaderDto()
    {
      Lines = new List<JournalEntryLineDto>();
    }

    public DateTime Date { get; set; }
    public string ReferenceNo { get; set; }
    public string Memo { get; set; }
    public bool Posted { get; set; }
    public bool ReadyForPosting { get; set; }
    public decimal DebitAmount => GetDebitAmount();
    public decimal CreditAmount => GetCreditAmount();
    public List<JournalEntryLineDto> Lines { get; set; }

    private decimal GetDebitAmount()
    {
      decimal sum = 0;
      foreach (var entry in Lines)
      {
        if (entry.DrCrId == 1)
          sum += entry.Amount;
      }
      return sum;
    }
    private decimal GetCreditAmount()
    {
      decimal sum = 0;
      foreach (var entry in Lines)
      {
        if (entry.DrCrId == 2)
          sum += entry.Amount;
      }
      return sum;
    }
  }


  //Line
  public class JournalEntryLineDto : BaseDto
  {
    public long AccountId { get; set; }
    public int DrCrId { get; set; }
    public decimal Amount { get; set; }
    public string Memo { get; set; }
  }
}
