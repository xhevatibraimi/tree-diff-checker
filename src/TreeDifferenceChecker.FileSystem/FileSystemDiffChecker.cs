using TreeDifferenceChecker.Abstractions;
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

        public static IDiffChecker CreateLazy()
        {
            var fileSystemPathProvider = new LazyFileSystemPathProvider();
            return new DiffChecker(fileSystemPathProvider);
        }
    }
}
