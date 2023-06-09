using TreeDifferenceChecker.Abstractions;
using TreeDifferenceChecker.Core;
using TreeDifferenceChecker.Core.Eager;

namespace TreeDifferenceChecker.FileSystem
{
    public class FileSystemDiffChecker
    {
        public static IDiffChecker Create()
        {
            var fileSystemPathProvider = new FileSystemPathProvider();
            return new DiffChecker(fileSystemPathProvider);
        }
    }
}
