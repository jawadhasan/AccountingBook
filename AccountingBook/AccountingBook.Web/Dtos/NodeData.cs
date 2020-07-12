using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountingBook.Core.Enums;

namespace AccountingBook.Web.Dtos
{
  public class NodeData
  {
    public AccountData Data { get; set; }
    public List<NodeData> Children { get; set; }

    public NodeData()
    {
      Data = new AccountData();
      Children = new List<NodeData>();
    }
  }

  public class AccountData
  {
    public long Id { get; set; }
    public AccountType AccountType { get; set; }
    public long? ParentAccountId { get; set; }
    public long AccountCode { get; set; }
    public string AccountName { get; set; }
    public decimal Balance { get; set; }
    public decimal DebitBalance { get; set; }
    public decimal CreditBalance { get; set; }

  }
}
