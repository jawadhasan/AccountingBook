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
  public class IncomeStatementController : ControllerBase
  {
    private readonly AppDbContext _db;
    public IncomeStatementController(AppDbContext db)
    {
      _db = db;
    }


    [HttpGet]
    public async Task<IActionResult> Get()
    {
      try
      {
        var dto = await IncomeStatement();
        return Ok(dto);
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        return BadRequest(e.Message);
      }

    }


    private async Task<IEnumerable<IncomeStatement>> IncomeStatement()
    {
      var revenues = await GetAccountsByAccountType(AccountType.Revenue);
      var expenses = await GetAccountsByAccountType(AccountType.Expense);

      var revenuesExpenses = new HashSet<IncomeStatement>();

      foreach (var revenue in revenues)
      {
        revenuesExpenses.Add(new IncomeStatement
        {
          AccountId = (int)revenue.Id,
          AccountCode = revenue.AccountCode.ToString(),
          AccountName = revenue.AccountName,
          Amount = revenue.Balance,
          IsExpense = false
        });
      }
      foreach (var expense in expenses)
      {
        revenuesExpenses.Add(new IncomeStatement
        {
          AccountId = (int)expense.Id,
          AccountCode = expense.AccountCode.ToString(),
          AccountName = expense.AccountName,
          Amount = expense.Balance,
          IsExpense = true
        });
      }
      return await Task.FromResult(revenuesExpenses);
    }



    //Duplicated method in balancesheet controller as well.
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
