using System.Collections.Generic;

namespace XChecker.Abstractions
{
    public interface ILeafProvider
    {
        List<LeafInformation> GetLeafs(string path);
    }
}
