using System;
using Taschenrechner.Calculator.Lexer;

namespace Taschenrechner.Calculator.Parser
{
    public class TreeNode
    {
        public IToken Value { get; set; }

        public TreeNode Parent { get; set; }

        public TreeNode Left { get; set; }
        public TreeNode Right { get; set; }

        public int Precedence
            => Value.GetPrecedence();

        public TreeNode(IToken pToken)
        {
            Value = pToken;
            Left = Right = Parent = null;
        }

        public double Evaluate()
        {
            double temp;
            if (Is(typeof(NumberToken)))
                return ((NumberToken)Value).Number;
            else if (Is(typeof(OperatorToken)))
                if (Left == null && Right != null)
                    return Right.Evaluate();
                else if (Left != null && Right == null)
                    return Left.Evaluate();
                else
                    switch (((OperatorToken)Value).Operator)
                    {
                        case OperatorType.Plus:
                            return Left.Evaluate() + Right.Evaluate();
                        case OperatorType.Minus:
                            return Left.Evaluate() - Right.Evaluate();
                        case OperatorType.Multiply:
                            return Left.Evaluate() * Right.Evaluate();
                        case OperatorType.Divide:
                            if ((temp = Right.Evaluate()) == 0)
                                throw new DivideByZeroException($"cannot divide by zero");
                            else
                                return Left.Evaluate() / temp;
                        default:
                            throw new NotImplementedException($"unkown OperatorType \"{((OperatorToken)Value).Operator}\"");
                    }
            else
                throw new NotImplementedException($"unkown IToken type \"{Value.GetType()}\"");
        }

        private bool Is(Type type)
            => Value.GetType().Equals(type);

        public override string ToString()
        {
            return $"TreeNode({Left}, {Value}, {Right})";
        }
    }
}
