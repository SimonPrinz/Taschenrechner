using System.Collections.Generic;

namespace Taschenrechner.Calculator.Common
{
    public class PositionQueue<T>
    {
        public int Position { get; private set; }
        public int Count { get => List.Count; }

        public T this[int index]
        {
            get
            {
                return List[index];
            }
            set
            {
                List[index] = value;
            }
        }

        private List<T> List;

        public PositionQueue()
        {
            List = new List<T>();
            Position = 0;
        }
        public PositionQueue(IEnumerable<T> args)
        {
            List = new List<T>(args);
            Position = 0;
        }

        public void Enqueue(T obj)
        {
            List.Add(obj);
        }

        public T Peek()
        {
            if (Count > 0)
            {
                return List[0];
            }

            return default(T);
        }

        public T Dequeue()
        {
            if (Count > 0)
            {
                Position++;
                T obj = List[0];
                List.RemoveAt(0);
                return obj;
            }

            return default(T);
        }

        public T[] ToArray()
        {
            return List.ToArray();
        }
    }
}
