using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using XChecker.Abstractions;
using XChecker.Utils;

namespace XChecker.Core
{
    public class LeafEnumerator : IEnumerator<LeafInformation>, IEnumerator, IDisposable
    {
        private readonly ILeafProvider LeafProvider;
        private readonly INodeProvider NodeProvider;

        public string InitialPath { get; private set; }
        public LeafInformation Current { get; private set; }
        object IEnumerator.Current { get => Current; }
        public NodeInformation CurrentDirectory { get; private set; }
        public LeafEnumerator(string initialPath, ILeafProvider leafProvider, INodeProvider nodeProvider)
        {
            InitialPath = PathHelper.ConvertToUnixPath(initialPath);
            LeafProvider = leafProvider;
            NodeProvider = nodeProvider;
            Initialize();
        }
        public bool MoveNext()
        {
            var nextCommand = NavigationState.LookForNextFileInTheCurrentDirectory;
            while (true)
            {
                if (nextCommand == NavigationState.LookForNextFileInTheCurrentDirectory)
                {
                    var nextFile = CurrentDirectory.Leafs.FirstOrDefault(file => !file.IsVisited);
                    if (nextFile != null)
                    {
                        Current = nextFile;
                        Current.IsVisited = true;
                        nextCommand = NavigationState.NextFileFound;
                    }
                    else
                    {
                        nextCommand = NavigationState.MoveToNextNotCompletedChildDirectory;
                    }
                }
                else if (nextCommand == NavigationState.MoveToNextNotCompletedChildDirectory)
                {
                    var nextDirectory = CurrentDirectory.ChildNodes.FirstOrDefault(directory => !directory.IsCompleted);
                    if (nextDirectory != null)
                    {
                        CurrentDirectory = nextDirectory;
                        CurrentDirectory.ChildNodes = NodeProvider.GetNodes(CurrentDirectory.Path).Select(node => new NodeInformation
                        {
                            Path = node.Path,
                            IsCompleted = false,
                            ParentNode = CurrentDirectory
                        }).ToList();
                        CurrentDirectory.Leafs = LeafProvider.GetLeafs(CurrentDirectory.Path).Select(leaf =>
                        {
                            return new LeafInformation
                            {
                                FullPath = leaf.FullPath,
                                IsVisited = false,
                                RelativePath = leaf.FullPath.Substring(InitialPath.Length + 1)
                            };
                        }).ToList();
                        nextCommand = NavigationState.LookForNextFileInTheCurrentDirectory;
                    }
                    else
                    {
                        nextCommand = NavigationState.MoveToParentDirectory;
                    }
                }
                else if (nextCommand == NavigationState.MoveToParentDirectory)
                {
                    if (CurrentDirectory.ParentNode != null)
                    {
                        CurrentDirectory.IsCompleted = true;
                        CurrentDirectory = CurrentDirectory.ParentNode;
                        nextCommand = NavigationState.MoveToNextNotCompletedChildDirectory;
                    }
                    else
                    {
                        nextCommand = NavigationState.BreakTheCycle;
                    }
                }
                else if (nextCommand == NavigationState.NextFileFound)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public void Dispose() => Reset();
        public void Reset() => Initialize();
        public void Initialize()
        {
            Current = null;
            CurrentDirectory = new NodeInformation();

            var leafs = LeafProvider.GetLeafs(InitialPath);
            CurrentDirectory.Leafs = leafs.Select(leaf =>
            {
                LeafInformation leafInformation = new LeafInformation
                {
                    FullPath = leaf.FullPath,
                    IsVisited = false,
                    RelativePath = leaf.FullPath.Substring(InitialPath.Length + 1)
                };
                return leafInformation;
            }).ToList();
            CurrentDirectory.ChildNodes = NodeProvider.GetNodes(InitialPath).Select(node => new NodeInformation
            {
                Path = node.Path,
                IsCompleted = false,
                ParentNode = CurrentDirectory
            }).ToList();
        }
    }
}

enum NavigationState
{
    BreakTheCycle = 0,
    LookForNextFileInTheCurrentDirectory = 1,
    MoveToNextNotCompletedChildDirectory = 2,
    MoveToParentDirectory = 3,
    NextFileFound = 4,
}

