using RuleEngine.DomainModel;
using RuleEngine.Entities;
using RuleEngine.Rules;

namespace RuleEngine
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Rule Engine");

            var loan = new Loan { Amount = 120_000, ApplicantAge = 25, ApplicantName = "Alice" };

            // Define simple rules
            var maxAmountRule = new MaxLoanAmountRule(100_000, priority: 1);
            var minAgeRule = new MinimumAgeRule(21, priority: 2);

            // Composite rule (AND)
            var rules = new List<IRule<Loan>> { maxAmountRule, minAgeRule };
            var compositeRule = new CompositeRule<Loan>("LoanEligibilityRule",
                CompositeOperator.And,
                rules,
                "Loan validation failed", priority: 0);

            // Create engine and register rules
            var engine = new RuleEngineHandler<Loan>();
            engine.AddRule(compositeRule);

            Action<Loan> onSuccess = l => Console.WriteLine("Loan approved for " + l.ApplicantName);

            Action<Loan, List<RuleResult>> onFailure = (l, results) =>
            {
                Console.WriteLine("Loan application denied for " + l.ApplicantName);
                foreach (var result in results)
                {
                    if (!result.Passed)
                    {
                        Console.WriteLine($" - Rule '{result.RuleName}' failed: {result.Message}");
                    }
                }
            };

            var ruleResults = engine.EvaluatedAndAct(loan, onSuccess, onFailure);

            var summary = engine.Summary(ruleResults);

            Console.WriteLine($"Passed : {summary.passed} Failed : {summary.failed}");
        }
    }
}
