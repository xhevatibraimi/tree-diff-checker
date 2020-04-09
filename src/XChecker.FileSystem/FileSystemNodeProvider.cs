using System.Collections.Generic;
using System.IO;
using System.Linq;
using XChecker.Abstractions;
using XChecker.Utils;

namespace XChecker.FileSystem
{
    public class FileSystemNodeProvider : INodeProvider
    {
        public List<NodeInformation> GetNodes(string path)
        {
            return Directory.GetDirectories(path).Select(folderPath => new NodeInformation
            {
                Path = PathHelper.ConvertToUnixPath(folderPath),
                IsCompleted = false,
                ParentNode = null
            }).ToList();
        }
    }
}
