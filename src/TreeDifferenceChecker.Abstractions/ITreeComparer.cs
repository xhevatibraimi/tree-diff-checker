using System.Collections.Generic;

namespace TreeDifferenceChecker.Abstractions
{
    public interface ITreeComparer: IEnumerator<LeafDifference>
    {
    }
}
