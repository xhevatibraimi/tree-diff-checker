using System.Collections.Generic;
using System.IO;
using System.Linq;
using TreeDifferenceChecker.Abstractions;

namespace TreeDifferenceChecker.FileSystem
{
    public class FileSystemPathProvider : IPathProvider
    {
        public IEnumerable<string> GetTraversedPaths(string path)
        {
            var filePaths = new List<string>();
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
                    filePaths.Add(resultPath);
                }
                foreach (var directory in directories)
                {
                    directoryPaths.Enqueue(Path.Combine(directoryPath, directory));
                }
            }
            return filePaths;
        }
    }
}
