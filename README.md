# MathExpressionParser
A simple math expression parser. With a basic console application that gets an input and solves it.

It follows the steps of:
- Lexer: Translates the string expression to a sequence of tokens that have a type and value and can be easily manipulated later.
- Parser: Gets the tokens generated with the lexer and creates a binary expression tree to perform the operation based on the operator priority order and parenthesis. It then Evaluates the expression by calling Evaluate on the root node, which then calls it on the subsequent nodes, generating the final result.

Usage example:
```csharp
Parser parser = new Parser(expression);
double result = parser.Evaluate();
```

Current supported operators:
- Addition (+)
- Subtraction (-) -> Which includes negative numbers
- Multiplication (*)
- Division (/)
- Power (^) -> Can be used for radicals by inverting then. Ex.: Sqrt(3) would be 3 ^ (1 / 2)
