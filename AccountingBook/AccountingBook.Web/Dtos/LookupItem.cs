using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingBook.Web.Dtos
{
  public class LookupItem
  {
    public LookupItem(long id, string name)
    {
      Id = id;
      Name = name;
    }
    public long Id { get; set; }
    public string Name { get; set; }
  }
}
