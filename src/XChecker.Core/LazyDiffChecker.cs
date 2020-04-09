using System.Collections;
using System.Collections.Generic;
using XChecker.Abstractions;
using XChecker.Utils;

namespace XChecker.Core
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
