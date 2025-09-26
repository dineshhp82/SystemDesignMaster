using RuleEngine.DomainModel;

namespace RuleEngine.Rules
{
    public class MaxLoanAmountRule : IRule<Loan>
    {
        public string Name => "MaxLoanAmountRule";

        public string Message => $"Loan amount exceeds maximum allowed";

        public int Priority { get; private set; }

        private readonly decimal _maxAmount;
        public MaxLoanAmountRule(decimal maxAmount, int priority = 100)
        {
            _maxAmount = maxAmount;
            Priority = priority;
        }

        public bool Evaluate(Loan loan) => loan.Amount <= _maxAmount;
    }
}
