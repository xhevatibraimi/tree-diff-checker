using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using XChecker.Abstractions;
using XChecker.Core;
using XChecker.FileSystem;
using XChecker.InMemory;

namespace XChecker.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            LazyDiffChecker();
        }

        private static void FileNavigatorDemo()
        {
            var leafProvider = new FileSystemLeafProvider();
            var nodeProvider = new FileSystemNodeProvider();
            var fileNavigator = new LazyLeafTraverser("c:/files/test", leafProvider, nodeProvider);
            foreach (var fileInformation in fileNavigator)
            {
                Console.WriteLine(fileInformation.RelativePath);
            }
        }

        private static void LazyDiffChecker()
        {
            var leftPath = @"c:/files/test/project-one";
            var rightPath = @"c:/files/test/project-two";


            var leafComparer = new FileSystemLeafComparer();
            var leafProvider = new FileSystemLeafProvider();
            var nodeProvider = new FileSystemNodeProvider();
            LazyDiffChecker lazyDiffChecker = new LazyDiffChecker(leftPath, rightPath, leafComparer, leafProvider, nodeProvider);
            foreach (var fileDifference in lazyDiffChecker)
            {
                Console.WriteLine($"[{fileDifference.DifferenceType}]: ");
            }
        }

        private static void NormalDiffChecker()
        {
            Differences differences = CLIGetFileSystemDifferences();

            Console.WriteLine($"deletions: {differences.Left.Count()}");
            File.WriteAllLines("deletions.txt", differences.Left);

            Console.WriteLine($"common: {differences.Common.Count()}");
            File.WriteAllLines("common.txt", differences.Common);

            Console.WriteLine($"additions: {differences.Right.Count()}");
            File.WriteAllLines("insertions.txt", differences.Right);
        }

        private static Differences CLIGetFileSystemDifferences()
        {
            var args = Environment.GetCommandLineArgs();

            if (args.Length != 3)
                Environment.Exit(1);

            var path1 = args[1];
            var path2 = args[2];

            Console.WriteLine(path1);
            Console.WriteLine(path2);

            IDiffChecker diffChecker = FileSystemDiffChecker.Create();

            var differences = diffChecker.FindDifferencesInPathNames(path1, path2);
            return differences;
        }

        private static Differences GetFileSystemDifferences()
        {
            var path1 = @"c:/files/test/project-one";
            var path2 = @"c:/files/test/project-two";

            IDiffChecker diffChecker = FileSystemDiffChecker.Create();

            var differences = diffChecker.FindDifferencesInPathNames(path1, path2);
            return differences;
        }

        private static Differences GetInMemoryDifferences()
        {
            var path1 = @"c:/files/project-one";
            var path2 = @"c:/files/project-two";

            var paths = TestPathGenerator.GeneratePaths();
            IDiffChecker diffChecker = InMemoryDiffChecker.Create(paths);

            var differences = diffChecker.FindDifferencesInPathNames(path1, path2);
            return differences;
        }

        static void Print(IEnumerable<string> items, ConsoleColor color)
        {
            foreach (var item in items)
            {
                Console.ForegroundColor = color;
                Console.WriteLine(item);
                Console.ResetColor();
            }
        }
    }
}
