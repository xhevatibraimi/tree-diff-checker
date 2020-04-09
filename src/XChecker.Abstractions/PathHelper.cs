namespace XChecker.Utils
{
    public static class PathHelper
    {
        public static string ConvertToUnixPath(string path) => path.Replace('\\', '/').Trim('/');
        public static bool IsBefore(this string left, string right) => left.CompareTo(right) > 0;
    }
}
