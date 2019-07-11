
using System.Collections.Generic;
using System.Linq;

namespace csrogue
{
    // Note: this is from a series of blog posts on MSDN:
    //   https://blogs.msdn.microsoft.com/ericlippert/2007/10/08/path-finding-using-a-in-c-3-0-part-three/
    public class PriorityQueue<P, V>
    {
        private SortedDictionary<P, Queue<V>> list = new SortedDictionary<P, Queue<V>>();

        public void Enqueue(P priority, V value)
        {
            Queue<V> q;
            if (!list.TryGetValue(priority, out q))
            {
                q = new Queue<V>();
                list.Add(priority, q);
            }
            q.Enqueue(value);
        }

        public V Dequeue()
        {
            // will throw if there isn't any first element!
            var pair = list.First();
            var v = pair.Value.Dequeue();
            if (pair.Value.Count == 0)
            {
                // nothing left of top priority
                list.Remove(pair.Key);
            }
            return v;
        }

        public bool IsEmpty
        {
            get { return !list.Any(); }
        }
    }
}
