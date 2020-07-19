using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountingBook.Core.Enums;
using AccountingBook.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AccountingBook.Web.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class DashboardController : ControllerBase
  {
    private readonly Repository _repo;

    public DashboardController(Repository repo)
    {
      _repo = repo;
    }

    [Route("[action]")]
    public async Task<IActionResult> GetAccountsStats()
    {
      var accounts = await _repo.GetAccounts();
      var test = accounts
        .Where(a=> a.ParentAccountId == null && a.AccountType != AccountType.Equity) //only top-level accounts and no equity
        .GroupBy(l => new { l.Id, l.AccountCode, l.AccountName, l.Balance })
        .Select(l => new
        {
          AccountId = l.Key.Id,
          AccountCode = l.Key.AccountCode,
          AccountName = l.Key.AccountName,
          Balance = l.Sum(d => d.Balance)
        });

      return Ok(test);
    }
  }
}
