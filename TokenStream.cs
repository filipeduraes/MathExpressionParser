namespace MathParser
{
    public class TokenStream(int initialIndex, string expression, bool negate = false)
    {
        private int _count = 0;
        
        public void AddToNumericalLiteralStream()
        {
            _count++;
        }

        public Token StopNumericalLiteralStream()
        {
            return GetNumericalLiteral();
        }
        
        private Token GetNumericalLiteral()
        {
            string value = expression.Substring(initialIndex, _count);

            if (negate)
                value = $"-{value}";
            
            return new Token(Token.TokenType.NumericLiteral, value);
        }
    }
}