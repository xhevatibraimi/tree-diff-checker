using System.Collections.Generic;
using System.IO;
using System.Linq;
using TreeDifferenceChecker.Abstractions;

namespace TreeDifferenceChecker.FileSystem
{
    public class LazyFileSystemPathProvider : IPathProvider
    {
        public IEnumerable<string> GetTraversedPaths(string path)
        {
            var directoryPaths = new Queue<string>();
            directoryPaths.Enqueue(path);
            while (directoryPaths.Count > 0)
            {
                var directoryPath = directoryPaths.Dequeue();
                var directories = Directory.GetDirectories(directoryPath);
                var files = Directory.GetFiles(directoryPath);
                foreach (var file in files)
                {
                    var newPath = Path.Combine(directoryPath, file);
                    var temporaryPath = string.Join("", newPath.SkipWhile(c => c != '\\'));
                    var resultPath = temporaryPath.Replace('\\', '/');
                    yield return resultPath;
                }
                foreach (var directory in directories)
                {
                    directoryPaths.Enqueue(Path.Combine(directoryPath, directory));
                }
            }
        }
    }
}
