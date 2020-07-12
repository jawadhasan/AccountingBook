using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountingBook.Core.Enums;

namespace AccountingBook.Web.Dtos
{
  public class AccountDto : BaseDto
  {
      public string Description => $"{AccountName} ({AccountCode})";
      public AccountType AccountType { get; set; }
      public long? ParentAccountId { get; set; }
      public long AccountCode { get; set; }
      public string AccountName { get; set; }
      public decimal Balance { get; set; }
      public decimal DebitBalance { get; set; }
      public decimal CreditBalance { get; set; }

      public List<AccountDto> Children { get; set; }

    public AccountDto()
      {
        Children = new List<AccountDto>();
      }
    }
  }

