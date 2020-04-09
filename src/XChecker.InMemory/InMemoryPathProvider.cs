using System.Collections.Generic;
using System.Linq;
using XChecker.Abstractions;

namespace XChecker.InMemory
{
    public class InMemoryPathProvider : IPathProvider
    {
        private readonly ICollection<string> _paths;

        public InMemoryPathProvider()
        {
            _paths = new List<string>();
        }

        public InMemoryPathProvider(IEnumerable<string> paths)
        {
            _paths = paths.ToList();
        }

        public IEnumerable<string> GetTraversedPaths(string path)
        {
            var paths = new List<string>();
            foreach (var p in _paths)
            {
                if (p.StartsWith(path))
                {
                    paths.Add(p.Substring(path.Length, p.Length - path.Length));
                }
            }
            return paths;
        }
    }
}
