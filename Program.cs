Dictionary<string, string> reposIdentity = new Dictionary<string, string>();
//Init Configuration Parameters
IConfiguration Configuration = new Configuration();
//Init Client 
ClientBase baseClient = new ClientBase(Configuration);
var myclient = baseClient.GetClient();
//Get Repositories List
IDevOpsCommand devOpsCommand = new DevOpsCommand();
RepoRoot repos = devOpsCommand.GetRepositories(myclient, Configuration);
//Make Folders or Upadate if exist
IFileManagerServices fileManager = new FileManagerServices();
fileManager.ZipFolders(Configuration);
if (repos != null && repos.count > 0)
{
    foreach (RepoModel.Value vals in repos.value)
    {
        reposIdentity.Add(vals.id, vals.name);
    }
}
fileManager.CreateFolders(reposIdentity.Values.ToList(), Configuration);
LogsServicecs.InitLogs();
//Download Files inside Destination Folder
if (reposIdentity.Count > 0)
{
    foreach (var id in reposIdentity.Keys.ToList())
    {
        //Set Folder Path
        string? repoName = reposIdentity.Where(x => x.Key.Equals(id)).Select(x => x.Value).FirstOrDefault();
        string rootRepoPath = Configuration.Settings.DestinationPath + repoName;
        //Get Files From Repo
        RepoFilesRoot files = devOpsCommand.GetRepoFiles(id, myclient, Configuration);

        if (files.count > 0 && files != null)
        {
            foreach (FilesRepo.Value vals in files.value)
            {
                string? filePath = rootRepoPath + vals.path.Replace("/", @"\");
                if (vals.isFolder && !vals.path.Equals("/"))
                {
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                }
                bool confirm = false;
                if (vals.gitObjectType == "blob")
                    confirm = await devOpsCommand.DownloadRepoFile(id, vals.path, filePath, myclient, Configuration);
                if (confirm)
                {
                    LogsServicecs.AddLogs(IEnumResult.Result.Success.ToString(), vals.path, DateTime.Now.ToString());
                }
            }
        }
    }
    LogsServicecs.CreateFileLogs();
}
else
{
    LogsServicecs.AddLogs(IEnumResult.Result.Warning.ToString(), "No Repo To Backup", DateTime.Now.ToString());
    LogsServicecs.CreateFileLogs();
}
