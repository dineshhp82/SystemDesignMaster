namespace RuleEngine
{
    public class CompositeRule<T> : IRule<T>
    {
        public string Name { get; private set; }
        public string Message { get; private set; }
        public int Priority { get; private set; } = 0;

        private readonly CompositeOperator _operator;

        private readonly List<IRule<T>> _rules;

        public CompositeRule(string name, CompositeOperator op, IEnumerable<IRule<T>> rules, string message = "", int priority = 100)
        {
            Name = name;
            _operator = op;
            _rules = [.. rules];
            Message = message;
            Priority = priority;
        }

        public bool Evaluate(T obj)
        {
            switch (_operator)
            {
                case CompositeOperator.And:
                    return _rules.All(r => r.Evaluate(obj));
                case CompositeOperator.Or:
                    return _rules.Any(r => r.Evaluate(obj));
                case CompositeOperator.Not:
                    if (_rules.Count != 1)
                        throw new InvalidOperationException("NOT operator requires exactly one rule.");
                    return !_rules[0].Evaluate(obj);
                default:
                    throw new NotSupportedException($"Operator {_operator} is not supported.");
            }
        }
    }
}
