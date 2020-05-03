namespace AccountingBook.Core
{
    public class Company : EntityBase
    {
        protected Company()
        {
        }

        public Company(string companyName, string shortName, string companyCode)
        {
            CompanyName = companyName;
            ShortName = shortName;
            CompanyCode = companyCode;
        }
        public string CompanyName { get; protected set; }
        public string ShortName { get; protected set; }
        public string CompanyCode { get; protected set; }
    }
}
