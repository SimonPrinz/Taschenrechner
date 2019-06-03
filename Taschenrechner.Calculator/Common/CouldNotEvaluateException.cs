using System;

namespace Taschenrechner.Calculator.Common
{
    public class CouldNotEvaluateException : Exception
    {
        public CouldNotEvaluateException(Exception inner) : base(inner.Message, inner)
        {
        }
    }
}

