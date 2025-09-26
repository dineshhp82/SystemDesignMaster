namespace RuleEngine.Entities
{
    public class RuleResult
    {
        public string RuleName { get; set; } = "";
        public bool Passed { get; set; }
        public string Message { get; set; } = "";
        public int Priority { get; set; }
    }
}
