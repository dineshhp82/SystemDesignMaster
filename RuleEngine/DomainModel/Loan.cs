namespace RuleEngine.DomainModel
{
    public class Loan
    {
        public decimal Amount { get; set; }
        public int ApplicantAge { get; set; }
        public string ApplicantName { get; set; } = "";
    }
}
