using System.Collections;
using System.Collections.Generic;
using TreeDifferenceChecker.Abstractions;
using TreeDifferenceChecker.Core.Eager;

namespace TreeDifferenceChecker.Core.Lazy
{
    public class LazyLeafTraverser : IEnumerable<LeafInformation>
    {
        public LeafEnumerator Enumerator { get; }

        public LazyLeafTraverser(string initialPath, ILeafProvider leafProvider, INodeProvider nodeProvider)
        {
            Enumerator = new LeafEnumerator(initialPath, leafProvider, nodeProvider);
        }

        public IEnumerator<LeafInformation> GetEnumerator() => Enumerator;
        IEnumerator IEnumerable.GetEnumerator() => Enumerator;
    }
}
