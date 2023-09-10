namespace DevOpsBck.Interfaces
{
    public interface IFileManagerServices
    {
        public void ZipFolders(string RepoPath, IConfiguration configuration);
        public void CreateFolders(List<string> foldersName, IConfiguration configuration);
        public string CreateSubFolders(string path, IConfiguration configuration, bool datestamp);
    }
}
