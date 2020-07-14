using System.Collections.Generic;
using System.Threading.Tasks;
using AccountingBook.Data;
using AccountingBook.Web.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace AccountingBook.Web.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class LookupController : ControllerBase
  {
    private readonly Repository _repo;

    public LookupController(Repository repo)
    {
      _repo = repo;
    }

    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> PostingAccounts()
    {
      var accounts = await _repo.GetPostingAccounts();

      var accountsDto = new HashSet<LookupItem>();

      foreach (var account in accounts)
        accountsDto.Add(new LookupItem(account.Id, account.AccountName));

      return Ok(accountsDto);
    }

  }
}
