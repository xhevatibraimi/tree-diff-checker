using System.Collections.Generic;
using System.IO;
using System.Linq;
using XChecker.Abstractions;
using XChecker.Utils;

namespace XChecker.FileSystem
{
    public class FileSystemLeafProvider : ILeafProvider
    {
        public List<LeafInformation> GetLeafs(string path)
        {
            return Directory.GetFiles(path).Select(filePath =>
            {
                return new LeafInformation
                {
                    FullPath = PathHelper.ConvertToUnixPath(filePath),
                    IsVisited = false,
                    RelativePath = null
                };
            }).ToList();
        }
    }
}
