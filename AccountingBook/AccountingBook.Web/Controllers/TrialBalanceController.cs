using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountingBook.Core.Enums;
using AccountingBook.Core.Financial;
using AccountingBook.Data;
using AccountingBook.Web.Dtos;
using AccountingBook.Web.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AccountingBook.Web.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class TrialBalanceController : ControllerBase
  {
    private readonly AppDbContext _db;

    public TrialBalanceController(AppDbContext db)
    {
      _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
      try
      {
        var dto = await TrialBalance();
        return Ok(dto);
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        return BadRequest(e.Message);
      }
    
    }


    private async Task<IEnumerable<TrialBalanceDto>> TrialBalance()
    {
      var allDebitLines = GetGeneralLedgerLines(DrOrCrSide.Dr);
      var allDr = allDebitLines
        .GroupBy(l => new { l.AccountId, l.Account.AccountCode, l.Account.AccountName, l.Amount })
        .Select(l => new
        {
          AccountId = l.Key.AccountId,
          AccountCode = l.Key.AccountCode,
          AccountName = l.Key.AccountName,
          Debit = l.Sum(d => d.Amount)
        });


      var allCreditLines = GetGeneralLedgerLines(DrOrCrSide.Cr);
      var allCr = allCreditLines
        .GroupBy(l => new { l.AccountId, l.Account.AccountCode, l.Account.AccountName, l.Amount })
        .Select(l => new
        {
          AccountId = l.Key.AccountId,
          AccountCode = l.Key.AccountCode,
          AccountName = l.Key.AccountName,
          Credit = l.Sum(d => d.Amount)
        });

      var allDrcr = (from x in allDr
          select new TrialBalanceDto
          {
            AccountId = x.AccountId,
            AccountCode = x.AccountCode.ToString(),
            AccountName = x.AccountName,
            Debit = x.Debit,
            Credit = (decimal)0,
          }
        ).Concat(from y in allCr
          select new TrialBalanceDto
          {
            AccountId = y.AccountId,
            AccountCode = y.AccountCode.ToString(),
            AccountName = y.AccountName,
            Debit = (decimal)0,
            Credit = y.Credit,
          });

      var sortedList = allDrcr
        .OrderBy(tb => tb.AccountCode)
        .ToList()
        .Reverse<TrialBalanceDto>();

      var accounts = sortedList.ToList().GroupBy(a => a.AccountCode)
        .Select(tb => new TrialBalanceDto()
        {
          AccountId = tb.First().AccountId,
          AccountCode = tb.First().AccountCode,
          AccountName = tb.First().AccountName,
          Credit = tb.Sum(x => x.Credit),
          Debit = tb.Sum(y => y.Debit)
        }).ToList();

      return await Task.FromResult(accounts);
    }

    //duplicated b/w general ledger controller and this one, refactor it
    private IEnumerable<GeneralLedgerLine> GetGeneralLedgerLines(DrOrCrSide drOrCr)
    {
      var drCr = _db.GeneralLedgerLines
        .Include(gl => gl.GeneralLedgerHeader)
        .Include(gl => gl.Account)
        .Where(l => l.DrCr == drOrCr)
        .AsEnumerable();

      return drCr;
    }

  }
}
