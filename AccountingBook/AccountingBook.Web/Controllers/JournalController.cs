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
  public class JournalController : ControllerBase
  {
    private readonly AppDbContext _db;
    private readonly Repository _repo;

    public JournalController(AppDbContext db, Repository repo)
    {
      _db = db;
      _repo = repo;
    }


    [HttpGet]
    [Route("[action]")]
    public ActionResult TempGet()
    {
      //static data
      var staticData = StaticData.GetJournalEntires();
      return Ok(staticData);
    }


    [HttpGet]
    public async Task<IActionResult> Get(int includePosted = 1)
    {

      var journalEntries = await _repo.GetJournalEntries();
      var journalEntryDtosList = new List<JournalEntryHeaderDto>();

      //mapping from db entity to Dto
      foreach (var je in journalEntries)
      {
        var journalEntryDto = new JournalEntryHeaderDto()
        {
          Id = je.Id,
          Date = je.Date,
          Memo = je.Memo,
          ReferenceNo = je.ReferenceNo,
          Posted = je.Posted
        };

        foreach (var line in je.JournalEntryLines)
        {
          var lineDto = new JournalEntryLineDto
          {
            Id = line.Id,
            AccountId = line.AccountId,
            Amount = line.Amount,
            DrCrId = (int)line.DrCr,
            Memo = line.Memo
          };

          journalEntryDto.Lines.Add(lineDto);
        }

        // is this journal entry ready for posting?
        if (!journalEntryDto.Posted
            && journalEntryDto.Lines.Count >= 2
            && (journalEntryDto.DebitAmount == journalEntryDto.CreditAmount)
            && (journalEntryDto.DebitAmount !=0))
        {
          journalEntryDto.ReadyForPosting = true;
        }

        journalEntryDtosList.Add(journalEntryDto);
      }
      
      //now prepare a simple model for grid

      var entries = new List<JournalEntryDto>();
      foreach (var item in journalEntryDtosList)
      {
        var entry = new JournalEntryDto
        {
          Id = item.Id,
          Date = item.Date,
          Debit = item.DebitAmount,
          Credit = item.CreditAmount,
          ReferenceNo = item.ReferenceNo,
          Memo = item.Memo,
          Posted = item.Posted,
          ReadyForPosting = item.ReadyForPosting
        };

        entries.Add(entry);
      }
  
      return Ok(entries);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> Get(long id)
    {
      try
      {
        var result = await _repo.GetJournalEntryById(id);

        //Mapping FROM database entity to Dto
        var dto = new JournalEntryHeaderDto();
        dto.Id = result.Id;
        dto.Date = result.Date;
        dto.ReferenceNo = result.ReferenceNo;
        dto.Memo = result.Memo;
        dto.Posted = result.Posted;

        //lines
        foreach (var lineItem in result.JournalEntryLines)
        {
          var line = new JournalEntryLineDto();
          line.AccountId = lineItem.AccountId;
          line.DrCrId = (int) lineItem.DrCr;
          line.Amount = lineItem.Amount;
          line.Memo = lineItem.Memo;

          //add to master
          dto.Lines.Add(line);
        }


        return Ok(dto);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
      try
      {
        var removeableJournal = await _db.JournalEntryHeaders.FirstOrDefaultAsync(c => c.Id == id);
        if (removeableJournal != null && !removeableJournal.Posted)
        {
          _db.JournalEntryHeaders.Remove(removeableJournal);
          await _db.SaveChangesAsync();
        }
        return Ok();
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> SaveJournal([FromBody] JournalEntryHeaderDto journalEntryDto)
    {

      var anyDuplicate = journalEntryDto.Lines.GroupBy(x => x.AccountId).Any(g => g.Count() > 1);
      if (anyDuplicate)
        return BadRequest("One or more journal entry lines has duplicate account.");

      var isNew = journalEntryDto.Id == 0;
      JournalEntryHeader journalEntry = null;

      if (isNew)
      {
        //inserting
        journalEntry = new JournalEntryHeader();
      }
      else
      {
        //editing
        journalEntry = await _db.JournalEntryHeaders
          .Where(j => j.Id == journalEntryDto.Id)
          .Include(j => j.JournalEntryLines)
          .FirstOrDefaultAsync();

        //get all oldLines
        var oldLines = await _db.JournalEntryLines
          .Where(l => l.JournalEntryHeaderId == journalEntryDto.Id)
          .ToListAsync();

        //Remove these lines
        _db.JournalEntryLines.RemoveRange(oldLines);

        //Save to db
        await _db.SaveChangesAsync();
      }
      
      //mapping - master
      journalEntry.Date = journalEntryDto.Date;
      journalEntry.ReferenceNo = journalEntryDto.ReferenceNo;
      journalEntry.Memo = journalEntryDto.Memo;

      //lines
      foreach (var line in journalEntryDto.Lines)
      {

        var journalLine = new JournalEntryLine
        {
          AccountId = line.AccountId,
          DrCr = (DrOrCrSide)line.DrCrId,
          Amount = line.Amount,
          Memo = line.Memo
        };
        journalEntry.JournalEntryLines.Add(journalLine);
      }

      //save to db
      if (isNew)
      {
        _db.JournalEntryHeaders.Add(journalEntry);
      }
      else
      {
        _db.JournalEntryHeaders.Update(journalEntry);
      }

      
      await _db.SaveChangesAsync();

      return Ok(journalEntry);
    }

    [HttpPost]
    [Route("[action]/{id}")]
    public async Task<IActionResult> PostJournal(long id)
    {
      try
      {
        var journal = await _repo.GetJournalEntryById(id);
        //update status
        journal.Posted = true;

        //build ledger
        if (journal.GeneralLedgerHeaderId == null || journal.GeneralLedgerHeaderId == 0)
        {
          var ledger = new GeneralLedgerHeader();
          ledger.Description = journal.ReferenceNo;

          //ledger-lines
          foreach (var item in journal.JournalEntryLines)
          {
            var newLedgerLine = new GeneralLedgerLine();
            newLedgerLine.AccountId = item.AccountId;
            newLedgerLine.Account = item.Account;//we need to attach the entity as it is being checked in validate method below.
            newLedgerLine.DrCr = item.DrCr;
            newLedgerLine.Amount = item.Amount;

            //add line to ledger
            ledger.GeneralLedgerLines.Add(newLedgerLine);
          }
         
          //validate ledger
          var isValid = await ValidateGeneralLedgerEntry(ledger);
          if (isValid)
          {

            //TODO: refactor and/or improve
            //saving the ledger 
            _db.GeneralLedgerHeaders.Add(ledger);
            await _db.SaveChangesAsync();
            

            journal.GeneralLedgerHeaderId = ledger.Id;
            //TODO: do we need to attach the entity itself?
            await _db.SaveChangesAsync();
          }
        }
        return Ok(journal);
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        return BadRequest(e.Message);
      }
    }


    //Private

    //TODO: Refactor
    private async Task<bool> ValidateGeneralLedgerEntry(GeneralLedgerHeader glEntry)
    {

      if (!glEntry.DrCrEqualityValidated())
        throw new InvalidOperationException("Debit/Credit are not equal.");

     

      if (!glEntry.NoLineAmountIsEqualToZero())
        throw new InvalidOperationException("One or more line(s) amount is zero.");

     
      var duplicateAccounts = glEntry.GeneralLedgerLines
        .GroupBy(gl => gl.AccountId)
        .Where(gl => gl.Count() > 1);

      if (duplicateAccounts.Any())
        throw new InvalidOperationException("Duplicate account id in a collection.");

   
      foreach (var line in glEntry.GeneralLedgerLines)
      {
        var account = await _db.Accounts.FirstOrDefaultAsync(a=> a.Id == line.AccountId);

        if (!account.CanPost())
          throw new InvalidOperationException("One of the account is not valid for posting");
      }


      if (!glEntry.ValidateAccountingEquation())
        throw new InvalidOperationException("One of the account not equal.");

      return true;

    }

  }
}
