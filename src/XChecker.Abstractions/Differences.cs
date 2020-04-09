using System.Collections.Generic;

namespace XChecker.Abstractions
{
    public class Differences
    {
        public IEnumerable<string> Left { get; set; }
        public IEnumerable<string> Right { get; set; }
        public IEnumerable<string> Common { get; set; }

        public Differences()
        {
            Left = new List<string>();
            Right = new List<string>();
            Common = new List<string>();
        }
    }
}
