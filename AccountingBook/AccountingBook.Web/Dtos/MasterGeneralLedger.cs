using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingBook.Web.Dtos
{
  public class MasterGeneralLedger: BaseDto
  {
    public MasterGeneralLedger()
    {
      Children = new List<MasterGeneralLedger>();
    }
    public long TransactionNo { get; set; } //this field is for grouping purposes. using journalheader-id
    public string Reference { get; set; } //?? using journalheader-refer
    public long AccountId { get; set; }
    public string AccountCode { get; set; }
    public string AccountName { get; set; }
    public DateTime Date { get; set; }
    public decimal Debit { get; set; }
    public decimal Credit { get; set; }
    //public int? GroupId { get; set; }

    public List<MasterGeneralLedger> Children { get; set; }


  }
}
