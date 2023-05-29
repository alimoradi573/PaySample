namespace Pay.OvetimePolicies.Domain.Entities
{
    public class PayEntity
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
