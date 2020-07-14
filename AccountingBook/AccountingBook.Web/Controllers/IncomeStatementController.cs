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
  public class IncomeStatementController : ControllerBase
  {
    private readonly Repository _repo;
    public IncomeStatementController(Repository repo)
    {
      _repo = repo;
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
      var revenues = await _repo.GetAccountsByAccountType(AccountType.Revenue);
      var expenses = await _repo.GetAccountsByAccountType(AccountType.Expense);

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

  }
}
