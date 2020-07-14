using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AccountingBook.Core.Enums;
using AccountingBook.Data;
using AccountingBook.Web.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace AccountingBook.Web.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class BalanceSheetController : ControllerBase
  {
    private readonly Repository _repo;
    public BalanceSheetController(Repository repo)
    {
      _repo = repo;
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
      var assets = await _repo.GetAccountsByAccountType(AccountType.Assets);
      var liabilities = await _repo.GetAccountsByAccountType(AccountType.Liabilities);
      var equities = await _repo.GetAccountsByAccountType(AccountType.Equity);

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

  }
}
