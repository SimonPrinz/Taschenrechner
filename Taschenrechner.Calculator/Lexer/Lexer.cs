using System;
using System.Collections.Generic;
using Taschenrechner.Calculator.Common;

namespace Taschenrechner.Calculator.Lexer
{
    public class Lexer
    {
        private PositionQueue<IToken> Tokens;
        private PositionQueue<char> Chars;

        public PositionQueue<IToken> Analyze(string pInput)
        {
            Tokens = new PositionQueue<IToken>();
            Chars = BuildQueue(pInput);

            while (Chars.Count > 0)
            {
                IToken token = null;
                if (IsWhitespace(Chars.Peek()))
                    Chars.Dequeue();
                else if (IsDigit(Chars.Peek()))
                    token = ReadNumber();
                else if (IsOperation(Chars.Peek()))
                    if (Tokens[Tokens.Count - 1].GetTokenType() == TokenType.Operator)
                        throw new NotImplementedException($"unexpected symbol \"{Chars.Dequeue()}\" at position {Chars.Position}");
                    else
                        token = ReadOperation();
                else
                    throw new NotImplementedException($"unexpected symbol \"{Chars.Dequeue()}\" at position {Chars.Position}");
                if (token != null)
                    Tokens.Enqueue(token);
            }

            return Tokens;
        }

        private void CheckNegative()
        {
            if (Tokens.Count == 1 &&
                ((OperatorToken)Tokens[Tokens.Count - 1]).Operator == OperatorType.Minus)
            {
                Tokens.Enqueue(new OperatorToken
                {
                    Operator = OperatorType.Minus
                });
                (Tokens[Tokens.Count - 2]) = new NumberToken
                {
                    Number = 0
                };
                return;
            }

            if (Tokens.Count < 2)
            {
                return;
            }

            if (Tokens[Tokens.Count - 1].GetTokenType() == TokenType.Operator &&
                Tokens[Tokens.Count - 2].GetTokenType() == TokenType.Operator)
            {
                if (((OperatorToken) Tokens[Tokens.Count - 1]).Operator == OperatorType.Minus)
                {
                    Tokens.Enqueue(new OperatorToken
                    {
                        Operator = OperatorType.Minus
                    });
                    (Tokens[Tokens.Count - 2]) = new NumberToken
                    {
                        Number = 0
                    };
                }
            }
        }

        private IToken ReadNumber(bool pComma = false)
        {
            //CheckNegative();

            string numberStr = $"{Chars.Dequeue()}";
            while (IsDigit(Chars.Peek()))
                numberStr += Chars.Dequeue();

            double number = double.Parse(numberStr);
            if (!pComma && Chars.Peek().Equals('.'))
            {
                Chars.Dequeue();
                numberStr = "";
                int decCounter = 1;
                while (IsDigit(Chars.Peek()))
                {
                    numberStr += Chars.Dequeue();
                    decCounter *= 10;
                }
                number += (double.Parse(numberStr) / decCounter);
            }

            return new NumberToken
            {
                Number = number
            };
        }

        private IToken ReadOperation()
        {
            return new OperatorToken
            {
                Operator = OperatorToken.GetOperatorBySymbol(Chars.Dequeue())
            };
        }

        private bool IsOperation(char pChar)
        {
            try
            {
                OperatorToken.GetOperatorBySymbol(pChar);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool IsWhitespace(char pChar)
            => pChar.Equals(' ');

        private bool IsDigit(char pChar)
            => pChar.Equals('.') || pChar.Equals('0') ||
            pChar.Equals('1') || pChar.Equals('2') || pChar.Equals('3') ||
            pChar.Equals('4') || pChar.Equals('5') || pChar.Equals('6') ||
            pChar.Equals('7') || pChar.Equals('8') || pChar.Equals('9');

        private PositionQueue<char> BuildQueue(string pInput)
            => new PositionQueue<char>(pInput.ToCharArray());
    }
}
