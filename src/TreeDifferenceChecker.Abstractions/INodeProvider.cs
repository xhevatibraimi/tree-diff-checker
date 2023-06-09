using System.Collections.Generic;

namespace TreeDifferenceChecker.Abstractions
{
    public interface INodeProvider
    {
        List<NodeInformation> GetNodes(string path);
    }
}
