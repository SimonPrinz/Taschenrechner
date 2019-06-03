using System;
using System.Collections.Generic;
using Taschenrechner.Calculator.Common;
using Taschenrechner.Calculator.Lexer;
using Taschenrechner.Calculator.Parser;

namespace Taschenrechner.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            while (true)
            {
                System.Console.Write("Please input the expression: ");
                string lInput = System.Console.ReadLine();

                try
                {
                    Lexer lexer = new Lexer();
                    PositionQueue<IToken> tokens = lexer.Analyze(lInput);
                    foreach (IToken token in tokens.ToArray())
                        System.Console.WriteLine(token);
                    System.Console.ReadKey();

                    Parser parser = new Parser();
                    TreeNode tree = parser.Parse(tokens);
                    BTreePrinter.Print(tree);
                    System.Console.ReadKey();

                    double result = tree.Evaluate();

                    System.Console.WriteLine($" => {result}");
                }
                catch (Exception e)
                {
                    System.Console.WriteLine($"{e.GetType().Name}: {e.Message}");
                    System.Console.WriteLine();
                }
                System.Console.ReadKey();
            }
        }
    }
}
