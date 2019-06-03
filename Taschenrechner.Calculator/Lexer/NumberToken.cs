namespace Taschenrechner.Calculator.Lexer
{
    public class NumberToken : IToken
    {
        TokenType IToken.GetTokenType()
        {
            return TokenType.Number;
        }

        int IToken.GetPrecedence()
        {
            return 6;
        }

        public double Number { get; set; }

        public override string ToString()
        {
            return $"{Number}";
        }
    }
}
