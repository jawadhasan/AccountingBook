using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingBook.Web.Dtos
{
  public class CompanyDto : BaseDto
  {
    [Required]
    public string CompanyName { get; set; }

    [Required]
    public string ShortName { get; set; }

    [Required]
    public string CompanyCode { get; set; }
  }
}
