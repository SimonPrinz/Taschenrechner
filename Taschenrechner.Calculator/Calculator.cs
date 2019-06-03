using System;
using System.Collections.Generic;
using Taschenrechner.Calculator.Common;
using Taschenrechner.Calculator.Lexer;
using Taschenrechner.Calculator.Parser;

namespace Taschenrechner.Calculator
{
    public class Calculator
    {
        private Lexer.Lexer Lexer;
        private Parser.Parser Parser;

        public Exception LastError { get; private set; }

        public Calculator()
        {
            Lexer = new Lexer.Lexer();
            Parser = new Parser.Parser();
        }

        public bool IsValid(string pExpression)
        {
            try
            {
                PositionQueue<IToken> tokens = Lexer.Analyze(pExpression);
                TreeNode treeNode = Parser.Parse(tokens);
                return true;
            }
            catch (Exception e)
            {
                LastError = e;
            }
            return false;
        }

        public double Evaluate(string pExpression)
        {
            try
            {
                PositionQueue<IToken> tokens = Lexer.Analyze(pExpression);
                TreeNode treeNode = Parser.Parse(tokens);
                return treeNode.Evaluate();
            }
            catch (Exception e)
            {
                LastError = e;
                throw new CouldNotEvaluateException(e);
            }
        }
    }
}
