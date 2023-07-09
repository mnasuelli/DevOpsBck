namespace DevOpsBck.Interfaces
{
    public interface IFileManagerServices
    {
        public void ZipFolders(IConfiguration configuration);
        public void CreateFolders(List<string> foldersName, IConfiguration configuration);
    }
}
