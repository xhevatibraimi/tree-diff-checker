using XChecker.Abstractions;
using XChecker.Core;

namespace XChecker.FileSystem
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
