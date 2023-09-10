using DevOpsBck.Interfaces;

namespace DevOpsBck.Common
{
    public class Configuration : IConfiguration
    {
        public AppSettings? Settings { get; }
        public string Credentials { get; }

        public Configuration()
        {
            using (StreamReader file = File.OpenText(@"jsconfig.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                Settings = (AppSettings?)serializer.Deserialize(file, typeof(AppSettings));
            }
            Credentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", "", Settings?.PersonalAccessToken)));
        }
        public class AppSettings
        {
            public string? OrganizationName { get; set; }
            public string? ProjectName { get; set; }
            public string? user { get; set; }
            public string? PersonalAccessToken { get; set; }
            public string? DestinationPath { get; set; }
            public string? BaseUri { get; set; }
            public string? ApiVersion { get; set; }

            public int BackupDays { get; set; }
        }
    }
}
