
using System.Collections;
using System.Collections.Generic;

namespace csrogue
{
    // Note: this is from a series of blog posts on MSDN:
    //   https://blogs.msdn.microsoft.com/ericlippert/2007/10/04/path-finding-using-a-in-c-3-0-part-two/
    public class Path<Node> : IEnumerable<Node>
    {
        public Node LastStep { get; private set; }
        public Path<Node> PreviousSteps { get; private set; }
        public double TotalCost { get; private set; }

        public Path(Node lastStep, Path<Node> previousSteps, double totalCost)
        {
            LastStep = lastStep;
            PreviousSteps = previousSteps;
            TotalCost = totalCost;
        }

        public Path(Node start) : this(start, null, 0)
        {
        }

        public Path<Node> AddStep(Node step, double stepCost)
        {
            return new Path<Node>(step, this, TotalCost + stepCost);
        }

        public IEnumerator<Node> GetEnumerator()
        {
            for (Path<Node> p = this; p != null; p = p.PreviousSteps)
            {
                yield return p.LastStep;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Node FirstStep
        {
            get
            {
                if (PreviousSteps == null || PreviousSteps.PreviousSteps == null)
                {
                    return LastStep;
                }

                return PreviousSteps.FirstStep;
            }
        }
    }
}
