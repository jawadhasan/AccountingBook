using AccountingBook.Web.Utils;
using Microsoft.AspNetCore.Mvc;

namespace AccountingBook.Web.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ReportController : ControllerBase
  {
    public ReportController()
    {
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
