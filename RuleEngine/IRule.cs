namespace RuleEngine
{
    public interface IRule<T>
    {
        string Name { get; }
        string Message { get; }
        int Priority { get; } // Optional priority

        bool Evaluate(T obj);
    }
}
