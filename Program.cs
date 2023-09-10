Dictionary<string, string> reposIdentity = new Dictionary<string, string>();
//Inizializzo i parametri di Configurazione
IConfiguration Configuration = new Configuration();
//Inizializzo il Client 
ClientBase baseClient = new ClientBase(Configuration);
var myclient = baseClient.GetClient();
//Ottengo la lista dei Repositories
IDevOpsCommand devOpsCommand = new DevOpsCommand();
RepoRoot repos = devOpsCommand.GetRepositories(myclient, Configuration);
//Creo le Cartelle o le aggiorno.
IFileManagerServices fileManager = new FileManagerServices();
if (repos != null && repos.count > 0)
{
    foreach (RepoModel.Value vals in repos.value)
    {
        reposIdentity.Add(vals.id, vals.name);
    }
}
fileManager.CreateFolders(reposIdentity.Values.ToList(), Configuration);
LogsServicecs.InitLogs();
//Scarico i File dentro alle Cartelle
if (reposIdentity.Count > 0)
{
    foreach (var id in reposIdentity.Keys.ToList())
    {
        //Setto la cartella Root del progetto
        string? repoName = reposIdentity.Where(x => x.Key.Equals(id)).Select(x => x.Value).FirstOrDefault();
        LogsServicecs.AddLogs(IEnumResult.Result.Normal.ToString(), repoName, DateTime.Now.ToString());
        string rootRepoPath = Configuration.Settings.DestinationPath + repoName;
        fileManager.ZipFolders(rootRepoPath, Configuration);
        rootRepoPath = fileManager.CreateSubFolders(rootRepoPath+ @"/"+repoName, Configuration, true);
        RepoFilesRoot? files = null;
        BranchesRootobject? branches = devOpsCommand.GetBranches(id, myclient, Configuration);
        for (int i = 0; i < branches.count; i++)
        {
            string branchName = branches.value[i].name.Replace("refs/heads/","");
            //Ottengo l'elenco dei Files del Repo
            files = devOpsCommand.GetRepoFiles(id, myclient, Configuration, branchName);
            string localPathFile = rootRepoPath + @"/" + branchName;
            localPathFile = fileManager.CreateSubFolders(localPathFile, Configuration, false);
            LogsServicecs.AddLogs(IEnumResult.Result.Normal.ToString(), localPathFile, DateTime.Now.ToString());
            if (files != null && files.count > 0)
            {
                foreach (FilesRepo.Value vals in files.value)
                {
                    string? filePath = localPathFile + vals.path.Replace("/", @"\");
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
    }
    LogsServicecs.CreateFileLogs();
}
else
{
    LogsServicecs.AddLogs(IEnumResult.Result.Warning.ToString(), "No Repo To Backup", DateTime.Now.ToString());
    LogsServicecs.CreateFileLogs();
}
