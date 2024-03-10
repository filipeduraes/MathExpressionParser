namespace MathParser
{
    public class ValueNode(Token valueToken) : IExpressionNode
    {
        public double Evaluate()
        {
            return double.Parse(valueToken.value);
        }
    }
}