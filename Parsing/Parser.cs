using System.Collections.Generic;

namespace MathParser
{
    public class Parser(string expression)
    {
        private readonly Lexer _lexer = new(expression);

        public double Parse()
        {
            List<Token> tokens = _lexer.Tokenize();

            if (tokens.Count != 0)
            {
                IExpressionNode root = BuildBinaryExpressionTree(tokens);
                return root.Evaluate();
            }

            return 0;
        }

        private static IExpressionNode BuildBinaryExpressionTree(List<Token> tokens)
        {
            RemoveParenthesisAround(tokens);

            if (tokens.Count == 1)
                return new ValueNode(tokens[0]);

            int lessPriorityOperator = GetOperatorWithSmallerPriority(tokens);

            List<Token> leftOperandTokens = tokens.GetRange(0, lessPriorityOperator);
            List<Token> rightOperandTokens = tokens.GetRange(lessPriorityOperator + 1, tokens.Count - lessPriorityOperator - 1);

            IExpressionNode leftOperand = BuildBinaryExpressionTree(leftOperandTokens);
            IExpressionNode rightOperand = BuildBinaryExpressionTree(rightOperandTokens);
            
            return new OperationNode(tokens[lessPriorityOperator], leftOperand, rightOperand);
        }

        private static void RemoveParenthesisAround(List<Token> tokens)
        {
            bool isSurroundedByParenthesis = tokens.Count > 2 && tokens[0].tokenType == Token.TokenType.OpenParenthesis 
                                                              && tokens[^1].tokenType == Token.TokenType.CloseParenthesis;
            
            if (isSurroundedByParenthesis)
            {
                tokens.RemoveAt(0);
                tokens.RemoveAt(tokens.Count - 1);
            }
        }

        private static int GetOperatorWithSmallerPriority(IReadOnlyList<Token> involvedTokens)
        {
            (int Index, int Priority) lessPriority = (-1, int.MaxValue);
            int openedParenthesis = 0;
            
            for (int index = 0; index < involvedTokens.Count; index++)
            {
                Token involvedToken = involvedTokens[index];
                openedParenthesis += GetParenthesisModifier(involvedToken);

                if (openedParenthesis == 0 && involvedToken.IsOperator())
                {
                    if (index - 1 < 0 || involvedTokens[index - 1].IsOperator())
                        continue;

                    int operatorPriority = GetOperatorPriority(involvedToken.tokenType);

                    if (operatorPriority < lessPriority.Priority)
                        lessPriority = (index, operatorPriority);
                }
            }

            return lessPriority.Index;
        }

        private static int GetParenthesisModifier(Token involvedToken)
        {
            return involvedToken.tokenType switch
            {
                Token.TokenType.OpenParenthesis => 1,
                Token.TokenType.CloseParenthesis => -1,
                _ => 0
            };
        }
        
        private static int GetOperatorPriority(Token.TokenType tokenType)
        {
            return tokenType switch
            {
                Token.TokenType.AdditionOperator => 1,
                Token.TokenType.SubtractionOperator => 1,
                Token.TokenType.MultiplicationOperator => 2,
                Token.TokenType.DivisionOperator => 2,
                Token.TokenType.ExponentialOperator => 3,
                _ => -1
            };
        }
    }
}