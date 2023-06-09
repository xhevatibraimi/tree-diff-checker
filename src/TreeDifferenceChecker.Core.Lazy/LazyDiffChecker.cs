using System.Collections;
using System.Collections.Generic;
using TreeDifferenceChecker.Abstractions;
using TreeDifferenceChecker.Utils;
using TreeDifferenceChecker.Core.Eager;

namespace TreeDifferenceChecker.Core.Lazy
{
    public class LazyDiffChecker : IEnumerable<LeafDifference>
    {
        private readonly LeafDifferenceEnumerator Enumerator;

        public LazyDiffChecker(string leftPath, string rightPath, ILeafComparer leafComparer, ILeafProvider leafProvider, INodeProvider nodeProvider)
        {
            leftPath = PathHelper.ConvertToUnixPath(leftPath);
            rightPath = PathHelper.ConvertToUnixPath(rightPath);
            Enumerator = new LeafDifferenceEnumerator(leftPath, rightPath, leafComparer, leafProvider, nodeProvider);
        }

        IEnumerator<LeafDifference> IEnumerable<LeafDifference>.GetEnumerator() => Enumerator;
        IEnumerator IEnumerable.GetEnumerator() => Enumerator;
    }
}
