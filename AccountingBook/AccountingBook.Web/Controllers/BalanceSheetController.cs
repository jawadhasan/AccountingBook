using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountingBook.Core.Enums;
using AccountingBook.Core.Financial;
using AccountingBook.Data;
using AccountingBook.Web.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AccountingBook.Web.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class BalanceSheetController : ControllerBase
  {
    private readonly AppDbContext _db;
    public BalanceSheetController(AppDbContext db)
    {
      _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
      try
      {
        var dto = await BalanceSheet();
        return Ok(dto);
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        return BadRequest(e.Message);
      }

    }




    public async Task<IEnumerable<BalanceSheet>> BalanceSheet()
    {
      var assets = await GetAccountsByAccountType(AccountType.Assets);
      var liabilities = await GetAccountsByAccountType(AccountType.Liabilities);
      var equities = await GetAccountsByAccountType(AccountType.Equity);

      var balanceSheet = new HashSet<BalanceSheet>();
      foreach (var asset in assets)
      {
        balanceSheet.Add(new BalanceSheet
        {
          AccountId = asset.Id,
          AccountType = (int) asset.AccountType,
          AccountCode = asset.AccountCode.ToString(),
          AccountName = asset.AccountName,
          Amount = asset.Balance
        });
      }
      foreach (var liability in liabilities)
      {
        balanceSheet.Add(new BalanceSheet
        {
          AccountId = liability.Id,
          AccountType = (int) liability.AccountType,
          AccountCode = liability.AccountCode.ToString(),
          AccountName = liability.AccountName,
          Amount = liability.Balance
        });
      }
      foreach (var equity in equities)
      {
        balanceSheet.Add(new BalanceSheet
        {
          AccountId = equity.Id,
          AccountType = (int) equity.AccountType,
          AccountCode = equity.AccountCode.ToString(),
          AccountName = equity.AccountName,
          Amount = equity.Balance
        });
      }
      return await Task.FromResult(balanceSheet);
    }

    private async Task<List<Account>> GetAccountsByAccountType(AccountType accountType)
    {
      var accounts = await _db.Accounts
        .Include(a => a.ChildAccounts)
        .Include(a => a.ParentAccount)
        .Include(a => a.GeneralLedgerLines)
        .Where(a => a.AccountType == accountType && a.ParentAccountId != null)
        .ToListAsync();
      return accounts;
    }

  }
}
