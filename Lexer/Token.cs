namespace MathParser
{
    public readonly struct Token(Token.TokenType tokenType, string value)
    {
        public readonly TokenType tokenType = tokenType;
        public readonly string value = value;

        public enum TokenType
        {
            NumericLiteral,
            OpenParenthesis,
            CloseParenthesis,
            AdditionOperator,
            SubtractionOperator,
            MultiplicationOperator,
            DivisionOperator,
            ExponentialOperator,
            NegateOperator
        }
    }
}