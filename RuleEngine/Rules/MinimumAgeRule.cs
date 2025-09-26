using RuleEngine.DomainModel;

namespace RuleEngine.Rules
{
    public class MinimumAgeRule : IRule<Loan>
    {
        public string Name => "MinimumAgeRule";
        public string Message => "Applicant does not meet minimum age requirement";
        public int Priority { get; private set; }

        private readonly int _minAge;
        public MinimumAgeRule(int minAge, int priority = 100)
        {
            _minAge = minAge;
            Priority = priority;
        }

        public bool Evaluate(Loan loan) => loan.ApplicantAge >= _minAge;
    }
}
