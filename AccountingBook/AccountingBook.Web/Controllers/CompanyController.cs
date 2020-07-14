using System;
using System.Threading.Tasks;
using AccountingBook.Data;
using AccountingBook.Web.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AccountingBook.Web.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CompanyController : ControllerBase
  {
    private readonly AppDbContext _db;

    public CompanyController(AppDbContext db)
    {
      _db = db;
    }


    [HttpGet]
    public async Task<IActionResult> Get()
    {
      var company = await _db.Companies.FirstOrDefaultAsync();

      //map to dto
      var companyDto = new CompanyDto
      {
        Id = company.Id,
        CompanyName = company.CompanyName,
        ShortName = company.ShortName,
        CompanyCode = company.CompanyCode
      };
      return Ok(companyDto);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CompanyDto updateCompany)
    {
        try
        {
          //get the entity from db
          var editableCompany = await _db.Companies.FirstOrDefaultAsync();

          //Mapping
          editableCompany.CompanyName = updateCompany.CompanyName;
          editableCompany.ShortName = updateCompany.ShortName;
          editableCompany.CompanyCode = updateCompany.CompanyCode;

          //persistence
            _db.Companies.Update(editableCompany);
            await _db.SaveChangesAsync();
            return Ok(updateCompany);
        }
        catch (Exception e)
        {

          return BadRequest(e.Message);
        }
    }

  }
}
