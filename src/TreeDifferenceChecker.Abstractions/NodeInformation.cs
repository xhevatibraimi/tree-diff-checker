using System.Collections.Generic;

namespace TreeDifferenceChecker.Abstractions
{
    public class NodeInformation
    {
        public string Path { get; set; }
        public bool IsCompleted { get; set; }

        public NodeInformation ParentNode { get; set; }
        public List<NodeInformation> ChildNodes { get; set; }
        public List<LeafInformation> Leafs { get; set; }

        public NodeInformation()
        {
            ChildNodes = new List<NodeInformation>();
            Leafs = new List<LeafInformation>();
        }
    }
}
