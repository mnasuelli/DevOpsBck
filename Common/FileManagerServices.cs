using DevOpsBck.Interfaces;
using System.Globalization;

namespace DevOpsBck.Common
{
    public class FileManagerServices : IFileManagerServices
    {
        
        public void ZipFolders(string RepoPath, IConfiguration configuration)
        {
            List<string> listFolderToZip = new List<string>();
            List<string> listFolder =  Directory.GetDirectories(RepoPath).ToList();
            if(listFolder.Count > 0)
            {
                foreach (string folder in listFolder)
                {
                    DateTime dateFolderDate;
                    string sdateFolder = folder.Split("-")[1];
                    dateFolderDate = DateTime.ParseExact(sdateFolder,
                                  "yyyyMMdd",
                                   CultureInfo.InvariantCulture);
                    if((DateTime.Now.Date - dateFolderDate).TotalDays > configuration.Settings.BackupDays)
                    {
                        listFolderToZip.Add(folder);
                    }
                }
            }
            if(listFolderToZip.Count > 0)
            {
                foreach (string folder in listFolderToZip)
                {
                    ZipFile.CreateFromDirectory(folder, folder + ".zip");
                    Directory.Delete(folder, true);
                }
            }
        }
        public void CreateFolders(List<string> foldersName, IConfiguration configuration)
        {
            string destinationRootFolder = configuration.Settings.DestinationPath;
            foreach (string folderName in foldersName)
            {
                if(!Directory.Exists(destinationRootFolder + folderName))
                {
                    Directory.CreateDirectory(destinationRootFolder + folderName);
                }    
            }
        }
        public string CreateSubFolders(string path, IConfiguration configuration, bool datestamp)
        {
            string subFolder = path;
            if (datestamp)
            {
                subFolder +="-" + DateTime.Now.Date.ToString("yyyyMMdd");
                if (!Directory.Exists(subFolder))
                {
                    Directory.CreateDirectory(subFolder);
                }
                return subFolder;
            }
            else
            {
                if (!Directory.Exists(subFolder))
                {
                    Directory.CreateDirectory(subFolder);
                }
                return subFolder;
            }         
        }
    }
}
