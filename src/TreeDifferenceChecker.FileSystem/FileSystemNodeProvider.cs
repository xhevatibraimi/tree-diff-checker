using System.Collections.Generic;
using System.IO;
using System.Linq;
using TreeDifferenceChecker.Abstractions;
using TreeDifferenceChecker.Utils;

namespace TreeDifferenceChecker.FileSystem
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
