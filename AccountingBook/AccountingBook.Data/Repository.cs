using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountingBook.Core.Enums;
using AccountingBook.Core.Financial;
using Microsoft.EntityFrameworkCore;

namespace AccountingBook.Data
{
    public class Repository
    {
        private readonly AppDbContext _db;
        public Repository(AppDbContext db)
        {
            _db = db;
        }

        //Accounts
        public async Task<List<Account>> GetAccounts()
        {
            var accounts = await _db.Accounts
                .Include(a => a.ChildAccounts)
                .Include(a => a.GeneralLedgerLines)
                .ToListAsync();
            return accounts;
        }

        public async Task<List<Account>> GetPostingAccounts()
        {
            var accounts = await _db.Accounts
                .Include(a => a.ChildAccounts)
                .Where(a => a.ChildAccounts.Count == 0) //notice this property
                .OrderBy(a => a.AccountName)
                .ThenBy(a => a.AccountType)
                .ToListAsync();
            return accounts;
        }

        public async Task<List<Account>> GetAccountsByAccountType(AccountType accountType)
        {
            var accounts = await _db.Accounts
                .Include(a => a.ChildAccounts)
                .Include(a => a.ParentAccount)
                .Include(a => a.GeneralLedgerLines)
                .Where(a => a.AccountType == accountType && a.ParentAccountId != null)
                .ToListAsync();
            return accounts;
        }


        //Journal
        public async Task<List<JournalEntryHeader>> GetJournalEntries()
        {
            var journalEntries = await _db.JournalEntryHeaders
                .Include(je => je.JournalEntryLines)
                .ThenInclude(c => c.Account)
                .Include(je => je.GeneralLedgerHeader)
                .ToListAsync();

            return journalEntries;
        }
        public async Task<JournalEntryHeader> GetJournalEntryById(long id)
        {
            var journal = await _db.JournalEntryHeaders
                .Include(je => je.JournalEntryLines)
                .ThenInclude(c => c.Account)
                .Include(je => je.GeneralLedgerHeader)
                .FirstOrDefaultAsync(c => c.Id == id);

            return journal;
        }


        //Ledger
        public IEnumerable<GeneralLedgerLine> GetGeneralLedgerLines(DrOrCrSide drOrCr)
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
