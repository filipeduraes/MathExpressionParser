namespace MathParser
{
    public class UnaryOperationNode(Token operation, IExpressionNode operand) : IExpressionNode
    {
        public double Evaluate()
        {
            switch (operation.tokenType)
            {
                case Token.TokenType.NegateOperator:
                    return -operand.Evaluate();
                default:
                    return double.NaN;
            }
        }
    }
}