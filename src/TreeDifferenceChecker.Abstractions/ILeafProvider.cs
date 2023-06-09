using System.Collections.Generic;

namespace TreeDifferenceChecker.Abstractions
{
    public interface ILeafProvider
    {
        List<LeafInformation> GetLeafs(string path);
    }
}
