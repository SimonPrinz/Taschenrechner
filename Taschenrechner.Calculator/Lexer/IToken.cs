namespace Taschenrechner.Calculator.Lexer
{
    public interface IToken
    {
        TokenType GetTokenType();


        int GetPrecedence();
    }
}
