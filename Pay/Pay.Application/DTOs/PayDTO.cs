namespace Pay.OvetimePolicies.Application.DTOs
{
    public class PayDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long BasicSalary { get; set; }
        public long Allowance { get; set; }
        public long Transportation { get; set; }
        public DateTime Date { get; set; }
 
    }

    public class PayFilterDTO
    {
        public int Id { get; set; } = 0;
        public DateTime ToDate { get; set; }
        public DateTime FromDate { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
