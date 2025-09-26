using RuleEngine.Entities;

namespace RuleEngine
{
    public class RuleEngineHandler<T>
    {
        private readonly List<IRule<T>> _rules = new();

        public void AddRule(IRule<T> rule)
        {
            if (rule == null)
                throw new ArgumentNullException(nameof(rule));
            _rules.Add(rule);
        }

        //Evaluate rules for a single object
        public List<RuleResult> Evaluate(T obj)
        {
            var results = new List<RuleResult>();

            var sortedRules = _rules.OrderBy(r => r.Priority).ToList();

            foreach (var rule in sortedRules)
            {
                var passed = rule.Evaluate(obj);

                results.Add(new RuleResult
                {
                    RuleName = rule.Name,
                    Message = rule.Message,
                    Passed = passed,
                    Priority = rule.Priority
                });
            }

            return results;
        }

        //Evaluate all 
        public Dictionary<T, List<RuleResult>> EvaluateAll(IEnumerable<T> objs)
        {
            var dic = new Dictionary<T, List<RuleResult>>();

            foreach (var obj in objs)
            {
                dic[obj] = Evaluate(obj);
            }

            return dic;
        }

        public (int passed, int failed) Summary(List<RuleResult> ruleResults)
        {
            var passed = ruleResults.Count(r => r.Passed);
            var failed = ruleResults.Count - passed;
            return (passed, failed);
        }

        //Evaluate object and execute actions
        public List<RuleResult> EvaluatedAndAct(T obj, Action<T>? onSuccess = null, Action<T, List<RuleResult>>? onFailure = null)
        {
            var results = Evaluate(obj);

            bool allPassed = results.All(r => r.Passed);

            if (allPassed)
            {
                onSuccess?.Invoke(obj);
            }
            else
            {
                onFailure?.Invoke(obj, results);
            }

            return results;
        }
    }
}
