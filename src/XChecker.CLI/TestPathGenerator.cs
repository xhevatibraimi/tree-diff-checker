using System.Collections.Generic;

namespace XChecker.CLI
{
    public class TestPathGenerator
    {
        public static IEnumerable<string> GeneratePaths()
        {
            return new List<string>
            {
                //project one/folder one
                "c:/files/project-one/folder-one/file-one.txt",
                "c:/files/project-one/folder-one/file-two.txt",
                "c:/files/project-one/folder-one/file-three.txt",
                //project one/folder two
                "c:/files/project-one/folder-two/file-one.txt",
                "c:/files/project-one/folder-two/file-two.txt",
                "c:/files/project-one/folder-two/file-three.txt",
                
                //project two/folder one
                "c:/files/project-two/folder-two/file-one.txt",
                "c:/files/project-two/folder-two/file-two.txt",
                "c:/files/project-two/folder-two/file-three.txt",
                //project two/folder two
                "c:/files/project-two/folder-three/file-one.txt",
                "c:/files/project-two/folder-three/file-two.txt",
                "c:/files/project-two/folder-three/file-three.txt",
            };
        }
    }
}
