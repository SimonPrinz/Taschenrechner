using System;

namespace Taschenrechner.Calculator.Lexer
{
    public class OperatorToken : IToken
    {
        TokenType IToken.GetTokenType()
        {
            return TokenType.Operator;
        }

        int IToken.GetPrecedence()
        {
            switch (Operator)
            {
                /*case OperatorType.BracketOpen:
                case OperatorType.BracketClose:
                    return 1;*/
                case OperatorType.Plus:
                case OperatorType.Minus:
                    return 2;
                /*case OperatorType.Negative:
                    return 3;*/
                case OperatorType.Divide:
                case OperatorType.Multiply:
                    return 4;
                /*case OperatorType.Exponent:
                    return 5;
                case OperatorType.Factorial:
                    return 6;*/
                default:
                    throw new NotImplementedException($"{Operator} is not a valid operator");
            }
        }

        public OperatorType Operator;

        public override string ToString()
        {
            return $"{Operator}";
        }

        public static OperatorType GetOperatorBySymbol(char pChar)
        {
            switch (pChar)
            {
                //case '(': return OperatorType.BracketOpen;
                //case ')': return OperatorType.BracketClose;
                case '+': return OperatorType.Plus;
                case '-': return OperatorType.Minus;
                // case '-': return OperatorType.Negative;
                case '/': return OperatorType.Divide;
                case '*': return OperatorType.Multiply;
                //case '^': return OperatorType.Exponent;
                //case '!': return OperatorType.Factorial;
                default:
                    throw new NotImplementedException($"{pChar} is not a valid operator");
            }
        }

        public static bool IsSingleOperator(OperatorType type)
        {
            return type == OperatorType.Factorial || type == OperatorType.Negative;
        }
    }
}
