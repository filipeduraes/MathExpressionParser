using System;

namespace MathParser
{
    public class OperationNode(Token operatorToken, IExpressionNode leftOperand, IExpressionNode rightOperand) : IExpressionNode
    {
        public double Evaluate()
        {
            double leftOperandValue = leftOperand.Evaluate();
            double rightOperandValue = rightOperand.Evaluate();

            return operatorToken.tokenType switch
            {
                Token.TokenType.AdditionOperator => leftOperandValue + rightOperandValue,
                Token.TokenType.SubtractionOperator => leftOperandValue - rightOperandValue,
                Token.TokenType.MultiplicationOperator => leftOperandValue * rightOperandValue,
                Token.TokenType.DivisionOperator => leftOperandValue / rightOperandValue,
                Token.TokenType.ExponentialOperator => Math.Pow(leftOperandValue, rightOperandValue),
                _ => double.NaN
            };
        }
    }
}