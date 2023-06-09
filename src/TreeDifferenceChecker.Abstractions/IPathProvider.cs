using System.Collections.Generic;

namespace TreeDifferenceChecker.Abstractions
{
    public interface IPathProvider
    {
        IEnumerable<string> GetTraversedPaths(string path);
    }
}
