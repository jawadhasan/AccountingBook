using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountingBook.Core.Enums;
using AccountingBook.Core.Financial;
using AccountingBook.Data;
using AccountingBook.Web.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace AccountingBook.Web.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class LedgerController : ControllerBase
  {
    private readonly Repository _repo;

    public LedgerController(Repository repo)
    {
      _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {

      var dto = await MasterGeneralLedger();
      var generalLedgerTree = BuildMasterGeneralLedger(dto);
      //return Ok(dto);
      return Ok(generalLedgerTree);
    }

    private async Task<IEnumerable<MasterGeneralLedger>> MasterGeneralLedger()
    {

      var allDr = GetAllDebits();
      var allCr = GetAllCredits();

      var allDrcr = (from x in allDr
                     select new MasterGeneralLedger
                     {
                       Id = x.Id,
                       TransactionNo = x.TransactionNo,
                       Reference = x.Reference,
                       AccountId = x.AccountId,
                       AccountCode = x.AccountCode,
                       AccountName = x.AccountName,
                       Date = x.Date,
                       Debit = x.Debit,
                       Credit = 0,
                     }
                    ).Concat(from y in allCr
                             select new MasterGeneralLedger
                             {
                               Id = y.Id,
                               TransactionNo = y.TransactionNo,
                               Reference = y.Reference,
                               AccountId = y.AccountId,
                               AccountCode = y.AccountCode,
                               AccountName = y.AccountName,
                               Date = y.Date,
                               Debit = 0,
                               Credit = y.Credit,
                             });

      var sortedList = allDrcr.OrderBy(gl => gl.Id).ToList().Reverse<MasterGeneralLedger>();

      return await Task.FromResult(sortedList.ToList());
    }

    //helper methods level-0
    private IEnumerable<MasterGeneralLedger> GetAllDebits()
    {
      //Get LedgerLines for Debit entries
      var allDr = _repo.GetGeneralLedgerLines(DrOrCrSide.Dr);
      return BuildMasterGeneralLedger(allDr, DrOrCrSide.Dr);
    }
    private IEnumerable<MasterGeneralLedger> GetAllCredits()
    {
      //Get LedgerLines for CreditEntries entries
      var allCr = _repo.GetGeneralLedgerLines(DrOrCrSide.Cr);
      return BuildMasterGeneralLedger(allCr, DrOrCrSide.Cr);
    }
    
    private static IEnumerable<MasterGeneralLedger> BuildMasterGeneralLedger(
      IEnumerable<GeneralLedgerLine> generalLedgerLines, DrOrCrSide drOrCr)
    {
      var masterGeneralLedgers = generalLedgerLines
        .Select(l => new MasterGeneralLedger
        {
          Id = l.Id,
          TransactionNo = l.GeneralLedgerHeader.Id,
          Reference = l.GeneralLedgerHeader.Description,
          AccountId = l.AccountId,
          AccountCode = l.Account.AccountCode.ToString(),
          AccountName = l.Account.AccountName,
          Date = l.GeneralLedgerHeader.Date,
          Debit = drOrCr == DrOrCrSide.Dr ? l.Amount : 0,
          Credit = drOrCr == DrOrCrSide.Cr ? l.Amount : 0,
        });

      return masterGeneralLedgers;
    }

    private IList<MasterGeneralLedger> BuildMasterGeneralLedger(IEnumerable<MasterGeneralLedger> allLedger)
    {

      var ledgersList = allLedger.ToList();

      var parentLedger = ledgersList.Select(a => a.TransactionNo).Distinct().ToList();
      var childLedgers = new List<MasterGeneralLedger>();

      parentLedger.ForEach(a =>
      {
        var childrenLedger = ledgersList.Where(x => x.TransactionNo == a);

        var secondChild = new MasterGeneralLedger();
        //secondChild.GroupId = null;//??
        //secondChild.TransactionNo = null;
        //secondChild.Credit = null;
        //secondChild.Debit = null;
        //secondChild.Date = null;

        var thirdChildren = new List<MasterGeneralLedger>();

        foreach (var ledger in childrenLedger)
        {
          var thirdChild = new MasterGeneralLedger();
          //  thirdChild.GroupId = ledger.GroupId;
          thirdChild.Id = ledger.Id;
          thirdChild.TransactionNo = ledger.TransactionNo;
          thirdChild.Reference = ledger.Reference;
          
          thirdChild.AccountId = ledger.AccountId;
          thirdChild.AccountName = ledger.AccountName;
          thirdChild.AccountCode = ledger.AccountCode;
          thirdChild.Date = ledger.Date;
          thirdChild.Debit = ledger.Debit;
          thirdChild.Credit = ledger.Credit;


          thirdChildren.Add(thirdChild);
          childLedgers.Add(thirdChild);
        }
      });

      return childLedgers;
    }

  }
}
