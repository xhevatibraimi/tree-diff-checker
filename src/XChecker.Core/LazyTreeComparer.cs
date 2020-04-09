using XChecker.Abstractions;

namespace XChecker.Core
{
    public class LazyTreeComparer
    {
        private readonly ILeafProvider LeftLeafProvider;
        private readonly ILeafProvider RightLeafProvider;
        private readonly ILeafComparer LeafComparer;

        public LazyTreeComparer(ILeafProvider leftLeafProvider, ILeafProvider rightLeafProvider, ILeafComparer leafComparer, ITreeComparer comparer)
        {
            LeftLeafProvider = leftLeafProvider;
            RightLeafProvider = rightLeafProvider;
            LeafComparer = leafComparer;
        }
    }
}
