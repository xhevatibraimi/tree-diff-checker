using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TreeDifferenceChecker.Abstractions;
using TreeDifferenceChecker.CLI;
using TreeDifferenceChecker.Core.Lazy;
using TreeDifferenceChecker.FileSystem;
using TreeDifferenceChecker.InMemory;

LazyDiffChecker();

static void FileNavigatorDemo()
{
    var leafProvider = new FileSystemLeafProvider();
    var nodeProvider = new FileSystemNodeProvider();
    var fileNavigator = new LazyLeafTraverser("c:/files/test", leafProvider, nodeProvider);
    foreach (var fileInformation in fileNavigator)
    {
        Console.WriteLine(fileInformation.RelativePath);
    }
}
static void LazyDiffChecker()
{
    var leftPath = @"C:\src\test\one";
    var rightPath = @"C:\src\test\two";

    var leafComparer = new FileSystemLeafComparer();
    var leafProvider = new FileSystemLeafProvider();
    var nodeProvider = new FileSystemNodeProvider();
    LazyDiffChecker lazyDiffChecker = new LazyDiffChecker(leftPath, rightPath, leafComparer, leafProvider, nodeProvider);
    foreach (var fileDifference in lazyDiffChecker)
    {
        Console.WriteLine($"[{fileDifference.DifferenceType}]: ");
    }
}
static void NormalDiffChecker()
{
    Differences differences = CLIGetFileSystemDifferences();

    Console.WriteLine($"deletions: {differences.Left.Count()}");
    File.WriteAllLines("deletions.txt", differences.Left);

    Console.WriteLine($"common: {differences.Common.Count()}");
    File.WriteAllLines("common.txt", differences.Common);

    Console.WriteLine($"additions: {differences.Right.Count()}");
    File.WriteAllLines("insertions.txt", differences.Right);
}
static Differences CLIGetFileSystemDifferences()
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
static Differences GetFileSystemDifferences()
{
    var path1 = @"c:/files/test/project-one";
    var path2 = @"c:/files/test/project-two";

    IDiffChecker diffChecker = FileSystemDiffChecker.Create();

    var differences = diffChecker.FindDifferencesInPathNames(path1, path2);
    return differences;
}
static Differences GetInMemoryDifferences()
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
