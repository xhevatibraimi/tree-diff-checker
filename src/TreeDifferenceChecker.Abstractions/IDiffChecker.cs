namespace TreeDifferenceChecker.Abstractions
{
    public interface IDiffChecker
    {
        Differences FindDifferencesInPathNames(string path1, string path2);
    }
}
