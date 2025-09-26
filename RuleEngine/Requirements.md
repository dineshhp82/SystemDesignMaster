 Rule Engine
 
 Requirements:

- Design a generic rule engine that can be fit into any type of application
- Allow to define rule dynamically
- Allow to composit the rules like AND ,OR etc.
- Allow to priorities the rules optional
- Evaluate the rules based on the input data
- Allow to display the result after evaluation
- Should be thread-safe

Entites:
 - Rule Result
	
+---------------------+
|      IRule<T>       |
|---------------------|
| + string Name       |
| + string Message    |
| + int Priority      |
| + bool Evaluate(T)  |
+---------------------+
          ^
          |
  +------------------------+
  |    SimpleRule<T>       |  (e.g., MaxLoanAmountRule)
  +------------------------+

  +------------------------+
  |   CompositeRule<T>     |
  |------------------------|
  | - List<IRule<T>> rules |
  | - CompositeOperator op |
  |------------------------|
  | + Evaluate(T obj)      |
  +------------------------+

+--------------------------+
|       RuleResult         |
|--------------------------|
| string RuleName          |
| bool Passed              |
| string Message           |
| int Priority             |
+--------------------------+

+--------------------------+
|      RuleEngine<T>       |
|--------------------------|
| - List<IRule<T>> rules   |
|--------------------------|
| + AddRule(IRule<T>)      |
| + Evaluate(T obj)        |
| + EvaluateAll(IEnumerable<T>) |
| + Summary(List<RuleResult>)   |
| + EvaluateAndAct(T, onSuccess, onFailure) |
+--------------------------+

+--------------------------+
|         Loan             |
|--------------------------|
| decimal Amount           |
| int ApplicantAge         |
| string ApplicantName     |
+--------------------------+


Design Pattern Used:
 - Specification Pattern
 - Stratgy Pattern
 - Factory
 - Composite Pattern


IRule<T>           <- interface for all rules
 ├─ SimpleRule<T>  <- basic single rule
 ├─ CompositeRule<T> <- And/Or/Not
RuleResult        <- evaluation result
RuleEngine<T>     <- engine to evaluate rules


SOLID & OOP Principles:
 Single Responsibility → each rule encapsulates one responsibility
 Open/Closed → add new rules without modifying engine
 Strategy/Specification → rules as interchangeable strategies
 Composite → combine rules logically

 



