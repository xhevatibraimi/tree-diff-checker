using System.Collections.Generic;
using XChecker.Abstractions;
using XChecker.Core;

namespace XChecker.InMemory
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
