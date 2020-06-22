namespace AccountingBook.Core
{
    public class Company : EntityBase
    {
        public Company()
        {
        }

        public Company(string companyName, string shortName, string companyCode)
        {
            CompanyName = companyName;
            ShortName = shortName;
            CompanyCode = companyCode;
        }
        public string CompanyName { get; set; }
        public string ShortName { get; set; }
        public string CompanyCode { get; set; }
    }
}
