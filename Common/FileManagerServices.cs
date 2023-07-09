namespace DevOpsBck.Common
{
    public class FileManagerServices : IFileManagerServices
    {
        public void ZipFolders(IConfiguration configuration)
        {
            List<string> listFolder = Directory.GetDirectories(configuration.Settings.DestinationPath).ToList();
            if(listFolder.Count > 0)
            {
                foreach (string folder in listFolder)
                {
                    ZipFile.CreateFromDirectory(folder, folder+"_"+DateTime.Now.ToString("ddMMyyyyHHmmss").Trim()+".zip");
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
    }
}
