using System.Collections.Generic;

namespace XChecker.Abstractions
{
    public interface IPathProvider
    {
        IEnumerable<string> GetTraversedPaths(string path);
    }
}
