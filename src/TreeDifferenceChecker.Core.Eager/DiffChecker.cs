using System.Collections.Generic;
using System.Linq;
using TreeDifferenceChecker.Abstractions;

namespace TreeDifferenceChecker.Core.Eager
{
    public class DiffChecker : IDiffChecker
    {
        private readonly IPathProvider _pathProvider;

        public DiffChecker(IPathProvider pathProvider)
        {
            _pathProvider = pathProvider;
        }

        public Differences FindDifferencesInPathNames(string path1, string path2)
        {
            IEnumerable<string> leftPaths = _pathProvider.GetTraversedPaths(path1);
            IEnumerable<string> rightPaths = _pathProvider.GetTraversedPaths(path2);

            var left = new List<string>();
            var right = new List<string>();
            var common = new List<string>();

            foreach (var path in leftPaths)
            {
                if (rightPaths.Contains(path))
                    common.Add(path);
                else
                    left.Add(path);
            }

            foreach (var path in rightPaths)
            {
                if (!leftPaths.Contains(path))
                    right.Add(path);
            }

            return new Differences
            {
                Left = left,
                Right = right,
                Common = common
            };
        }
    }
}
