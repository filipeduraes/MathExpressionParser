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
            ['^'] = Token.TokenType.ExponentialOperator
        };

        private readonly Dictionary<char, Token.TokenType> _auxiliaryTokens = new()
        {
            ['('] = Token.TokenType.OpenParenthesis,
            [')'] = Token.TokenType.CloseParenthesis
        };
        
        private readonly Dictionary<char, Token.TokenType> _unaryOperatorTokens = new()
        {
            ['-'] = Token.TokenType.NegateOperator,
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
                else
                    HandleSignDigit(i, tokens);
            }

            return tokens;
        }

        public bool IsOperator(Token token)
        {
            return _operatorTokens.ContainsValue(token.tokenType);
        }
        
        public bool IsUnaryOperator(Token token)
        {
            return _unaryOperatorTokens.ContainsValue(token.tokenType);
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

        private void HandleSignDigit(int i, List<Token> tokens)
        {
            bool isUnaryOperator = i == 0 || IsOperator(tokens[^1]) || tokens[^1].tokenType == Token.TokenType.OpenParenthesis;
                
            if (isUnaryOperator && _unaryOperatorTokens.TryGetValue(expression[i], out Token.TokenType tokenType))
            {
                tokens.Add(new Token(Token.TokenType.NegateOperator, expression[i].ToString()));
            }
            else if (_operatorTokens.TryGetValue(expression[i], out tokenType) || _auxiliaryTokens.TryGetValue(expression[i], out tokenType))
            {
                tokens.Add(new Token(tokenType, expression[i].ToString()));
            }
        }
    }
}