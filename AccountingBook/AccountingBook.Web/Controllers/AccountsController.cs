using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountingBook.Core.Financial;
using AccountingBook.Data;
using AccountingBook.Web.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace AccountingBook.Web.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AccountsController : ControllerBase
  {
   
    private readonly Repository _repo;

    public AccountsController(Repository repo)
    {
      _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
      var accounts = await _repo.GetAccounts();

      //var accountTree = BuildAccountGrouping(accounts.ToList(), null);
      //return Ok(accountTree);

      var nodeTree = BuildNodeTree(accounts.ToList(), null);
      return Ok( nodeTree);
    }


    private List<NodeData> BuildNodeTree(List<Account> allAccounts,long? parentAccountId)
    {
      var nodeTree = new List<NodeData>();
      var childAccounts = allAccounts.Where(o => o.ParentAccountId == parentAccountId).ToList();
      foreach (var account in childAccounts)
      {
        var nodeData = new NodeData
        {
          Data = new AccountData()
          {
            Id = account.Id,
            AccountType = account.AccountType,
            AccountCode = account.AccountCode,
            AccountName = account.AccountName,
            DebitBalance = account.ParentAccountId == null ? account.ChildAccounts.Sum(c => c.DebitBalance) : account.DebitBalance,
            CreditBalance = account.ParentAccountId == null ? account.ChildAccounts.Sum(c => c.CreditBalance) : account.CreditBalance,
            Balance = account.Balance,
            ParentAccountId = account.ParentAccountId
          }
        };
        var children = BuildNodeTree(allAccounts, account.Id);
        nodeData.Children = children;
        nodeTree.Add(nodeData);
      }
      return nodeTree;
    }



    private static List<AccountDto> BuildAccountGrouping(List<Account> allAccounts,
      long? parentAccountId)
    {
      var accountTree = new List<AccountDto>();
      var childAccounts = allAccounts.Where(o => o.ParentAccountId == parentAccountId).ToList();

      foreach (var account in childAccounts)
      {
        var accountDto = new AccountDto
        {
          Id = account.Id,
          AccountType = account.AccountType,
          ParentAccountId = account.ParentAccountId,
          AccountCode = account.AccountCode,
          AccountName = account.AccountName,
          Balance = account.Balance,
          DebitBalance = account.ParentAccountId == null ? account.ChildAccounts.Sum(c => c.DebitBalance) : account.DebitBalance,
          CreditBalance = account.ParentAccountId == null ? account.ChildAccounts.Sum(c => c.CreditBalance) : account.CreditBalance
        };
        var children = BuildAccountGrouping(allAccounts, account.Id);
        accountDto.Children = children;
        accountTree.Add(accountDto);
      }

      return accountTree;
    }
  }
}
