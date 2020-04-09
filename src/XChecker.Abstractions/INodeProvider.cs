using System.Collections.Generic;

namespace XChecker.Abstractions
{
    public interface INodeProvider
    {
        List<NodeInformation> GetNodes(string path);
    }
}
