namespace MathParser
{
    public readonly struct Token(Token.TokenType tokenType, string value)
    {
        public readonly TokenType tokenType = tokenType;
        public readonly string value = value;
        
        private static readonly TokenType[] OperatorTypes =
        {
            TokenType.AdditionOperator,
            TokenType.SubtractionOperator,
            TokenType.MultiplicationOperator, 
            TokenType.DivisionOperator,
            TokenType.ExponentialOperator
        };

        public bool IsOperator()
        {
            foreach (TokenType operatorType in OperatorTypes)
            {
                if (operatorType == tokenType)
                    return true;
            }

            return false;
        }

        public enum TokenType
        {
            NumericLiteral,
            OpenParenthesis,
            CloseParenthesis,
            AdditionOperator,
            SubtractionOperator,
            MultiplicationOperator,
            DivisionOperator,
            ExponentialOperator
        }
    }
}