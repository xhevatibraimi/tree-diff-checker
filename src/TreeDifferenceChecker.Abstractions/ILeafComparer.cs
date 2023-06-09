namespace TreeDifferenceChecker.Abstractions
{
    public interface ILeafComparer
    {
        LeafDifference CompareFiles(LeafInformation leftFile, LeafInformation rightFile);
    }
}
