using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountingBook.Data;
using AccountingBook.Web.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AccountingBook.Web.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class LookupController : ControllerBase
  {
    private readonly AppDbContext _db;

    public LookupController(AppDbContext db)
    {
      _db = db;
    }

    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> PostingAccounts()
    {
      var accounts = await _db.Accounts
        .Include(a => a.ChildAccounts)
        .Where(a => a.ChildAccounts.Count == 0) //notice this property
        .OrderBy(a => a.AccountName)
        .ThenBy(a => a.AccountType)
        .ToListAsync();

      var accountsDto = new HashSet<LookupItem>();

      foreach (var account in accounts)
        accountsDto.Add(new LookupItem(account.Id, account.AccountName));

      return Ok(accountsDto);
    }

  }
}
