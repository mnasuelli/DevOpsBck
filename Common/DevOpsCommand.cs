namespace DevOpsBck.Common
{
    internal class DevOpsCommand : IDevOpsCommand
    {
        public RepoRoot? GetRepositories(HttpClient clientBase, IConfiguration configuration)
        {
            if (clientBase != null && configuration != null)
            {
                try
                {
                    string getRepoCommand = $"{clientBase.BaseAddress}/_apis/git/repositories?/{configuration.Settings?.ApiVersion}";
                    HttpResponseMessage response = clientBase.GetAsync(getRepoCommand).Result;
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        RepoRoot? repos = response.Content.ReadFromJsonAsync<RepoRoot>().Result;
                        if (repos == null)
                        {
                            repos = null;
                        }
                        return repos;
                    }
                }
                catch (Exception ex)
                {
                    LogsServicecs.AddLogs(IEnumResult.Result.Error.ToString(), ex.Message.ToString(), DateTime.Now.ToString());
                    //return null;
                }
                
            }
            return null;
        }

        public RepoFilesRoot? GetRepoFiles(string id, HttpClient clientBase, IConfiguration configuration)
        {
            if (clientBase != null && configuration != null)
            {
                try
                {
                    string getRepoCommand = $"{clientBase.BaseAddress}/_apis/git/repositories/{id}/items?includeContentMetadata=true&recursionlevel=full&{configuration.Settings?.ApiVersion}";
                    HttpResponseMessage response = clientBase.GetAsync(getRepoCommand).Result;
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        RepoFilesRoot? repos = response.Content.ReadFromJsonAsync<RepoFilesRoot>().Result;
                        if (repos == null)
                        {
                            repos = null;
                        }
                        return repos;
                    }
                }
                catch (Exception ex)
                {
                    LogsServicecs.AddLogs(IEnumResult.Result.Error.ToString(), ex.Message.ToString(), DateTime.Now.ToString());
                    return null;
                }               
            }
            return null;
        }

        public async Task<bool> DownloadRepoFile(string idRepo, string cloudPath, string localPath, HttpClient clientBase, IConfiguration configuration)
        {
            if (clientBase != null && configuration != null)
            {
                try
                {
                    HttpClient httpClient = new HttpClient();
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + configuration.Credentials);
                    string getContentCommand = $"{clientBase.BaseAddress}/_apis/git/repositories/{idRepo}/items?scopePath={cloudPath}&includeContent=true&download=true&{configuration.Settings?.ApiVersion}";
                    HttpResponseMessage resultContent = await httpClient.GetAsync(getContentCommand);
                    if (resultContent.IsSuccessStatusCode)
                    {
                        using (Stream output = File.OpenWrite(localPath))
                        {
                            Stream input = await resultContent.Content.ReadAsStreamAsync();
                            await input.CopyToAsync(output);
                            if (File.Exists(localPath))
                                return true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogsServicecs.AddLogs(IEnumResult.Result.Error.ToString(), ex.Message.ToString(), DateTime.Now.ToString());
                    return false;
                }               
            }
            return false;
        }
    }
}
