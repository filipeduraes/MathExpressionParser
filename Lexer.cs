using System.Collections.Generic;

namespace MathParser
{
    public class Lexer(string expression)
    {
        private bool _negateNextValue = false;
        private TokenStream? _numericalLiteralStream;
        
        private readonly Dictionary<char, Token.TokenType> _operatorTokens = new()
        {
            ['+'] = Token.TokenType.AdditionOperator,
            ['-'] = Token.TokenType.SubtractionOperator,
            ['*'] = Token.TokenType.MultiplicationOperator,
            ['/'] = Token.TokenType.DivisionOperator,
            ['^'] = Token.TokenType.ExponentialOperator,
            ['('] = Token.TokenType.OpenParenthesis,
            [')'] = Token.TokenType.CloseParenthesis
        };

        public List<Token> Tokenize()
        {
            List<Token> tokens = new();
            _numericalLiteralStream = null;
            _negateNextValue = false;

            for (int i = 0; i < expression.Length; i++)
            {
                char current = expression[i];

                if (char.IsNumber(current))
                    HandleNumericDigit(i, tokens);
                else if (_operatorTokens.TryGetValue(expression[i], out Token.TokenType tokenType))
                    HandleSignDigit(i, tokens, tokenType);
            }

            return tokens;
        }

        private void HandleNumericDigit(int i, List<Token> tokens)
        {
            bool isLastDigit = i == expression.Length - 1;
                    
            _numericalLiteralStream ??= new TokenStream(i, expression, _negateNextValue);
            _numericalLiteralStream.AddToNumericalLiteralStream();
            _negateNextValue = false;
                    
            if(isLastDigit || !char.IsNumber(expression[i + 1]))
            {
                Token numericalLiteralToken = _numericalLiteralStream.StopNumericalLiteralStream();
                _numericalLiteralStream = null;
                tokens.Add(numericalLiteralToken);
            }
        }

        private void HandleSignDigit(int i, List<Token> tokens, Token.TokenType tokenType)
        {
            bool canTreatAsNegate = i == 0 || tokens[^1].IsOperator() || tokens[^1].tokenType == Token.TokenType.OpenParenthesis;
            bool isNegateInsteadOfSubtraction = canTreatAsNegate && tokenType == Token.TokenType.SubtractionOperator;
                    
            if (isNegateInsteadOfSubtraction)
            {
                _negateNextValue = true;
                return;
            }
                            
            tokens.Add(new Token(tokenType, expression[i].ToString()));
        }
    }
}