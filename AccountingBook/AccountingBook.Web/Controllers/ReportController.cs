using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountingBook.Data;
using AccountingBook.Web.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AccountingBook.Web.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ReportController : ControllerBase
  {
    private readonly AppDbContext _db;
    public ReportController(AppDbContext db)
    {
      _db = db;
    }

    [HttpGet]
    public ActionResult Get()
    {
      //static data
      var staticData = StaticData.GetJournalEntires();
      return Ok(staticData);
    }
  }
}
