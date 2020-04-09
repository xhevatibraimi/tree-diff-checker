namespace XChecker.Abstractions
{
    public interface ILeafComparer
    {
        LeafDifference CompareFiles(LeafInformation leftFile, LeafInformation rightFile);
    }
}
