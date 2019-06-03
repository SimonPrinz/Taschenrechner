using Taschenrechner.Calculator.Common;
using Taschenrechner.Calculator.Lexer;

namespace Taschenrechner.Calculator.Parser
{
    public class Parser
    {
        public TreeNode Parse(PositionQueue<IToken> pTokens)
        { 
            /*
             * 1.
             * Get the first item and initialise the tree with it.
             */
            TreeNode root = new TreeNode(pTokens.Dequeue());
            /*
             * 2.
             * Currently the root node is also the current node.
             * The current node is the node we currently lie on.
             */
            TreeNode current = root;

            while (pTokens.Count > 0)
            {
                /*
                 * 3.
                 * Get the next item.
                 * This will be called the new item.
                 */
                current = ProcessTokenToNode(pTokens.Dequeue(), current);
            }

            while (current.Parent != null)
                current = current.Parent;

            return current;
        }

        private TreeNode ProcessTokenToNode(IToken pToken, TreeNode current)
        {
            /*
             * 4.
             * Climb up the tree as long as the precedence of the current node item is greater than or equal to that of the new item.
             * When this is over, the current node item has a precedence strictly less than that of the new item.
             */
            while (current.Precedence >= pToken.GetPrecedence() && current.Parent != null)
            {
                current = current.Parent;
            }

            /*
             * 5.
             * Create a new node for the new item.
             * Then set the left child of the new node to be the old right child of the current node.
             * Finally set the new right child of the current node to be the new node (also set the parent of the new node).
             */
            TreeNode node = new TreeNode(pToken);
            node.Left = current.Left == null ? current : current.Right;
            current.Right = current.Left == null ? null : node;
            node.Parent = current.Left == null ? null : current;

            /*
             * 6.
             * Set the current node to be the new node.
             */
            current = node;

            /*
             * Repeat steps 3, 4, 5 and 6 till there is no item left.
             */
            return current;
        }
    }
}
