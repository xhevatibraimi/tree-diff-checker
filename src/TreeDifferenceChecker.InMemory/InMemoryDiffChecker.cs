using System.Collections.Generic;
using TreeDifferenceChecker.Abstractions;
using TreeDifferenceChecker.Core.Eager;

namespace TreeDifferenceChecker.InMemory
{
    public class InMemoryDiffChecker
    {
        public static IDiffChecker Create(IEnumerable<string> paths)
        {
            var pathProvider = new InMemoryPathProvider(paths);
            return new DiffChecker(pathProvider);
        }
    }
}
