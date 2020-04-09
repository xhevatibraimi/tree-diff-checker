using System.Collections;
using System.Collections.Generic;
using XChecker.Abstractions;

namespace XChecker.Core
{
    internal class LeafDifferenceEnumerator : IEnumerator<LeafDifference>
    {
        public LeafDifference Current { get; private set; }
        object IEnumerator.Current { get; }
        public string LeftPath { get; }
        public string RightPath { get; }

        private LeafEnumerator LeftFiles;
        private LeafEnumerator RightFiles;
        private ILeafComparer LeafComparer;
        private ILeafProvider LeafProvider;
        private INodeProvider NodeProvider;


        public LeafDifferenceEnumerator(string leftPath, string rightPath, ILeafComparer leafComparer, ILeafProvider leafProvider, INodeProvider nodeProvider)
        {
            LeftPath = leftPath;
            RightPath = rightPath;
            LeafComparer = leafComparer;
            LeafProvider = leafProvider;
            NodeProvider = nodeProvider;
            Initialize();
        }
        public void Dispose() => Initialize();
        public void Reset() => Initialize();
        private void Initialize()
        {
            Current = null;
            LeftFiles = new LeafEnumerator(LeftPath, LeafProvider, NodeProvider);
            RightFiles = new LeafEnumerator(RightPath, LeafProvider, NodeProvider);
        }

        private bool OnlyLeftFileLeft = false;
        private bool OnlyRightFileLeft = false;
        private LeafInformation RightQueuedFile = null;
        private LeafInformation LeftQueuedFile = null;

        public bool MoveNext()
        {
            if (OnlyLeftFileLeft)
            {
                var leftFileExists = LeftFiles.MoveNext();
                if (leftFileExists)
                {
                    Current = new LeafDifference
                    {
                        DifferenceType = LeafDifferenceType.Deleted
                    };
                    return true;
                }
                else
                {
                    return false;
                }
            }

            else if (OnlyRightFileLeft)
            {
                var rightFileExists = RightFiles.MoveNext();
                if (rightFileExists)
                {
                    Current = new LeafDifference
                    {
                        DifferenceType = LeafDifferenceType.Created
                    };
                    return true;
                }
                else
                {
                    return false;
                }
            }

            // left file is in the queue
            else if (LeftQueuedFile != null)
            {
                //get the right file
                var thereIsRightFile = RightFiles.MoveNext();

                if (thereIsRightFile)
                {
                    //if the right file exists => compare the paths
                    RightQueuedFile = RightFiles.Current;
                    int pathComparison = string.Compare(LeftQueuedFile.RelativePath, RightQueuedFile.RelativePath);
                    if (pathComparison < 0)
                    {
                        // if the left file is first in order
                        LeftQueuedFile = null;
                        RightQueuedFile = RightFiles.Current;
                        Current = new LeafDifference
                        {
                            DifferenceType = LeafDifferenceType.Deleted
                        };
                        return true;
                    }
                    else if (pathComparison > 0)
                    {
                        // if the right file is first in order

                        Current = new LeafDifference
                        {
                            DifferenceType = LeafDifferenceType.Created
                        };
                        // left file already in queue
                        return true;
                    }
                    else
                    {
                        // paths are the same => compare the files
                        Current = LeafComparer.CompareFiles(LeftQueuedFile, RightQueuedFile);
                        LeftQueuedFile = null;
                        RightQueuedFile = null;
                        return true;
                    }
                }
                else
                {
                    // mark the left file as "deleted"
                    // remove left file from the queue
                    Current = new LeafDifference { DifferenceType = LeafDifferenceType.Deleted };
                    LeftQueuedFile = null;
                    return true;
                }
            }

            // right file is in the queue
            else if (RightQueuedFile != null)
            {
                //get the right file
                var thereIsLeftFile = LeftFiles.MoveNext();

                if (thereIsLeftFile)
                {
                    //if the left file exists => compare the paths
                    LeftQueuedFile = LeftFiles.Current;
                    int pathComparison = string.Compare(LeftQueuedFile.RelativePath, RightQueuedFile.RelativePath);
                    if (pathComparison < 0)
                    {
                        // left file is first in order
                        // right file already in queue
                        LeftQueuedFile = null;
                        Current = new LeafDifference { DifferenceType = LeafDifferenceType.Deleted };
                        return true;
                    }
                    else if (pathComparison > 0)
                    {
                        // right file is first in order
                        // remove the right file from queue
                        RightQueuedFile = null;
                        Current = new LeafDifference { DifferenceType = LeafDifferenceType.Created };
                        return true;
                    }
                    else
                    {
                        // paths are the same => compare the files
                        RightQueuedFile = null;
                        Current = LeafComparer.CompareFiles(LeftQueuedFile, RightQueuedFile);
                        return true;
                    }
                }
                else
                {
                    // mark the left file as "deleted"
                    // remove left file from the queue
                    Current = new LeafDifference { DifferenceType = LeafDifferenceType.Deleted };
                    LeftQueuedFile = null;
                    return true;
                }
            }

            // there are no files in the queue
            else
            {
                var leftExists = LeftFiles.MoveNext();
                var rightExists = RightFiles.MoveNext();
                // both files exist
                if (leftExists && rightExists)
                {
                    // compare the paths
                    var pathComparison = string.Compare(LeftFiles.Current.RelativePath, RightFiles.Current.RelativePath);
                    // left file is first in order
                    if (pathComparison < 0)
                    {
                        Current = new LeafDifference { DifferenceType = LeafDifferenceType.Deleted };
                        RightQueuedFile = RightFiles.Current;
                        return true;
                    }
                    // right file is first in order
                    else if (pathComparison > 0)
                    {
                        Current = new LeafDifference { DifferenceType = LeafDifferenceType.Created };
                        LeftQueuedFile = LeftFiles.Current;
                        return true;
                    }
                    // paths are the same
                    else
                    {
                        Current = LeafComparer.CompareFiles(LeftFiles.Current, RightFiles.Current);
                        return true;
                    }
                }
                // left file exists, but there is no right file
                else if (leftExists)
                {
                    OnlyLeftFileLeft = true;
                    Current = new LeafDifference { DifferenceType = LeafDifferenceType.Deleted };
                    return true;
                }
                // right file exists, but there is no left file
                else if (rightExists)
                {
                    OnlyRightFileLeft = true;
                    Current = new LeafDifference { DifferenceType = LeafDifferenceType.Created };
                    return true;
                }
                // none of the files exist
                else
                {
                    return false;
                }
            }
        }
    }
}