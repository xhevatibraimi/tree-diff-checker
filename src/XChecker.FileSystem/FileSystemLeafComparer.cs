using System.IO;
using XChecker.Abstractions;

namespace XChecker.FileSystem
{
    public class FileSystemLeafComparer : ILeafComparer
    {
        public LeafDifference CompareFiles(LeafInformation leftFile, LeafInformation rightFile)
        {
            var leftFileByteLength = File.ReadAllBytes(leftFile.FullPath).Length;
            var rightFileByteLength = File.ReadAllBytes(rightFile.FullPath).Length;
            if (leftFileByteLength == rightFileByteLength)
                return new LeafDifference { DifferenceType = LeafDifferenceType.None };
            else
                return new LeafDifference { DifferenceType = LeafDifferenceType.Modified };
        }
    }
}
