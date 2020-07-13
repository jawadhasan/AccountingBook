using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingBook.Web.Dtos
{

  //notice not from baseDto
  public class TrialBalanceDto
  {
    public long AccountId { get; set; }
    public string AccountCode { get; set; }
    public string AccountName { get; set; }
    public decimal Debit { get; set; }
    public decimal Credit { get; set; }
  }
}
