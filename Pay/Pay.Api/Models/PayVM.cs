using Microsoft.AspNetCore.Mvc;

namespace Pay.OvetimePolicies.Api.Models
{
    [ModelBinder(binderType: typeof(SeparatorModelBinder))]
    public class PayVM
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long BasicSalary { get; set; }
        public long Allowance { get; set; }
        public long Transportation { get; set; }
        public DateTime Date { get; set; }

    }
}
