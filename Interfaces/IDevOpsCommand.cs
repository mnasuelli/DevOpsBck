namespace DevOpsBck.Interfaces
{
    public interface IDevOpsCommand
    {
        public RepoRoot GetRepositories(HttpClient clientBase, IConfiguration configuration);
        public RepoFilesRoot GetRepoFiles(string idRepo,HttpClient clientBase, IConfiguration configuration);
        public Task<bool> DownloadRepoFile(string idRepo, string cloudPath, string localPath, HttpClient clientBase, IConfiguration configuration);
    }
}
